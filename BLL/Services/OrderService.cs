using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Models;
using DAL.Interfaces;
using AutoMapper;
using System.Linq;
using BLL.Interfaces;
using DAL.Entities;
using System;

namespace BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork ofWork, IMapper mapper)
        {
            _unitOfWork = ofWork;
            _mapper = mapper;
        }

        public async Task AddAsync(OrderModel model)
        {
            if (model == null)
                throw new AuctionException("Model is null");

            var receipt = _mapper.Map<OrderModel, Order>(model);

            await _unitOfWork.OrderRepository.AddAsync(receipt);
        }

        public async Task AddLotAsync(int lotId, int orderId)
        {
            var order = await _unitOfWork.OrderRepository.GetByIdWithDetailsAsync(orderId);

            if (order == null)
            {
                throw new AuctionException($"Order with Id = {orderId} does not exists");
            }
            else
            {
                var product = await _unitOfWork.LotRepository.GetByIdAsync(lotId);

                if (product == null)
                {
                    throw new AuctionException($"Lot with Id = {lotId} does not exists");
                }
                var entity = new OrderDetail()
                {
                    LotId = lotId,
                    OrderId = orderId,
                    Status = LotDetailStatus.Bid_placed,
                };

                await _unitOfWork.OrderDetailRepository.AddAsync(entity);
            }

        }

        public async Task DeleteAsync(int modelId)
        {
            await _unitOfWork.OrderRepository.DeleteByIdAsync(modelId);
        }

        public async Task<IEnumerable<OrderModel>> GetAllAsync()
        {
            var orders = await _unitOfWork.OrderRepository.GetAllWithDetailsAsync();

            return _mapper.Map<IEnumerable<OrderModel>>(orders.ToList());
        }

        public async Task<OrderModel> GetByIdAsync(int id)
        {
            var order = await _unitOfWork.OrderRepository.GetByIdWithDetailsAsync(id);

            return _mapper.Map<OrderModel>(order); ;
        }

        public async Task<IEnumerable<OrderDetailModel>> GetOrderDetailsAsync(int orderId)
        {
            var order = await _unitOfWork.OrderRepository.GetByIdWithDetailsAsync(orderId);

            return _mapper.Map<IEnumerable<OrderDetailModel>>(order.OrderDetails);
        }

        public async Task<IEnumerable<OrderModel>> GetOrdersByPeriodAsync(DateTime startDate, DateTime endDate)
        {
            var orders = await _unitOfWork.OrderRepository.GetAllWithDetailsAsync();

            return _mapper.Map<IEnumerable<OrderModel>>(orders.Where(r => r.OperationDate >= startDate
                                                                            && r.OperationDate <= endDate));
        }

        public async Task RemoveLotAsync(int lotId, int orderId)
        {
            var order = await _unitOfWork.OrderRepository.GetByIdWithDetailsAsync(orderId);
            var orderDetail = order.OrderDetails.First(r => r.LotId == lotId);

            _unitOfWork.OrderDetailRepository.Delete(orderDetail);

            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(OrderModel model)
        {
            if (model == null)
                throw new AuctionException("Model is null");

            var entity = await _unitOfWork.OrderRepository.GetByIdAsync(model.Id);

            entity = _mapper.Map(model, entity);

            _unitOfWork.OrderRepository.Update(entity);

            await _unitOfWork.SaveAsync();
        }
    }
}
