using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.Data;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class LotRepository : ILotRepository
    {
        private AuctionDbContext DbContext { get; set; }

        public LotRepository(AuctionDbContext context)
        {
            this.DbContext = context;
        }

        public async Task<IEnumerable<Lot>> GetAllWithDetailsAsync()
        {
            return await DbContext.Lots
                            .Include(p => p.OrderDetails)
                            .Include(p => p.Photo)
                            .ToArrayAsync();
        }

        public async Task<Lot> GetByIdWithDetailsAsync(int id)
        {
            return await DbContext.Lots
                            .Include(p => p.OrderDetails)
                            .Include(p => p.Photo)
                            .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Lot>> GetAllAsync()
        {
            return await DbContext.Lots.ToListAsync();
        }

        public async Task<Lot> GetByIdAsync(int id)
        {
            return await DbContext.Lots.FindAsync(id);
        }

        public async Task AddAsync(Lot entity)
        {
            await DbContext.Lots.AddAsync(entity);
            DbContext.SaveChanges();
        }

        public void Delete(Lot entity)
        {
            DbContext.Lots.Remove(entity);
            DbContext.SaveChanges();
        }

        public Task DeleteByIdAsync(int id)
        {
            var lot = DbContext.Lots.FirstOrDefaultAsync(x => x.Id == id).Result;
            DbContext.Lots.Remove(lot);
            DbContext.SaveChanges();
            return Task.CompletedTask;
        }

        public void Update(Lot entity)
        {
            this.DbContext.Lots.Update(entity);
            DbContext.SaveChanges();
        }
    }
}
