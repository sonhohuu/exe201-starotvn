using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.Dashboard.GetTotalProduct
{
    public class GetProductSalesPercentageQuery : IRequest<List<ProductSalesPercentageDTO>>
    {
        public int Month { get; set; }
        public int Year { get; set; }

        public GetProductSalesPercentageQuery(int month, int year)
        {
            Month = month;
            Year = year;
        }
    }
}
