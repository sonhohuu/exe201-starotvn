using AutoMapper;
using Exe.Starot.Domain.Entities.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Exe.Starot.Application.Dashboard.GetTotalProduct
{
    public class GetProductSalesPercentageQueryHandler : IRequestHandler<GetProductSalesPercentageQuery, List<ProductSalesPercentageDTO>>
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IMapper _mapper;

        public GetProductSalesPercentageQueryHandler(IOrderDetailRepository orderDetailRepository, IMapper mapper)
        {
            _orderDetailRepository = orderDetailRepository;
            _mapper = mapper;
        }

        public async Task<List<ProductSalesPercentageDTO>> Handle(GetProductSalesPercentageQuery request, CancellationToken cancellationToken)
        {
            var allOrderDetails = await _orderDetailRepository
                                            .FindAllAsync(cancellationToken);

            if (allOrderDetails == null || !allOrderDetails.Any())
            {
               
                return new List<ProductSalesPercentageDTO>();
            }

           
            string dateFormat = "dd/MM/yyyy";

            var orderDetails = allOrderDetails
                .Where(od =>
                {
                    // Parse the OrderDate based on the correct date format
                    if (!DateTime.TryParseExact(od.Order.OrderDate, dateFormat, null, System.Globalization.DateTimeStyles.None, out var date))
                    {
                        return false;
                    }

                    // Filter by year and month
                    return date.Month == request.Month && date.Year == request.Year;
                })
                .ToList();

            if (!orderDetails.Any())
            {
 
                return new List<ProductSalesPercentageDTO>();
            }

            var totalProductsSold = orderDetails.Sum(od => od.Amount);
           

            if (totalProductsSold == 0)
            {
                return new List<ProductSalesPercentageDTO>();
            }
            var productSalesPercentageList = orderDetails
                .GroupBy(od => od.ProductId)
                .Select(g => new ProductSalesPercentageDTO
                {
                    ProductId = g.Key,
                    TotalSold = g.Sum(od => od.Amount),
                    SalesPercentage = Math.Round((g.Sum(od => od.Amount) / (double)totalProductsSold) * 100, 2)
                })
                .ToList();

            return productSalesPercentageList;
        }

    }
}

