using SportStoreFreeman.Models;

namespace SportStoreFreeman.Repositories.Db
{
    public interface IStoreRepository
    {
        IQueryable<Product> Products { get; }
    }
}
