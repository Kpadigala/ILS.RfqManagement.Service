using Flurl.Http;
using Shouldly;
using System;
using System.Net;
using Xunit;

namespace ILS.RfqManagement.IntegrationTests.Endpoints
{
    [Collection("Test client collection")]
    public class HealthChecksIntegrationTests
    {
        public class ReadinessResponse
        {
            public string Status { get; set; }
            public DateTime Duration { get; set; }
            public object Entries { get; set; }
        }


        private readonly TestClientFixture _testClientFixture;

        public HealthChecksIntegrationTests(TestClientFixture testClientFixture)
        {
            _testClientFixture = testClientFixture;
        }

        [Fact]
        public void Liveness_ShouldReturnCorrectValue()
        {
            //Arrange
            var expected = "HEALTHY";

            //Act
            var actual = _testClientFixture.RootFlurlClient.Request("liveness")
                .GetAsync()
                .SafeResult();

            //Assert
            actual.StatusCode.ShouldBe((int)HttpStatusCode.OK);
            var content = actual.GetStringAsync().SafeResult();
            content.ShouldNotBeNullOrEmpty();
            content.ToUpper().ShouldBe(expected);
        }

        // No negative test for the /liveness endpoint is possible.
        // If the service is running at all, /liveness should return
        // "healthy". If the service isn't running, there's nothing
        // to run a test against. Since our TestClientFixture is always
        // going to start the service, either our test session will
        // fail before we reach this point (because that step failed),
        // or all calls to the /liveness endpoint will return "healthy".
        // It's impossible to get an "unhealthy" response.

        [Fact]
        public void Readiness_ShouldReturnCorrectValue()
        {
            //Arrange
            var expected = "HEALTHY";

            //Act
            var actual = _testClientFixture.RootFlurlClient.Request("readiness")
                .OnError(call => CheckError(call))
                .GetJsonAsync<ReadinessResponse>()
                .SafeResult();

            //Assert
            void CheckError(FlurlCall call)
            {
                // If the call fails, we'll want to know which dependency reported itself as unhealthy.
                // The response body will contain that information.
                var body = call.Response.GetStringAsync().SafeResult();
                // It's possible we'll get an error response for some reason *other than* a dependency
                // reporting itself as unhealthy. If that happens, we *don't* want to mark the exception
                // as handled so it will bubble on out where we can see it.
                call.ExceptionHandled = !body.Contains("UNHEALTHY");
                // Assert that the body should be empty so the actual contents will be reported in
                // the test output (assuming it *isn't* empty). That's the info we're after.
                body.ShouldBeNullOrEmpty();
            }
            actual.ShouldNotBeNull();
            actual.Status.ToUpper().ShouldBe(expected);
        }

        // This negative test presents a challenge for the person running
        // the tests. For this test to pass, at least one of the services
        // or databases this service depends on has to be unhealthy. If
        // that's the case, then this test will pass, but a lot of our
        // other tests will FAIL (including the one above). It will be
        // impossible to get a completely green run of all tests.
        // The tester will have to set up the conditions where our other
        // tests should pass, run the tests and see everything green
        // except this test. Then change the conditions so this test
        // should pass, run the tests again and see a lot of other tests
        // fail (depending on what services or databases the tester has
        // made unhealthy) but this one pass.
        [Fact(Skip = "It is impossible for this test to succeed at the same time the other tests succeed.")]
        public void Readiness_WhenADependencyIsUnhealthy_ShouldReturnUnhealthy()
        {
            //Arrange
            var expected = "Unhealthy";
            var errorOccurred = false;

            //Act
            _ = _testClientFixture.RootFlurlClient.Request("readiness")
                .OnError(call => CheckError(call))
                .GetJsonAsync<ReadinessResponse>()
                .SafeResult();

            //Assert
            void CheckError(FlurlCall call)
            {
                errorOccurred = true;
                call.ExceptionHandled = true;
                call.Response.StatusCode.ShouldBe((int)HttpStatusCode.ServiceUnavailable);
                call.Response.GetStringAsync()
                    .SafeResult()
                    .ShouldContain(expected);
            }
            // Check to make sure we did, in fact, get an exception. The call did not work.
            errorOccurred.ShouldBeTrue();
        }

    }
}
