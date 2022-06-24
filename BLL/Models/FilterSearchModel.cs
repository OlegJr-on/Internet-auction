using System;
using System.Collections.Generic;


namespace BLL.Models
{
    public class FilterSearchModel
    {
        public DateTime? BeginningPeriod { get; set; }
        public DateTime? EndPeriod { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }
}
