


namespace Testing
{
    public class SimpleTest
    {
        [Fact]
        public async Task SchemaChangeTest()
        {
            var schema = await new ServiceCollection()
            .AddGraphQLServer()
            .AddQueryType<Query>()
            .BuildSchemaAsync();

            schema.ToString().MatchSnapshot();
        }
    }
}
