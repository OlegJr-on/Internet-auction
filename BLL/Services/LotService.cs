using System;
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
    public class LotService : ILotService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LotService(IUnitOfWork ofWork, IMapper mapper)
        {
            _unitOfWork = ofWork;
            _mapper = mapper;
        }

        public async Task AddAsync(LotModel model)
        {
            if (model == null)
                throw new AuctionException("Model is null");

            if (model.StartPrice < 0)
                throw new AuctionException("Start price of lot can not be negative.");

            if (model.CurrentPrice < 0)
                throw new AuctionException("Current price of lot can not be negative.");

            if (model.StartDate < DateTime.Now || model.EndDate <= DateTime.Now ||
                (model.StartDate > model.EndDate))
                throw new AuctionException("Date period is not correct.");

            if (string.IsNullOrEmpty(model.Title))
                throw new AuctionException("Title of lot can not be empty or null.");


            var lot = _mapper.Map<Lot>(model);

            await _unitOfWork.LotRepository.AddAsync(lot);
        }

        public async Task AddPhotoAsync(PhotoModel photoModel)
        {
            if (photoModel == null)
                throw new AuctionException("Model is null");

            if (string.IsNullOrEmpty(photoModel.PhotoSrc))
                throw new AuctionException("Сell with photo can not be empty or null.");

            var photo = _mapper.Map<PhotoModel, Photo>(photoModel);
            await _unitOfWork.PhotoRepository.AddAsync(photo);

        }

        public async Task DeleteAsync(int modelId)
        {
            await _unitOfWork.LotRepository.DeleteByIdAsync(modelId);
        }

        public async Task<IEnumerable<LotModel>> GetAllAsync()
        {
            var lots = await _unitOfWork.LotRepository.GetAllWithDetailsAsync();

            return _mapper.Map<IEnumerable<LotModel>>(lots.ToList()); ;
        }

        public async Task<IEnumerable<PhotoModel>> GetAllPhotosAsync()
        {
            var photos = await _unitOfWork.PhotoRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<PhotoModel>>(photos);
        }

        public async Task<IEnumerable<LotModel>> GetByFilterAsync(FilterSearchModel filterSearch)
        {
            var lots = await _unitOfWork.LotRepository.GetAllWithDetailsAsync();

            var filtred = lots
                .Where(p => p.StartPrice >= (filterSearch.MinPrice ?? decimal.MinValue) &&
                       p.StartPrice <= (filterSearch.MaxPrice ?? decimal.MaxValue));

            if (filterSearch.BeginningPeriod != null && filterSearch.EndPeriod != null)
            {
                filtred = filtred.Where(x => x.StartDate >= filterSearch.BeginningPeriod && x.EndDate <= filterSearch.EndPeriod);
            }
            var model = new List<LotModel>();

            foreach (var lot in filtred)
            {
                model.Add(_mapper.Map<Lot, LotModel>(lot));
            }
            return model;
        }

        public async Task<LotModel> GetByIdAsync(int id)
        {
            var lot = await _unitOfWork.LotRepository.GetByIdWithDetailsAsync(id);

            return _mapper.Map<LotModel>(lot);
        }

        public async Task<IEnumerable<PhotoModel>> GetPhotosGroupByIdAsync(int id)
        {
            var lot = await _unitOfWork.LotRepository.GetByIdAsync(id);

            var photos = _unitOfWork.PhotoRepository.GetAllAsync().Result;

            var searchedImg = photos.Where(x => x.Id == lot.PhotoId).FirstOrDefault();

            return _mapper.Map<IEnumerable<PhotoModel>>(photos.Where(ph => ph.GroupOfPhoto == searchedImg.GroupOfPhoto));
        }

        public async Task RemovePhotoAsync(int photoId)
        {
            await _unitOfWork.PhotoRepository.DeleteByIdAsync(photoId);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(LotModel model)
        {
            if (model == null)
                throw new AuctionException("Model is null");

            if (model.StartPrice < 0)
                throw new AuctionException("Price of lot can not be negative.");

            if (string.IsNullOrEmpty(model.Title))
                throw new AuctionException("Title of lot can not be empty or null.");

            if (model.StartDate < DateTime.Now || model.EndDate <= DateTime.Now)
                throw new AuctionException("Date period is not correct.");

            var entity = await _unitOfWork.LotRepository.GetByIdWithDetailsAsync(model.Id);

            entity = _mapper.Map(model, entity);

            _unitOfWork.LotRepository.Update(entity);

            await _unitOfWork.SaveAsync();

        }

        public async Task UpdatePhotoAsync(PhotoModel photoModel)
        {
            if (photoModel == null)
                throw new AuctionException("Model is null");

            if (string.IsNullOrEmpty(photoModel.PhotoSrc))
                throw new AuctionException("Сell with photo can not be empty or null.");

            var entity = await _unitOfWork.PhotoRepository.GetByIdAsync(photoModel.Id);

            entity = _mapper.Map(photoModel, entity);

            _unitOfWork.PhotoRepository.Update(entity);

            await _unitOfWork.SaveAsync();
        }
    }
}
