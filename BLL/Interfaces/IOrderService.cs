using BLL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IOrderService : ICrud<OrderModel>
    {
        Task AddLotAsync(int lotId, int orderId);

        Task RemoveLotAsync(int lotId, int orderId);

        Task<IEnumerable<OrderDetailModel>> GetOrderDetailsAsync(int orderId);

        Task<IEnumerable<OrderModel>> GetOrdersByPeriodAsync(DateTime startDate, DateTime endDate);
    }
}
