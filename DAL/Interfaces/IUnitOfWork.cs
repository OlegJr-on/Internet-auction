using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }

        ILotRepository LotRepository { get; }

        IPhotoRepository PhotoRepository { get; }

        IOrderRepository OrderRepository { get; }

        IOrderDetailRepository OrderDetailRepository { get; }

        Task SaveAsync();
    }
}
