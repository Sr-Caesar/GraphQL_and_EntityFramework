using Microsoft.EntityFrameworkCore;
using Serilog.Events;
using Serilog;
using GraphQL_Exercice.GraphQLSchema.Querries;
using GraphQL_Exercice.GraphQLResolvers;
using GraphQL_Exercice.GraphQLSchema.Mutations;

var builder = WebApplication.CreateBuilder(args);
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();
builder.Services.AddDbContext<ExerciceData.Context.AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<CompanyRepository>();
builder.Services.AddGraphQLServer()
    .AddMutationConventions()
    .AddMutationType<Mutation>()
    //.AddType(CompanyType)
    .AddQueryType<Query>();

var app = builder.Build();
app.MapGraphQL();
app.Run();