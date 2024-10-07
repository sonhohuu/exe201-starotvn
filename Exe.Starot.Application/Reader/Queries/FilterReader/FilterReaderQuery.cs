using AutoMapper;
using Exe.Starot.Application.Common.Pagination;
using Exe.Starot.Domain.Entities.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.Reader.Queries.FilterReader
{
    public class FilterReaderQuery : IRequest<PagedResult<ReaderDTO>>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Phone { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Expertise { get; set; }
        public string? Experience { get; init; }
        public decimal? MinRating { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class FilterReaderQueryHandler : IRequestHandler<FilterReaderQuery, PagedResult<ReaderDTO>>
    {
        private readonly IReaderRepository _readerRepository;
        private readonly IMapper _mapper;

        public FilterReaderQueryHandler(IReaderRepository readerRepository, IMapper mapper)
        {
            _readerRepository = readerRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<ReaderDTO>> Handle(FilterReaderQuery request, CancellationToken cancellationToken)
        {
            // Fetch all readers that are not soft-deleted
            var readers = await _readerRepository.FindAllAsync(r => r.DeletedDay == null, cancellationToken);

            // Apply filters
            if (!string.IsNullOrEmpty(request.FirstName))
            {
                readers = readers.Where(r => r.User.FirstName.ToLower().Contains(request.FirstName.ToLower())).ToList();
            }

            if (!string.IsNullOrEmpty(request.LastName))
            {
                readers = readers.Where(r => r.User.LastName.ToLower().Contains(request.LastName.ToLower())).ToList();
            }

            if (!string.IsNullOrEmpty(request.Phone))
            {
                readers = readers.Where(r => r.User.Phone.ToLower().Contains(request.Phone.ToLower())).ToList();
            }

            if (request.DateOfBirth.HasValue)
            {
                readers = readers.Where(r => r.User.DateOfBirth == request.DateOfBirth).ToList();
            }

            if (!string.IsNullOrEmpty(request.Expertise))
            {
                readers = readers.Where(r => r.Expertise.ToLower().Contains(request.Expertise.ToLower())).ToList();
            }

            if (!string.IsNullOrEmpty(request.Experience))
            {
                readers = readers.Where(r => r.Experience.ToLower().Contains(request.Experience.ToLower())).ToList();
            }

            if (request.MinRating.HasValue)
            {
                readers = readers.Where(r => r.Rating >= request.MinRating.Value).ToList();
            }

            // If no records are found, return an empty paged result
            if (!readers.Any())
            {
                return new PagedResult<ReaderDTO>
                {
                    Data = new List<ReaderDTO>(),
                    TotalCount = 0,
                    PageCount = 0,
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize
                };
            }

            // Handle pagination
            var query = readers.AsQueryable();
            var totalCount = query.Count();
            var items = query.Skip((request.PageNumber - 1) * request.PageSize)
                             .Take(request.PageSize)
                             .ToList();

            var pageCount = (int)Math.Ceiling((double)totalCount / request.PageSize);
            var dtos = items.MapToReaderDTOList(_mapper);  // Assuming this method exists to map to DTO

            // Return paged result
            return new PagedResult<ReaderDTO>
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
