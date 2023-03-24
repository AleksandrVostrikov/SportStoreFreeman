using SportStoreFreeman.Data;
using SportStoreFreeman.Models;

namespace SportStoreFreeman.Repositories.Db
{
    public class StoreRepository : IStoreRepository
    {
        private readonly SportStoreDbContext _sportStoreDbContext;

        public StoreRepository(SportStoreDbContext sportStoreDbContext)
        {
            _sportStoreDbContext = sportStoreDbContext;
        }

        public IQueryable<Product> Products => _sportStoreDbContext.Products;
    }
}
