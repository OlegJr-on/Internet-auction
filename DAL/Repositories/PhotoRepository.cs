using DAL.Entities;
using DAL.Interfaces;
using DAL.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DAL.Repositories
{
    public class PhotoRepository : IPhotoRepository
    {
        private AuctionDbContext DbContext { get; set; }
        public PhotoRepository(AuctionDbContext context)
        {
            this.DbContext = context;
        }

        public async Task<IEnumerable<Photo>> GetAllAsync()
        {
            return await DbContext.Photos.ToListAsync();
        }

        public async Task<Photo> GetByIdAsync(int id)
        {
            return await DbContext.Photos.FindAsync(id);
        }

        public async Task AddAsync(Photo entity)
        {
            await DbContext.Photos.AddAsync(entity);
            DbContext.SaveChanges();
        }

        public void Delete(Photo entity)
        {
            DbContext.Photos.Remove(entity);
            DbContext.SaveChanges();
        }

        public Task DeleteByIdAsync(int id)
        {
            var photo = DbContext.Photos.FirstOrDefaultAsync(x => x.Id == id).Result;
            DbContext.Photos.Remove(photo);
            DbContext.SaveChanges();
            return Task.CompletedTask;
        }

        public void Update(Photo entity)
        {
            this.DbContext.Photos.Update(entity);
            DbContext.SaveChanges();
        }

        public async Task<IEnumerable<Photo>> GetByGroupNumber(int id)
        {
            var listPhoto = await DbContext.Photos.ToListAsync();

            var photos = new List<Photo>();

            foreach (var item in listPhoto)
            {
                if (item.GroupOfPhoto == id)
                    photos.Add(item);
            }

            return photos;
        }
    }
}
