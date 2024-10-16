using AutoMapper;
using Exe.Starot.Application.Common.Interfaces;
using Exe.Starot.Application.Common.Pagination;
using Exe.Starot.Domain.Entities.Repositories;
using Exe.Starot.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.Booking.Queries.Filter
{
    public class FilterBookingQuery : IRequest<PagedResult<BookingDTO>>
    {
        public string? CustomerId { get; set; }
        public string? ReaderId { get; set; }
        public string? Status { get; set; }
        public bool? ViewMyBooking { get; set; }
        public string? Date { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class FilterBookingQueryHandler : IRequestHandler<FilterBookingQuery, PagedResult<BookingDTO>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public FilterBookingQueryHandler(IMapper mapper, ApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _mapper = mapper;
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<PagedResult<BookingDTO>> Handle(FilterBookingQuery request, CancellationToken cancellationToken)
        {
            // Start with the base query filtering out soft-deleted bookings
            var query = _context.Bookings.AsQueryable();

            // If ViewMyBooking is true, filter by current user's ID
            if (request.ViewMyBooking.HasValue && request.ViewMyBooking.Value)
            {
                query = query.Where(b => b.Customer.User.ID == _currentUserService.UserId);
            }

            // Apply other filters based on provided query parameters
            if (!string.IsNullOrEmpty(request.CustomerId))
            {
                query = query.Where(b => b.Customer.User.ID == request.CustomerId);
            }

            if (!string.IsNullOrEmpty(request.ReaderId))
            {
                query = query.Where(b => b.Reader.User.ID == request.ReaderId);
            }

            if (!string.IsNullOrEmpty(request.Status))
            {
                query = query.Where(b => b.Status.ToLower().Contains(request.Status.ToLower()));
            }

            if (!string.IsNullOrEmpty(request.Date))
            {
                query = query.Where(b => b.Date == request.Date);
            }

            query = query.Where(b => !b.DeletedDay.HasValue);

            query = query.OrderByDescending(b => b.CreatedDate);

            // Pagination: Get total count first
            var totalCount = await query.CountAsync(cancellationToken);

            // If no bookings are found, return an empty list
            if (totalCount == 0)
            {
                return new PagedResult<BookingDTO>
                {
                    Data = new List<BookingDTO>(),
                    TotalCount = 0,
                    PageCount = 0,
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize
                };
            }

            // Get paginated items
            var items = await query.Skip((request.PageNumber - 1) * request.PageSize)
                                   .Take(request.PageSize)
                                   .ToListAsync(cancellationToken);

            // Calculate total page count
            var pageCount = (int)Math.Ceiling((double)totalCount / request.PageSize);

            // Map entities to DTOs
            var dtos = _mapper.Map<List<BookingDTO>>(items);

            // Return paged result
            return new PagedResult<BookingDTO>
            {
                Data = dtos,
                TotalCount = totalCount,
                PageCount = pageCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }

    }

}
