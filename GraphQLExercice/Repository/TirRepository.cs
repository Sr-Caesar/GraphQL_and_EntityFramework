using ExerciceData.Context;
using ExerciceData.Models;
using GraphQL_Exercice.GraphQLSchema;
using GraphQL_Exercice.Repository;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace GraphQL_Exercice.GraphQLResolvers
{
    public class TirRepository : ITirRepository
    {
        private readonly AppDbContext _dbContext;
        public TirRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<TirModel?> UpdateAsync(TirModel updatedTir)
        {
            var existingTir = await _dbContext.Tirs.FindAsync(updatedTir.Id);
            if (existingTir == null)
            {
                Log.Information($"Tir with ID {updatedTir.Id} not found.");
                return null;
            }
            _dbContext.Entry(existingTir).CurrentValues.SetValues(updatedTir);
            await _dbContext.SaveChangesAsync();

            Log.Information($"Tir with ID {updatedTir.Id} updated.");
            return existingTir;
        }
        public async Task<TirModel> InsertAsync(string model, int companyId, int driverId)
        {
            var tir = new TirModel()
            {
                Model = model,
                CompanyId = companyId,
                DriverId = driverId
            };

            _dbContext.Tirs.Add(tir);
            await _dbContext.SaveChangesAsync();
            Log.Information($"New Tir with ID {tir.Id} inserted at {DateTime.Now}");
            return tir;
        }
        public async Task<TirModel?> DeleteAsync(int id)
        {
            var tir = await _dbContext.Tirs.FindAsync(id);
            if (tir == null)
            {
                Log.Information($"Tir not found for ID {id}");
                return null;
            }
            _dbContext.Drivers.Where(x => x.TirId == id).ToList().ForEach(driver => driver.TirId = -1);
            _dbContext.Remove(tir);
            await _dbContext.SaveChangesAsync();
            Log.Information($"Tir with ID {id} successfully deleted.");
            return tir;
        }
        public async Task<TirModel?> GetTirByIdAsync(int id)
        {
            var tir = await _dbContext.Tirs.FirstOrDefaultAsync(x => x.Id == id);
            if (tir == null)
            {
                Log.Information($"Tir not found for ID {id}");
            }
            Log.Information($"Tir found for ID {id}");
            return tir;
        }
        public async Task<IEnumerable<TirModel>?> GetAllTirsAsync()
        {
            var tirs = await _dbContext.Tirs.ToListAsync();
            if (tirs == null || !tirs.Any())
            {
                Log.Information("No Tirs found in the database");
            }
            Log.Information("Tirs found in the database");
            return tirs;
        }
    }

}
