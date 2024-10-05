using Exe.Starot.Application.Common.Interfaces;
using Exe.Starot.Domain.Common.Exceptions;
using Exe.Starot.Domain.Entities.Base;
using Exe.Starot.Domain.Entities.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.Feedback.Create
{
    public class CreateFeedbackCommandHandler : IRequestHandler<CreateFeedbackCommand, string>
    {
        private readonly IFeedBackRepository _feedbackRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly ICustomerRepository _customerRepository;

        public CreateFeedbackCommandHandler(IFeedBackRepository feedbackRepository, ICurrentUserService currentUserService, ICustomerRepository customerRepository)
        {
            _feedbackRepository = feedbackRepository;
            _currentUserService = currentUserService;
            _customerRepository = customerRepository;
        }


        public async Task<string> Handle(CreateFeedbackCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            // Find the customer based on the current user ID
            var customer = await _customerRepository.FindAsync(c => c.UserId == userId && c.DeletedDay == null, cancellationToken);

            if (customer is null)
            {
                throw new UnauthorizedAccessException("User is not logged in.");
            }

            var customerId = customer.ID;


            var feedback = new FeedbackEntity
            {
                CustomerId = customerId,
                ReaderId = request.ReaderId,
                Rating = request.Rating,
                Comment = request.Comment,
                Date = DateTime.UtcNow
            };

            _feedbackRepository.Add(feedback);

            if (await _feedbackRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0)
            {
                return "Feedback Created Successfully!";
            }
            else
            {
                return "Failed to create feedback.";
            }
        }
    }
}
