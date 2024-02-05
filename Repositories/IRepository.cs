using Project1.Models.Domain;

namespace Project1.Repositories
{
    public interface IRepository
    {
        Task<List<Regions>> GetAllAsync();
        Task<Regions?> GetAsync(Guid Id);

        Task<Regions> CreateAsync(Regions region);

        Task<Regions?> UpdateAsync(Guid Id, Regions region);

        Task<Regions?> DeleteAsync(Guid Id);
    }
}
