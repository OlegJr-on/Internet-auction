using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<User>> GetAllWithDetailsAsync();

        Task<User> GetByIdWithDetailsAsync(int id);
    }
}
