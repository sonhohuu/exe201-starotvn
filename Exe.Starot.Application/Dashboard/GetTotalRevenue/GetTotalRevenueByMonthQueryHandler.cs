using AutoMapper;
using Exe.Starot.Domain.Entities.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Exe.Starot.Application.Dashboard.GetTotalRevenue
{
    public class GetTotalRevenueByMonthQueryHandler : IRequestHandler<GetTotalRevenueByMonthQuery, List<RevenueByMonthDTO>>
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;

        public GetTotalRevenueByMonthQueryHandler(
            IOrderDetailRepository orderDetailRepository,
            IBookingRepository bookingRepository,
            IMapper mapper)
        {
            _orderDetailRepository = orderDetailRepository;
            _bookingRepository = bookingRepository;
            _mapper = mapper;
        }

        public async Task<List<RevenueByMonthDTO>> Handle(GetTotalRevenueByMonthQuery request, CancellationToken cancellationToken)
        {
            string dateFormat = "dd/MM/yyyy";

            // Set default year to the current year if not provided
            int year = request.Year > 0 ? request.Year : DateTime.Now.Year;

            // Fetch all order details and bookings
            var allOrderDetails = await _orderDetailRepository.FindAllAsync(cancellationToken);
            var allBookings = await _bookingRepository.FindAllAsync(cancellationToken);

            if ((allOrderDetails == null || !allOrderDetails.Any()) &&
                (allBookings == null || !allBookings.Any()))
            {
                return new List<RevenueByMonthDTO>();
            }

            // Process product sales (OrderDetails)
            var productRevenuesByMonth = allOrderDetails
                .Where(od =>
                {
                    // Parse the OrderDate based on the correct date format
                    if (!DateTime.TryParseExact(od.Order.OrderDate, dateFormat, null, System.Globalization.DateTimeStyles.None, out var date))
                    {
                        return false;
                    }

                    // Filter by the provided year or default year
                    return date.Year == year;
                })
                .GroupBy(od => DateTime.ParseExact(od.Order.OrderDate, dateFormat, null).Month)
                .Select(g => new
                {
                    Month = g.Key,
                    TotalRevenue = g.Sum(od => od.UnitPrice * od.Amount)
                })
                .ToList();

            // Process package bookings (BookingEntity)
            var packageRevenuesByMonth = allBookings
                .Where(b =>
                {
                    // Use the CreatedDate for filtering
                    if (b.CreatedDate == null)
                    {
                        return false;
                    }

                    // Filter by the provided year or default year
                    return b.CreatedDate.Value.Year == year;
                })
                .GroupBy(b => b.CreatedDate.Value.Month)
                .Select(g => new
                {
                    Month = g.Key,
                    TotalRevenue = g.Sum(b => b.Package.Price)
                })
                .ToList();

            // Combine the revenues from products and packages
            var totalRevenuesByMonth = productRevenuesByMonth
                .Union(packageRevenuesByMonth)
                .GroupBy(r => r.Month)
                .Select(g => new RevenueByMonthDTO
                {
                    Month = g.Key,
                    TotalRevenue = g.Sum(r => r.TotalRevenue)
                })
                .OrderBy(r => r.Month)
                .ToList();

            return totalRevenuesByMonth;
        }
    }
}
