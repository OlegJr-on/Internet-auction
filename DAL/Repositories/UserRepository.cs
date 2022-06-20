using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Data;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private AuctionDbContext DbContext { get; set; }

        public UserRepository(AuctionDbContext context)
        {
            this.DbContext = context;
        }

        public async Task AddAsync(User entity)
        {
            await DbContext.Users.AddAsync(entity);
            DbContext.SaveChanges();
        }

        public void Delete(User entity)
        {
            DbContext.Users.Remove(entity);
            DbContext.SaveChanges();
        }

        public Task DeleteByIdAsync(int id)
        {
            var user = DbContext.Users.FirstOrDefaultAsync(x => x.Id == id).Result;
            DbContext.Users.Remove(user);
            DbContext.SaveChanges();
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await DbContext.Users.ToListAsync();
        }

        public async Task<IEnumerable<User>> GetAllWithDetailsAsync()
        {
            return await DbContext.Users
                .Include(r => r.Orders)
                .ThenInclude(r => r.OrderDetails)
                .ToArrayAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await DbContext.Users.FindAsync(id);
        }

        public async Task<User> GetByIdWithDetailsAsync(int id)
        {
            return await DbContext.Users
                .Include(r => r.Orders)
                .ThenInclude(r => r.OrderDetails)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Update(User entity)
        {
            this.DbContext.Users.Update(entity);
            DbContext.SaveChanges();
        }
    }
}
