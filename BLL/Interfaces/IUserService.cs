using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Models;

namespace BLL.Interfaces
{
    public interface IUserService : ICrud<UserModel>
    {
        Task<IEnumerable<UserModel>> GetUsersByLotIdAsync(int LotId);
    }
}
