using ExerciceData.Models;

namespace GraphQL_Exercice.Repository
{
    public interface ICompanyRepository
    {
        public interface ICompanyRepository
        {
            Task<CompanyModel> UpdateAsync(CompanyModel updatedCompany);
            Task<CompanyModel> InsertAsync(string name);
            Task<CompanyModel> CreateAsync(CompanyModel company);
            Task<CompanyModel?> DeleteAsync(int id);
            Task<CompanyModel?> GetCompanyByIdAsync(int id);
            Task<IEnumerable<CompanyModel>?> GetAllCompaniesAsync();
        }
    }
}
