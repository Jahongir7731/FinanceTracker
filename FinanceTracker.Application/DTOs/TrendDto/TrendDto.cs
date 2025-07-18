using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTracker.Application.DTOs.TrendDto
{
    public class TrendDto
    {
        public decimal ThisMonth { get; set; }
        public decimal LastMonth { get; set; }
        public decimal Trend { get; set; }
    }

}
