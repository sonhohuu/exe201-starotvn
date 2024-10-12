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
    }

}
