using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class OrderDetail : BaseEntity
    {
        public int OrderId { get; set; }
        public int LotId { get; set; }
        public LotDetailStatus Status { get; set; }
        public Order Order { get; set; }
        public Lot Lot { get; set; }
    }
}
