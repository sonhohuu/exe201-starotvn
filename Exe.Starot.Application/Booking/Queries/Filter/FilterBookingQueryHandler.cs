using AutoMapper;
using Exe.Starot.Application.Common.Pagination;
using Exe.Starot.Domain.Entities.Repositories;
using MediatR;
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
        public DateTime? StartDate { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class FilterBookingQueryHandler : IRequestHandler<FilterBookingQuery, PagedResult<BookingDTO>>
    {
        private readonly IBookingRepository _repository;
        private readonly IMapper _mapper;

        public FilterBookingQueryHandler(IBookingRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PagedResult<BookingDTO>> Handle(FilterBookingQuery request, CancellationToken cancellationToken)
        {
            // Fetch all bookings excluding soft-deleted ones
            var bookings = await _repository.FindAllAsync(b => b.DeletedDay == null, cancellationToken);

            // Apply filters based on provided query parameters
            if (!string.IsNullOrEmpty(request.CustomerId))
            {
                bookings = bookings.Where(b => b.CustomerId == request.CustomerId).ToList();
            }

            if (!string.IsNullOrEmpty(request.ReaderId))
            {
                bookings = bookings.Where(b => b.ReaderId == request.ReaderId).ToList();
            }

            if (!string.IsNullOrEmpty(request.Status))
            {
                bookings = bookings.Where(b => b.Status.ToLower().Contains(request.Status.ToLower())).ToList();
            }

            if (request.StartDate.HasValue)
            {
                bookings = bookings.Where(b => b.StartDate.Date == request.StartDate.Value.Date).ToList();
            }

            // If no bookings are found, return an empty list
            if (!bookings.Any())
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

            // Pagination logic
            var query = bookings.AsQueryable();
            var totalCount = query.Count();
            var items = query.Skip((request.PageNumber - 1) * request.PageSize)
                             .Take(request.PageSize)
                             .ToList();

            var pageCount = (int)Math.Ceiling((double)totalCount / request.PageSize);
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
