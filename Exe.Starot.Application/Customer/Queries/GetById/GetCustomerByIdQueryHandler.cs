using AutoMapper;
using Exe.Starot.Application.Common.Interfaces;
using Exe.Starot.Domain.Common.Exceptions;
using Exe.Starot.Domain.Entities.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.Customer.Queries.GetById
{
    public class GetCustomerByIdQuery : IRequest<CustomerWithInfoDTO>
    {
    }

    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerWithInfoDTO>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public GetCustomerByIdQueryHandler(ICustomerRepository customerRepository, IMapper mapper, ICurrentUserService currentUserService)
        {
            _customerRepository = customerRepository;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public async Task<CustomerWithInfoDTO> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            if (userId == null)
            {
                throw new UnauthorizedException("User not login");
            }
            // Find the customer by Id
            var customer = await _customerRepository.FindAsync(c => c.User.ID == userId && c.DeletedDay == null, cancellationToken);

            if (customer == null)
            {
                throw new NotFoundException("Customer not found.");
            }
            var dto = customer.MapToCustomerWithInfoDTO(_mapper);

            return dto;
        }
    }

}
