using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<IEnumerable<Order>> GetAllWithDetailsAsync();

        Task<Order> GetByIdWithDetailsAsync(int id);

        Task<IEnumerable<object>> GetAllUserOrdersById(int id);
    }
}
