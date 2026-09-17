using Xunit;

namespace ILS.RfqManagement.IntegrationTests
{
    [CollectionDefinition("Test client collection")]
    public class TestClientCollection : ICollectionFixture<TestClientFixture>
    {
        // The TestClientFixture creates a TestServer to host the Endpoints instance. It then
        // creates a FlurlClient that calls this test server. All this setup can only be done
        // once per test run.

        // Therefore, in order to be able to run all the integration tests at once ("Run All"
        // in the TestExplorer), all the test classes that involve Flurl client calls to the
        // test hosted Endpoints must be part of a collection that shares a single
        // TestClientFixture instance. This class defines that collection.

        // To add a test class to the collection, apply the [Collection("Test client collection")]
        // attribute to the class.
    }
}