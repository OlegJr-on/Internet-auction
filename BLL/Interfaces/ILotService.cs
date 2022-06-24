using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Models;


namespace BLL.Interfaces
{
    public interface ILotService : ICrud<LotModel>
    {
        Task<IEnumerable<LotModel>> GetByFilterAsync(FilterSearchModel filterSearch);
        Task<IEnumerable<PhotoModel>> GetAllPhotosAsync();
        Task<IEnumerable<PhotoModel>> GetPhotosGroupByIdAsync(int id);
        Task AddPhotoAsync(PhotoModel photoModel);
        Task UpdatePhotoAsync(PhotoModel photoModel);
        Task RemovePhotoAsync(int photoId);
    }
}
