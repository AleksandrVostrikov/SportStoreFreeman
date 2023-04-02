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

        public void CreateProduct(Product p)
        {
            _sportStoreDbContext.Add(p);
            _sportStoreDbContext.SaveChanges();
        }

        public void DeleteProduct(Product p)
        {
            _sportStoreDbContext.Remove(p);
            _sportStoreDbContext.SaveChanges();
        }

        public void SaveProduct(Product p)
        {
            _sportStoreDbContext.SaveChanges();
        }
    }
}
