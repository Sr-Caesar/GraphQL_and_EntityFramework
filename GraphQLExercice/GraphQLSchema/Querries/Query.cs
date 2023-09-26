using ExerciceData.Models;
using GraphQL_Exercice.GraphQLResolvers;
using GraphQL_Exercice.Resolvers;

namespace GraphQL_Exercice.GraphQLSchema.Querries
{
    public class Query
    {
        public async Task<IEnumerable<CompanyModel>> GetCompanies([Service] CompanyRepository _companyRepository)
        {
            return await _companyRepository.GetAllCompaniesAsync();
        }
        public async Task<IEnumerable<DriverModel>> GetDrivers([Service] DriverRepository _driverRepository)
        {
            return await _driverRepository.GetAllDriversAsync();
        }
    }
}
