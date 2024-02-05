using Microsoft.EntityFrameworkCore;
using Project1.Data;
using Project1.Models.Domain;
using Project1.Models.DTO;

namespace Project1.Repositories
{
    public class SQLRepository : IRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SQLRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<Regions>> GetAllAsync()
        {
            return await dbContext.Regions.ToListAsync();
        }

        public async Task<Regions?> GetAsync(Guid Id)
        {
            return await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<Regions> CreateAsync(Regions regionDomainModel)
        {
            await dbContext.Regions.AddAsync(regionDomainModel);
            await dbContext.SaveChangesAsync();
            return regionDomainModel;
        }

        public async Task<Regions?> UpdateAsync(Guid Id, Regions region)
        {
            var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x=> x.Id == Id);

            if (regionDomainModel == null) { return null; }

            regionDomainModel.Code = region.Code;
            regionDomainModel.RegionImageUrl = region.RegionImageUrl;
            regionDomainModel.Name = region.Name;


            await dbContext.SaveChangesAsync();

            return regionDomainModel;
        }

        public async Task<Regions?> DeleteAsync(Guid Id)
        {
            var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == Id);
            if (regionDomainModel == null) { return null; }

            dbContext.Regions.Remove(regionDomainModel);
            await dbContext.SaveChangesAsync();
            
            return regionDomainModel;
        }
    }
}
