using Microsoft.EntityFrameworkCore;
using SportStoreFreeman.Data;
using SportStoreFreeman.Models;

namespace SportStoreFreeman.Repositories.Db
{
    public class OrderRepository : IOrderRepository
    {
        private readonly SportStoreDbContext _sportStoreDbContext;

        public OrderRepository(SportStoreDbContext sportStoreDbContext)
        {
            _sportStoreDbContext = sportStoreDbContext;
        }
        public IQueryable<Order> Orders => _sportStoreDbContext.Orders
            .Include(o => o.Lines)
            .ThenInclude(l => l.Product);

        public void SaveOrder(Order order)
        {

            _sportStoreDbContext.AttachRange(order.Lines.Select(l => l.Product));
            if (order.OrderID == 0)
            {
                _sportStoreDbContext.Orders.Add(order);
            }
            _sportStoreDbContext.SaveChanges();
        }
    }
}
