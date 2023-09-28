using ExerciceData.Models;

namespace GraphQL_Exercice.Repository
{
    public interface ITirRepository
    {
        Task<TirModel?> UpdateAsync(TirModel updatedTir);
        Task<TirModel> InsertAsync(string model, int companyId, int driverId);
        Task<TirModel?> DeleteAsync(int id);
        Task<TirModel?> GetTirByIdAsync(int id);
        Task<IEnumerable<TirModel>?> GetAllTirsAsync();
    }

}
