using ExerciceData.Models;

namespace GraphQL_Exercice.Repository
{
    public interface IDriverRepository
    {
        Task<DriverModel?> GetDriverByIdAsync(int id);
        Task<DriverModel?> GetDriverByTirIdAsync(int id);
        Task<IEnumerable<DriverModel>?> GetAllDriversAsync();
        Task<DriverModel?> UpdateAsync(DriverModel updatedDriver);
        Task<DriverModel> InsertAsync(string name, string surname, int age, int tirId, int companyId);
        Task<DriverModel?> DeleteAsync(int id);
    }

}
