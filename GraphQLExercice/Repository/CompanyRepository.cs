using ExerciceData.Context;
using ExerciceData.Models;
using GraphQL_Exercice.GraphQLSchema;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace GraphQL_Exercice.GraphQLResolvers
{
    public class CompanyRepository 
    {
        private readonly AppDbContext _dbContext;
        public CompanyRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<CompanyModel> UpdateAsync(CompanyModel updatedCompany)
        {
            var existingCompany = await _dbContext.Companies.FindAsync(updatedCompany.Id);
            if (existingCompany == null)
            {
                Log.Information($"Company with ID {updatedCompany.Id} not found.");
                return null;
            }

            _dbContext.Entry(existingCompany).CurrentValues.SetValues(updatedCompany);
            await _dbContext.SaveChangesAsync();
            Log.Information($"Company with ID {updatedCompany.Id} updated");
            return existingCompany;
        }
        public async Task<CompanyModel> InsertAsync(string name)
        {
            var company = new CompanyModel()
            {
                Name = name,
                DateOfFundation = DateTime.Now
            };
            _dbContext.Companies.Add(company);
            await _dbContext.SaveChangesAsync();
            Log.Information($"New Company inserted at {DateTime.Now}");
            return company;
        }
        public async Task<CompanyModel> CreateAsync(CompanyModel company)
        {
            try
            {
                _dbContext.Companies.Add(company);
                await _dbContext.SaveChangesAsync();
                return company;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore durante il salvataggio dei dati: {ex.Message}");
                throw; 
            }
        }

        public async Task<CompanyModel?> DeleteAsync(int id)
        {
            var company = await _dbContext.Companies.FindAsync(id);
            if (company == null)
            {
                Log.Information($"Company not found for ID {id}");
                return null;
            }
            _dbContext.Tirs.RemoveRange(_dbContext.Tirs.Where(t => t.CompanyId == id));
            _dbContext.Drivers.RemoveRange(_dbContext.Drivers.Where(d => d.CompanyId == id));
            _dbContext.Remove(company);
            await _dbContext.SaveChangesAsync();
            Log.Information($"Company with ID {id} successfully deleted.");
            Log.Information($"Drivers and Tirs associated with the company have been removed from the database.");
            return company;
        }
        public async Task<CompanyModel?> GetCompanyByIdAsync(int id)
        {
            var company = await _dbContext.Companies.FirstOrDefaultAsync(c => c.Id == id);
            if (company == null)
            {
                Log.Information($"Company not found for ID {id}");
            }
            Log.Information($"Company found for ID {id}");
            return company;
        }
        public async Task<IEnumerable<CompanyModel>?> GetAllCompaniesAsync()
        {
            var companies = await _dbContext.Companies.ToListAsync();
            if (companies == null || !companies.Any())
            {
                Log.Information("No companies found in the database.");
            }
            Log.Information("Companies found in the database.");
            return companies;
        }
    }

}

