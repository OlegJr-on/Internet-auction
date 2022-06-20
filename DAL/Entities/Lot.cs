using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public class Lot : BaseEntity
    {
        public string Title { get; set; }
        public decimal CurrentPrice { get; set; }
        public decimal StartPrice { get; set; }
        public int MinRate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public LotStatus Status { get; set; }
        public int PhotoId { get; set; }
        public Photo Photo { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
