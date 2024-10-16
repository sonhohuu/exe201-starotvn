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

namespace Exe.Starot.Application.Order.Filter
{
    public class FilterOrderQueryHandler : IRequestHandler<FilterOrderQuery, PagedResult<OrderDTO>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserRepository _userRepository;
        public FilterOrderQueryHandler(ApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService, IUserRepository userRepository)
        {
            _mapper = mapper;
            _context = context;
            _currentUserService = currentUserService;
            _userRepository = userRepository;
        }

        public async Task<PagedResult<OrderDTO>> Handle(FilterOrderQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Orders.AsQueryable();

            var user = await _userRepository.FindAsync(x => x.ID == _currentUserService.UserId && !x.DeletedDay.HasValue, cancellationToken);
            if (user != null && user.Role == "Customer")
            {
                query = query.Where(p => p.UserId == _currentUserService.UserId);
            }

            query = query.Where(p => !p.DeletedDay.HasValue);

            query = query.OrderByDescending(p => p.CreatedDate);

            if (!string.IsNullOrEmpty(request.UserName))
            {
                query = query.Where(p => (p.User.FirstName + " " +p.User.LastName).Contains(request.UserName));
            }

         

            if (!string.IsNullOrEmpty(request.Status))
            {
                query = query.Where(p => p.Status.Contains(request.Status));
            }

          

            if (request.SortOrder.HasValue)
            {
                // Apply sorting by total
                query = request.SortOrder.Value == true
                ? query.OrderByDescending(p => p.Total)
                : query.OrderBy(p => p.Total);
            }

            // Pagination
            var totalCount = await query.CountAsync(cancellationToken);
            var items = await query.Skip((request.PageNumber - 1) * request.PageSize)
                                   .Take(request.PageSize)
                                   .ToListAsync(cancellationToken);
            var pageCount = (int)Math.Ceiling((double)totalCount / request.PageSize);

            var dtos = _mapper.Map<List<OrderDTO>>(items);

            return new PagedResult<OrderDTO>
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

