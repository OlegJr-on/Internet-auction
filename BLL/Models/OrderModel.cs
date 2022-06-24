using System;
using System.Collections.Generic;

namespace BLL.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime OperationDate { get; set; }
        public ICollection<int> OrderDetailsIds { get; set; }
    }
}
