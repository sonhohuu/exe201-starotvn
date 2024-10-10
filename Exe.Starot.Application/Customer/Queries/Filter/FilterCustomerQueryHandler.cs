using AutoMapper;
using Exe.Starot.Application.Common.Pagination;
using Exe.Starot.Domain.Entities.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.Customer.Queries.Filter
{
    public class FilterCustomerQuery : IRequest<PagedResult<CustomerDTO>>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Phone { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class FilterCustomerQueryHandler : IRequestHandler<FilterCustomerQuery, PagedResult<CustomerDTO>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public FilterCustomerQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<CustomerDTO>> Handle(FilterCustomerQuery request, CancellationToken cancellationToken)
        {
            // Fetch all customers that are not soft-deleted
            var customers = await _customerRepository.FindAllAsync(c => c.DeletedDay == null, cancellationToken);

            // Apply filters
            if (!string.IsNullOrEmpty(request.FirstName))
            {
                customers = customers.Where(c => c.User.FirstName.ToLower().Contains(request.FirstName.ToLower())).ToList();
            }

            if (!string.IsNullOrEmpty(request.LastName))
            {
                customers = customers.Where(c => c.User.LastName.ToLower().Contains(request.LastName.ToLower())).ToList();
            }

            if (!string.IsNullOrEmpty(request.Phone))
            {
                customers = customers.Where(c => c.User.Phone.ToLower().Contains(request.Phone.ToLower())).ToList();
            }

            if (request.DateOfBirth.HasValue)
            {
                customers = customers.Where(c => c.User.DateOfBirth == request.DateOfBirth?.ToString("dd/MM/yyyy")).ToList();
            }

            // If no records are found, return an empty paged result
            if (!customers.Any())
            {
                return new PagedResult<CustomerDTO>
                {
                    Data = new List<CustomerDTO>(),
                    TotalCount = 0,
                    PageCount = 0,
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize
                };
            }

            // Handle pagination
            var query = customers.AsQueryable();
            var totalCount = query.Count();
            var items = query.Skip((request.PageNumber - 1) * request.PageSize)
                             .Take(request.PageSize)
                             .ToList();

            var pageCount = (int)Math.Ceiling((double)totalCount / request.PageSize);
            var dtos = items.MapToCustomerDTOList(_mapper);

            // Return paged result
            return new PagedResult<CustomerDTO>
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
