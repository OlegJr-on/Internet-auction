using DAL.Entities;


namespace BLL.Models
{
    public class OrderDetailModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int LotId { get; set; }
        public LotDetailStatus Status { get; set; }
    }
}
