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
            int month = request.Month > 0 ? request.Month : DateTime.Now.Month;
            int year = request.Year > 0 ? request.Year : DateTime.Now.Year;

            var allOrderDetails = await _orderDetailRepository.FindAllAsync(cancellationToken);

            if (allOrderDetails == null || !allOrderDetails.Any())
            {
                return new List<ProductSalesPercentageDTO>();
            }

            // Filter by year and month
            var orderDetails = allOrderDetails
                .Where(od =>
                {
                    if (!DateTime.TryParseExact(od.Order.OrderDate, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out var date))
                    {
                        return false;
                    }

                    return date.Month == month && date.Year == year;
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

            // Prepare the result list with percentage calculation
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

