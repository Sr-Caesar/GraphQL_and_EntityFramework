using ExerciceData.Models;
using GraphQL_Exercice.GraphQLResolvers;

namespace GraphQL_Exercice.GraphQLSchema.Querries
{
    public class Query
    {
        public async Task<IEnumerable<CompanyModel>> GetCompanies([Service] CompanyRepository _companyRepository)
        {
            return await _companyRepository.GetAllCompaniesAsync();
        }
    }
}
