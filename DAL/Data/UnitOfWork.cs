using DAL.Interfaces;
using DAL.Repositories;
using System.Threading.Tasks;

namespace DAL.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private AuctionDbContext DbContext { get; set; }

        public UnitOfWork(AuctionDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        public UnitOfWork()
        {
            this.DbContext = new AuctionDbContext();
        }

        public IUserRepository UserRepository => new UserRepository(this.DbContext);

        public ILotRepository LotRepository => new LotRepository(this.DbContext);

        public IPhotoRepository PhotoRepository => new PhotoRepository(this.DbContext);

        public IOrderRepository OrderRepository => new OrderRepository(this.DbContext);

        public IOrderDetailRepository OrderDetailRepository => new OrderDetailRepository(this.DbContext);

        public async Task SaveAsync()
                    => await this.DbContext.SaveChangesAsync();
    }
}
