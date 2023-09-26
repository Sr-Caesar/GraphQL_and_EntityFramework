using ExerciceData.Models;
using GraphQL_Exercice.GraphQLResolvers;
using GraphQL_Exercice.GraphQLSchema.Mutations.Company;

namespace GraphQL_Exercice.GraphQLSchema.Mutations
{
    public class Mutation
    {
        public async Task<CompanyResult> CreateCompanyAsync([Service] CompanyRepository companyRepository, CompanyInputType companyInput)
        {
            CompanyModel companyModel = new CompanyModel()
            {
                //Id = companyInput.Id,
                Name = companyInput.Name,
                DateOfFundation = (DateTime)companyInput.DateOfFundation
            };
            companyModel = await companyRepository.CreateAsync(companyModel);
            CompanyResult company = ModeltoResult(companyModel);
            return company;
        }
        public async Task<CompanyResult> DeleteCompanyAsync([Service] CompanyRepository companyRepository, int id)
        {
            CompanyModel companyModel = await companyRepository.GetCompanyByIdAsync(id);
            CompanyResult company = ModeltoResult(companyModel);
            await companyRepository.DeleteAsync(id);
            return company;
        }
        public async Task<CompanyResult> UpdateCompanyAsync([Service] CompanyRepository companyRepository, CompanyInputType companyInput)
        {
            CompanyModel companyModel = new CompanyModel()
            {
                Id = (int)companyInput.Id,
                Name = companyInput.Name,
                //DateOfFundation = (DateTime)companyInput.DateOfFundation
            };
            companyModel = await companyRepository.UpdateAsync(companyModel);
            CompanyResult company = ModeltoResult(companyModel);
            return company;
        }
        private CompanyResult ModeltoResult(CompanyModel companyModel) {
            CompanyResult company = new CompanyResult()
            {
                Id = companyModel.Id,
                Name = companyModel.Name,
                DateOfFundation = companyModel.DateOfFundation,
            };
            return company;
        }
    }
}
