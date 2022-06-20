using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public class Order : BaseEntity
    {
        public int UserId { get; set; }
        public DateTime OperationDate { get; set; }
        public User User { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
