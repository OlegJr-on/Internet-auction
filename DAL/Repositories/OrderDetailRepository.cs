using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Data;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private AuctionDbContext DbContext { get; set; }

        public OrderDetailRepository(AuctionDbContext context)
        {
            this.DbContext = context;
        }

        public async Task<IEnumerable<OrderDetail>> GetAllWithDetailsAsync()
        {
            return await DbContext.OrderDetails
              .Include(r => r.Order)
              .ThenInclude(r => r.User)
              .Include(p => p.Lot)
              .ThenInclude(p => p.Photo)
              .ToArrayAsync();
        }

        public async Task<IEnumerable<OrderDetail>> GetAllAsync()
        {
            return await DbContext.OrderDetails.ToListAsync();
        }

        public async Task<OrderDetail> GetByIdAsync(int id)
        {
            return await DbContext.OrderDetails.FindAsync(id);
        }

        public async Task AddAsync(OrderDetail entity)
        {
            await DbContext.OrderDetails.AddAsync(entity);
            DbContext.SaveChanges();
        }

        public void Delete(OrderDetail entity)
        {
            DbContext.OrderDetails.Remove(entity);
            DbContext.SaveChanges();
        }

        public Task DeleteByIdAsync(int id)
        {
            var od = DbContext.OrderDetails.FirstOrDefaultAsync(x => x.Id == id).Result;
            DbContext.OrderDetails.Remove(od);
            DbContext.SaveChanges();
            return Task.CompletedTask;
        }

        public void Update(OrderDetail entity)
        {
            this.DbContext.OrderDetails.Update(entity);
            DbContext.SaveChanges();
        }
    }
}
