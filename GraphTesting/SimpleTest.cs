


using GraphQL_Exercice.GraphQLSchema.Mutations;
using GraphTesting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Testing
{
    public class SimpleTest
    {
        [Fact]
        public async Task SchemaChangeTest()//CodeFirst or Anotation Based with schema as an artifact
        {
            var schema = await new ServiceCollection()
            .AddGraphQLServer()
            .AddQueryType<Query>()
            .BuildSchemaAsync();

            //var schema = await TestServices.Executor.GetSchemaAsync(default);

            schema.ToString().MatchSnapshot();
        }

        [Fact]//TODO https://www.youtube.com/watch?v=Nf7nX2H_iiM
        public async Task GetCompaniesTest()
        {
            var testBuilder = WebApplication.CreateBuilder(new string[] { });
            string myQuery = @"{
                  companies{
                    id
                    name
                    dateOfFundation
                  }
                }";
            var result = await new ServiceCollection()
            .AddDbContext<ExerciceData.Context.AppDbContext>(option =>
            {
                option.UseSqlServer(testBuilder.Configuration.GetConnectionString("DefaultConnection"));
            })
            .AddScoped<CompanyRepository>()
            .AddGraphQLServer()
            .AddQueryType<Query>()
            .ExecuteRequestAsync(myQuery);

            result.ToJson().MatchSnapshot();
        }
    }
}
