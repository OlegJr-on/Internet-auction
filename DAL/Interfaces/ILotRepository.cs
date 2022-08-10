using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface ILotRepository : IRepository<Lot>
    {
        Task<IEnumerable<Lot>> GetAllWithDetailsAsync();

        Task<Lot> GetByIdWithDetailsAsync(int id);

        Task<IEnumerable<object>> GetAllLotsWithPhoto();
    }
}
