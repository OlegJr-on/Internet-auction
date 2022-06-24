using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Models;
using DAL.Interfaces;
using AutoMapper;
using System.Linq;
using BLL.Interfaces;
using DAL.Entities;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork ofWork, IMapper mapper)
        {
            _unitOfWork = ofWork;
            _mapper = mapper;
        }

        public async Task AddAsync(UserModel model)
        {
            if (model == null)
                throw new AuctionException("Model is null");

            if (model.Name == null || model.Name == "")
                throw new AuctionException("Name is empty or null");

            if (model.Surname == null || model.Surname == "")
                throw new AuctionException("Surnmame is empty or null");

            if (model.Email == null || model.Email == "")
                throw new AuctionException("Email is not correct");

            if (model.Password == null || model.Password == "")
                throw new AuctionException("Password is empty or null");

            if (model.PhoneNumber == null || model.PhoneNumber == "")
                throw new AuctionException("Phone number is empty or null");

            var user = _mapper.Map<User>(model);

            await _unitOfWork.UserRepository.AddAsync(user);

        }

        public async Task DeleteAsync(int modelId)
        {
            await _unitOfWork.UserRepository.DeleteByIdAsync(modelId);
        }

        public async Task<IEnumerable<UserModel>> GetAllAsync()
        {
            var users = await _unitOfWork.UserRepository.GetAllWithDetailsAsync();

            return _mapper.Map<IEnumerable<UserModel>>(users.ToList());
        }

        public async Task<UserModel> GetByIdAsync(int id)
        {
            var user = await _unitOfWork.UserRepository.GetByIdWithDetailsAsync(id);

            return _mapper.Map<UserModel>(user);
        }

        public async Task<IEnumerable<UserModel>> GetUsersByLotIdAsync(int LotId)
        {
            var users = await _unitOfWork.UserRepository.GetAllWithDetailsAsync();

            return _mapper.Map<IEnumerable<UserModel>>(users.Where(u => u.Orders
                          .SelectMany(r => r.OrderDetails).Any(rd => rd.LotId == LotId)));
        }

        public async Task UpdateAsync(UserModel model)
        {
            if (model == null)
                throw new AuctionException("Model is null");

            if (model.Name == null || model.Name == "")
                throw new AuctionException("Name is empty or null");

            if (model.Surname == null || model.Surname == "")
                throw new AuctionException("Surnmame is empty or null");

            if (model.Email == null || model.Email == "" || !model.Email.Contains("@gmail.com"))
                throw new AuctionException("Email is not correct");

            if (model.Password == null || model.Password == "")
                throw new AuctionException("Password is empty or null");

            if (model.PhoneNumber == null || model.PhoneNumber == "")
                throw new AuctionException("Phone number is empty or null");

            var entity = await _unitOfWork.UserRepository.GetByIdWithDetailsAsync(model.Id);

            entity = _mapper.Map(model, entity);

            _unitOfWork.UserRepository.Update(entity);

            await _unitOfWork.SaveAsync();
        }
    }
}
