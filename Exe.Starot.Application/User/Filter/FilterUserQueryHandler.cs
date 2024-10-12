using AutoMapper;
using Exe.Starot.Application.Common.Pagination;
using Exe.Starot.Application.Customer;
using Exe.Starot.Domain.Entities.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.User.Filter
{
    public class FilterUserQuery : IRequest<PagedResult<UserDTO>>
    {
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? Role { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class FilterUserQueryHandler : IRequestHandler<FilterUserQuery, PagedResult<UserDTO>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public FilterUserQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<UserDTO>> Handle(FilterUserQuery request, CancellationToken cancellationToken)
        {
            // Fetch all customers that are not soft-deleted
            var customers = await _userRepository.FindAllAsync(c => c.DeletedDay == null, cancellationToken);

            // Apply filters
            if (!string.IsNullOrEmpty(request.Email))
            {
                customers = customers.Where(c => c.FirstName.ToLower().Contains(request.Email.ToLower())).ToList();
            }

            if (!string.IsNullOrEmpty(request.Phone))
            {
                customers = customers.Where(c => c.Phone.ToLower().Contains(request.Phone.ToLower())).ToList();
            }

            if (request.DateOfBirth.HasValue)
            {
                customers = customers.Where(c => c.DateOfBirth == request.DateOfBirth?.ToString("dd/MM/yyyy")).ToList();
            }

            if (!string.IsNullOrEmpty(request.Gender))
            {
                customers = customers.Where(c => c.Gender == request.Gender).ToList();
            }

            if (!string.IsNullOrEmpty(request.Role))
            {
                customers = customers.Where(c => c.Role == request.Role).ToList();
            }

            // Apply order
            customers.OrderByDescending(c => c.CreatedDate);

            // If no records are found, return an empty paged result
            if (!customers.Any())
            {
                return new PagedResult<UserDTO>
                {
                    Data = new List<UserDTO>(),
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
            var dtos = items.MapToUserDTOList(_mapper);

            // Return paged result
            return new PagedResult<UserDTO>
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
