using Exe.Starot.Application.Common.Interfaces;
using Exe.Starot.Domain.Common.Exceptions;
using Exe.Starot.Domain.Entities.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.Feedback.Update
{
    public record UpdateFeedbackCommand : IRequest<string>
    {
        public string FeedbackId { get; init; }
        public int? Rating { get; init; }
        public string? Comment { get; init; }
    }

    public class UpdateFeedbackCommandHandler : IRequestHandler<UpdateFeedbackCommand, string>
    {
        private readonly IFeedBackRepository _feedbackRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly ICustomerRepository _customerRepository;

        public UpdateFeedbackCommandHandler(IFeedBackRepository feedbackRepository, ICurrentUserService currentUserService, ICustomerRepository customerRepository)
        {
            _feedbackRepository = feedbackRepository;
            _currentUserService = currentUserService;
            _customerRepository = customerRepository;
        }

        public async Task<string> Handle(UpdateFeedbackCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            // Find the customer based on the current user ID
            var customer = await _customerRepository.FindAsync(c => c.UserId == userId && c.DeletedDay == null, cancellationToken);
            if (customer is null)
            {
                throw new UnauthorizedAccessException("User is not logged in.");
            }

            // Retrieve the existing feedback
            var feedback = await _feedbackRepository.FindAsync(x => x.ID == request.FeedbackId && !x.DeletedDay.HasValue,cancellationToken);
            if (feedback == null)
            {
                throw new NotFoundException($"Feedback with ID {request.FeedbackId} not found.");
            }

            // Ensure that the current user is the owner of the feedback
            if (feedback.CustomerId != customer.ID)
            {
                throw new UnauthorizedAccessException("User does not own this feedback.");
            }

            // Update feedback properties
            feedback.Rating = request.Rating ?? feedback.Rating;
            feedback.Comment = request.Comment ?? feedback.Comment;

            feedback.UpdatedBy = _currentUserService.UserId;
            feedback.LastUpdated = DateTime.UtcNow; // Optionally update date

            _feedbackRepository.Update(feedback);

            // Save changes to the repository
            if (await _feedbackRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0)
            {
                return "Feedback updated successfully!";
            }

            return "Failed to update feedback.";
        }
    }

}
