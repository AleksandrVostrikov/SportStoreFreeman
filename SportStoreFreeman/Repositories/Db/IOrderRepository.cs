using SportStoreFreeman.Models;

namespace SportStoreFreeman.Repositories.Db
{
    public interface IOrderRepository
    {
        IQueryable<Order> Orders { get; }
        void SaveOrder (Order order);
    }
}
