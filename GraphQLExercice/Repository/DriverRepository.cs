using ExerciceData.Context;
using ExerciceData.Models;
using GraphQL_Exercice.GraphQLResolvers;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace GraphQL_Exercice.Resolvers
{
    public class DriverRepository
    {
        private readonly AppDbContext _dbContext;
        public DriverRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<DriverModel?> GetDriverByIdAsync(int id)
        {
            var driver = await _dbContext.Drivers.FirstOrDefaultAsync(x => x.Id == id);
            if (driver == null)
            {
                Log.Information($"Driver not found for ID {id}");
            }
            Log.Information($"Driver found for ID {id}");
            return driver;
        }
        public async Task<DriverModel?> GetDriverByTirIdAsync(int id)
        {
            var myDriver = await _dbContext.Tirs
                                .Where(tir => tir.Id == id)
                                .Select(tir => tir.Driver)
                                .FirstOrDefaultAsync();
            if (myDriver == null)
            {
                Log.Information($"Driver not found for ID {id}");
            }
            Log.Information($"Driver found for ID {id}");
            return myDriver;
        }
        public async Task<IEnumerable<DriverModel>?> GetAllDriversAsync()
        {
            var drivers = await _dbContext.Drivers.ToListAsync();
            if (drivers == null || !drivers.Any())
            {
                Log.Information("No Drivers found in the database");
            }
            Log.Information("Drivers found in the database");
            return drivers;
        }
        public async Task<DriverModel?> UpdateAsync(DriverModel updatedDriver)
        {
            var existingDriver = await _dbContext.Drivers.FindAsync(updatedDriver.Id);
            if (existingDriver == null)
            {
                Log.Information($"Driver with ID {updatedDriver.Id} not found.");
                return null;
            }
            _dbContext.Entry(existingDriver).CurrentValues.SetValues(updatedDriver);
            await _dbContext.SaveChangesAsync();
            Log.Information($"Driver with ID {updatedDriver.Id} updated");
            return existingDriver;
        }
        public async Task<DriverModel> InsertAsync(string name, string surname, int age, int tirId, int companyId)
        {
            var driver = new DriverModel()
            {
                Name = name,
                Surname = surname,
                Age = age,
                TirId = tirId,
                CompanyId = companyId
            };
            _dbContext.Drivers.Add(driver);
            await _dbContext.SaveChangesAsync();
            Log.Information($"New Driver with ID {driver.Id} inserted at {DateTime.Now}");
            return driver;
        }
        public async Task<DriverModel?> DeleteAsync(int id)
        {
            var driver = await _dbContext.Drivers.FindAsync(id);
            if (driver == null)
            {
                Log.Information($"Driver not found for ID {id}");
                return null;
            }
            _dbContext.Tirs.Where(t => t.DriverId == id).ToList().ForEach(tir => tir.DriverId = -1);
            _dbContext.Remove(driver);
            await _dbContext.SaveChangesAsync();
            Log.Information($"Driver with ID {id} successfully deleted.");
            return driver;
        }
    }
}