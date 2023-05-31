using Xunit;

namespace PearApi.Test.TestDB;

//Defines the context for multiple tests so that all the test classes so thay all can share the same
//Database context -> https://hamidmosalla.com/2020/02/02/xunit-part-5-share-test-context-with-iclassfixture-and-icollectionfixture/

[CollectionDefinition("Database")]
public class DatabaseCollectionFixture : ICollectionFixture<DatabaseFixture>
{
}