using ILS.Exceptions.Middleware.Extensions;
using ILS.Extensions;
using ILS.HealthChecks;
using ILS.HealthChecks.DomainModel;
using ILS.RfqManagement.Endpoints.Extensions.DependencyInjection;
using ILS.RfqManagement.Repositories.Extensions.DependencyInjection;
using ILS.RfqManagement.Services.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text.RegularExpressions;
using IConnectionFactory = ILS.RfqManagement.DomainModel.Contracts.Repositories.IConnectionFactory;

namespace ILS.RfqManagement.Endpoints
{
    [ExcludeFromCodeCoverage] // JUSTIFICATION: not unit testable due to .NET Core dependencies
    public class Startup
    {
        public class ConfigurationKeys
        {
            public const string IdentityAuthority = "Identity:Authority";
            public const string IdentityAudience = "Identity:Audience";
            public const string ServiceDependencies = "ServiceDependencies";
#if DEBUG
            // this one is read from an environment variable set in launchSettings.json
            public const string LaunchProfile = "LAUNCH_PROFILE";
            public const string AuthorityUrl = "AUTHORITY_URL";
            public const string AudienceUrl = "AUDIENCE_URL";
            public const string KestrelHttpsEndpoint = "Kestrel:Endpoints:Https";
            public const string KestrelHttpsCertificate = $"{KestrelHttpsEndpoint}:Certificate";
#endif
        }

        public const string CorsPolicyName = "ILSPolicy";


        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
#if DEBUG
            const string myServicePort = "7210";
            var assemblyName = System.Reflection.Assembly.GetEntryAssembly()?.GetName().Name;
            var processId = Environment.ProcessId;

            // This sets the title of the command window to show the Project Name along with the Process ID, so it's easier to see when running multiple services
            Console.Title = $"{assemblyName} [Port: {myServicePort}] [PID: {processId}]";
            Console.WriteLine($"{assemblyName} Running on Machine: {Environment.MachineName.ToLower()}");
            Console.WriteLine($"{assemblyName} Process Id: {processId}\n");

            var launchProfile = Configuration[ConfigurationKeys.LaunchProfile];
            if ("MachineName".Equals(launchProfile, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"Identity audience: {Configuration[ConfigurationKeys.IdentityAudience]}");

                Configuration[$"{ConfigurationKeys.KestrelHttpsEndpoint}:Url"] = $"https://*:{myServicePort}";
                Configuration[$"{ConfigurationKeys.KestrelHttpsCertificate}:Subject"] = $"{Environment.MachineName.ToLower()}.ilsdev.lan";
                Configuration[$"{ConfigurationKeys.KestrelHttpsCertificate}:Store"] = "My";
                Configuration[$"{ConfigurationKeys.KestrelHttpsCertificate}:Location"] = "LocalMachine";
            }
            
            // if not set to true, http exceptions during authn/authz will not include the relevant url
            IdentityModelEventSource.ShowPII = true;
#endif

            services.AddMvc(options => options.EnableEndpointRouting = false);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "ILS.RfqManagement.Service",
                    Version = "v1",
                    Description = "ILS.RfqManagement.Service"
                });

                c.CustomSchemaIds(type => type.FullName);

                // Add Bearer token security definition
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Description = "JWT Authorization header using the Bearer scheme."
                });

                // Add security requirement
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                             Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                        },
                        new List<string>()
                    }
                });

                var paths = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", System.IO.SearchOption.TopDirectoryOnly);
                foreach (var path in paths) c.IncludeXmlComments(path, includeControllerXmlComments: true);

            });

            services.RegisterEndpoints()
                .RegisterServices()
                .RegisterRepositories()
                .AddCors(options =>
                {
                    options.AddPolicy(CorsPolicyName, builder =>
                    {
                        builder
                            .SetIsOriginAllowed((origin) =>
                            {
                                var uri = new Uri(origin);
                                // allow any of the following host names:
                                // localhost
                                // [*.]ilsmart.com
                                // mkt-{name}-{one or more digits}.ilsdev.lan
                                var re = new Regex(@"^localhost$|^(?:.*\.?)ilsmart\.com$|^mkt-\w+-\d+\.ilsdev\.lan$|^(?:.*\.?)ilsmart\.com.mkt-\w+-\d+\.ilsdev\.lan$",
                                    RegexOptions.Compiled | RegexOptions.IgnoreCase);
                                return re.IsMatch(uri.Host);
                            })
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
                    });
                });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer((options) =>
                {
#if DEBUG
                    options.RequireHttpsMetadata = false;
#endif

                    // Base address of the identity server
                    options.Authority = Configuration[ConfigurationKeys.IdentityAuthority];
                    // Name of the API resource
                    options.Audience = Configuration[ConfigurationKeys.IdentityAudience];
                    options.AddImpersonationSigningKey();
                });

            var serviceDependencies = Configuration.GetSection(ConfigurationKeys.ServiceDependencies).Get<ServiceDependency[]>();

            services.AddHealthChecks()
                .AddServiceChecks(serviceDependencies)
                .AddOracleCheck<IConnectionFactory>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                app.UseHsts()
                   .UseCustomExceptionMiddleware("ILS.RfqManagement.Service");

            }
            app.UseStaticFiles();
            app.UsePathBase("/rfqmanagement");
            app.UseSwagger(c => { c.RouteTemplate = "docs/{documentName}/doc.json"; });
            if (!env.IsProduction())
            {
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/rfqmanagement/docs/v1/doc.json", "ILS.RfqManagement.Service V1");
                    c.RoutePrefix = "apidocs";

                    if (env.IsProduction())
                    {
                        c.InjectStylesheet("/css/swagger-custom-production.css");
                    }
                });
            }


            app.UseHttpsRedirection()
                .UseAuthentication()
                .UseCors(CorsPolicyName)
                .UseMvc()
                .UseHealthChecks();
        }
    }
}
