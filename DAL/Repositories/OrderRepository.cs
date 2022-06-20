using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Data;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private AuctionDbContext DbContext { get; set; }

        public OrderRepository(AuctionDbContext context)
        {
            this.DbContext = context;
        }

        public async Task<IEnumerable<Order>> GetAllWithDetailsAsync()
        {
            return await DbContext.Orders
                .Include(p => p.User)
                .Include(p => p.OrderDetails)
                .ThenInclude(r => r.Lot)
                .ThenInclude(p => p.Photo)
                .ToArrayAsync();
        }

        public async Task<Order> GetByIdWithDetailsAsync(int id)
        {
            return await DbContext.Orders
                .Include(p => p.User)
                .Include(p => p.OrderDetails)
                .ThenInclude(r => r.Lot)
                .ThenInclude(p => p.Photo)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await DbContext.Orders.ToListAsync();
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            return await DbContext.Orders.FindAsync(id);
        }

        public async Task AddAsync(Order entity)
        {
            await DbContext.Orders.AddAsync(entity);
            DbContext.SaveChanges();
        }

        public void Delete(Order entity)
        {
            DbContext.Orders.Remove(entity);
            DbContext.SaveChanges();
        }

        public Task DeleteByIdAsync(int id)
        {
            var order = DbContext.Orders.FirstOrDefaultAsync(x => x.Id == id).Result;
            DbContext.Orders.Remove(order);
            DbContext.SaveChanges();
            return Task.CompletedTask;
        }

        public void Update(Order entity)
        {
            this.DbContext.Orders.Update(entity);
            DbContext.SaveChanges();
        }
    }
}
