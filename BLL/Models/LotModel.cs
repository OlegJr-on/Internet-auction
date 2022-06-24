using DAL.Entities;
using System;
using System.Collections.Generic;

namespace BLL.Models
{
    public class LotModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal CurrentPrice { get; set; }
        public decimal StartPrice { get; set; }
        public int MinRate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public LotStatus Status { get; set; }
        public int PhotoId { get; set; }
        public ICollection<int> OrderDetailsIds { get; set; }
    }
}
