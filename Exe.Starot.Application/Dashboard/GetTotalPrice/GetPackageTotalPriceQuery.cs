using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.Dashboard.GetTotalPrice
{
    public class GetPackageTotalPriceQuery : IRequest<List<PackageTotalPriceDTO>>
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public GetPackageTotalPriceQuery(int month, int year)
        {
            Month = month;
            Year = year;
        }
    }

}
