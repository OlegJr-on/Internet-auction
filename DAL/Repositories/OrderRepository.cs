using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Data;
using DAL.Entities;
using System.Linq;
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

        public async Task<IEnumerable<object>> GetAllUserOrdersById(int id)
        {
            return await DbContext.Orders
                .Join(DbContext.OrderDetails,
                orders => orders.Id,
                od => od.OrderId,
                (orders, od) => new { orders.OperationDate, OrderId = orders.Id, od.LotId, orders.UserId })
                    .Join(DbContext.Lots,
                    orders => orders.LotId,
                    lot => lot.Id,
                    (orders, lot) => new { orders.OperationDate, orders.OrderId, lot.Id, lot.Title, lot.EndDate, 
                                           lot.CurrentPrice, lot.PhotoId, orders.UserId })
                        .Join(DbContext.Photos,
                        PastRequest => PastRequest.PhotoId,
                        photo => photo.Id,
                        (pastRequest, photo) =>
                                            new
                                            {
                                                pastRequest.Id, // lot id
                                                pastRequest.Title, // lot title
                                                pastRequest.EndDate,// lot end date
                                                pastRequest.CurrentPrice, // current price of lot
                                                pastRequest.OperationDate, // operation date of order
                                                pastRequest.OrderId, // order id
                                                pastRequest.UserId, // user id from orders
                                                photo.PhotoSrc       // photo
                                            })
                                            .Where(x => x.UserId == id)
                                            .ToListAsync();
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
