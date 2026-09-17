using System;
using System.IO;
using System.Net.Http;
using Flurl;
using Flurl.Http;
using Duende.IdentityModel.Client;
using ILS.ConnectionStrings.Client;
using ILS.RfqManagement.DomainModel.Contracts.Repositories;
using ILS.RfqManagement.Endpoints;
using ILS.RfqManagement.Repositories.DbConnections;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

namespace ILS.RfqManagement.IntegrationTests
{
    public class TestClientFixture : IDisposable
    {
        public const string Auth0Domain = "Auth0:Domain";
        public const string Auth0ClientId = "Auth0:ClientId";
        public const string Auth0ClientSecret = "Auth0:ClientSecret";
        public const string Auth0Audience = "Auth0:Audience";
        public const string Auth0Realm = "Auth0:Realm";
        public const string UserId = "8086U01";
        public const string Password = "Exchange2025!";
        public const string Auth0GrantType = "http://auth0.com/oauth/grant-type/password-realm";
        public static readonly string RootUrl;
        public static readonly string CreationPreferencesEndpointsUrl;
        public static readonly string ViewPreferencesEndpointsUrl;
        public static readonly string AuthenticationUrl;
        public static readonly IConfiguration Configuration;
        public static readonly IConnectionFactory ConnectionFactory;
        private static TestServer _server;

        static TestClientFixture()
        {
            var launchSettings = new ConfigurationBuilder()
                .AddJsonFile(Path.GetFullPath(@"..\..\..\..\ILS.RfqManagement.Endpoints\properties\launchsettings.json"), optional: false, reloadOnChange: true)
                .Build();
            RootUrl = launchSettings["iisSettings:iisExpress:applicationUrl"];
            CreationPreferencesEndpointsUrl = Url.Combine(RootUrl, "api/rfqs/CreationPreferences");
            ViewPreferencesEndpointsUrl = Url.Combine(RootUrl, "api/rfqs/ViewPreferences");

            Configuration = new ConfigurationBuilder()
                .AddJsonFile(Path.GetFullPath(@"..\..\..\..\ILS.RfqManagement.Endpoints\appsettings.development.json"), optional: false, reloadOnChange: true)
                .AddJsonFile(Path.GetFullPath(@"..\..\..\..\..\ApplicationConfiguration\development\configuration.json"), optional: false, reloadOnChange: true)
                .Build();
            AuthenticationUrl = Configuration[Auth0Domain];

            ConnectionFactory = new ConnectionFactory(Configuration, new ConnectionStringsClient(Configuration));
        }

        private static TokenResponse GetSecurityToken()
        {
            using (var client = new HttpClient())
            {
                var disco = client.GetDiscoveryDocumentAsync(AuthenticationUrl).SafeResult();

                var opt = new TokenClientOptions();
                opt.Address = disco.TokenEndpoint;
                opt.ClientId = Configuration[Auth0ClientId];
                opt.ClientSecret = Configuration[Auth0ClientSecret];
                opt.Parameters = new() {
                    { "username", UserId },
                    { "password", Password },
                    { "scope", "openid offline_access" },
                    { "realm", Configuration[Auth0Realm] },
                    { "audience", Configuration[Auth0Audience] },
                };

                TokenClient tokenClient = new(client, opt);
                return tokenClient.RequestTokenAsync(Auth0GrantType).SafeResult();
            }
        }

        private static void CreateTestServer()
        {
            if (_server == null)
            {
                _server = new TestServer(
                    new WebHostBuilder()
                        .UseEnvironment("Development")
                        .ConfigureAppConfiguration((hostingContext, config) =>
                        {
                            var env = hostingContext.HostingEnvironment;
                            var configPath = Path.GetFullPath(@"..\..\..\..\ILS.RfqManagement.Endpoints");
                            config.SetBasePath(configPath);
                            config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                            config.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: false, reloadOnChange: true);
                            config.AddJsonFile(
                                Path.GetFullPath(
                                    @"..\..\..\..\..\ApplicationConfiguration\development\configuration.json"),
                                optional: false, reloadOnChange: true);
                        })
                        .UseStartup<Startup>());
            }
        }

        private static FlurlClient GetFlurlClient(string baseUrl)
        {
            return new FlurlClient(_server.CreateClient()) { BaseUrl = baseUrl };
        }

        public TokenResponse Token { get; }
        public IFlurlClient RootFlurlClient { get; }
        public IFlurlClient CreationPreferenceFlurlClient { get; }
        public IFlurlClient ViewPreferenceFlurlClient { get; }

        public TestClientFixture()
        {
            Token = GetSecurityToken();
            CreateTestServer();
            RootFlurlClient = GetFlurlClient(RootUrl);
            CreationPreferenceFlurlClient = GetFlurlClient(CreationPreferencesEndpointsUrl);
            ViewPreferenceFlurlClient = GetFlurlClient(ViewPreferencesEndpointsUrl);
        }

        public void Dispose()
        {
            RootFlurlClient?.Dispose();
            CreationPreferenceFlurlClient?.Dispose();
            _server?.Dispose();
        }
    }
}