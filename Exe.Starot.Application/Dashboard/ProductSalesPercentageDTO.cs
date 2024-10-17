using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.Dashboard
{
    public class ProductSalesPercentageDTO
    {
        public string ProductId { get; set; }
        public int TotalSold { get; set; }
        public double SalesPercentage { get; set; }

    }
}
