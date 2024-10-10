using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.Dashboard
{
    public class PackageTotalPriceDTO
    {
        public int PackageId { get; set; }
        public string Name { get; set; }
        public decimal TotalPrice { get; set; }
        public int BookingCount { get; set; }
        public decimal PricePercentage {  get; set; }
    }
}
