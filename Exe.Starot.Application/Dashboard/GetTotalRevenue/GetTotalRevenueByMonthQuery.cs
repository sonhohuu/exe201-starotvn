using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.Dashboard.GetTotalRevenue
{
    public class GetTotalRevenueByMonthQuery : IRequest<List<RevenueByMonthDTO>>
    {
        public int Year { get; set; }
    }
}
