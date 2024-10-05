using AutoMapper;
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
    public class GetCustomerByIdQuery : IRequest<CustomerDTO>
    {
        [Required]
        public required string Id { get; set; }
    }

    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerDTO>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetCustomerByIdQueryHandler(ICustomerRepository customerRepository, IUserRepository userRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<CustomerDTO> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            // Find the customer by Id
            var customer = await _customerRepository.FindAsync(c => c.User.ID == request.Id && c.DeletedDay == null, cancellationToken);

            if (customer == null)
            {
                throw new NotFoundException("Customer not found.");
            }
            var dto = customer.MapToCustomerDTO(_mapper);

            return dto;
        }
    }

}
