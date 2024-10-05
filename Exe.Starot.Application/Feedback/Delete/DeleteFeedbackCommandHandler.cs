using Exe.Starot.Application.Common.Interfaces;
using Exe.Starot.Domain.Common.Exceptions;
using Exe.Starot.Domain.Entities.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.Feedback.Delete
{
    public record DeleteFeedbackCommand : IRequest<string>
    {
        public string FeedbackId { get; init; }
    }

    public class DeleteFeedbackCommandHandler : IRequestHandler<DeleteFeedbackCommand, string>
    {
        private readonly IFeedBackRepository _feedbackRepository;
        private readonly ICurrentUserService _currentUserService;

        public DeleteFeedbackCommandHandler(IFeedBackRepository feedbackRepository, ICurrentUserService currentUserService)
        {
            _feedbackRepository = feedbackRepository;
            _currentUserService = currentUserService;
        }

        public async Task<string> Handle(DeleteFeedbackCommand request, CancellationToken cancellationToken)
        {
            // Retrieve the existing feedback
            var feedback = await _feedbackRepository.FindAsync(x => x.ID == request.FeedbackId && !x.DeletedDay.HasValue, cancellationToken);
            if (feedback == null)
            {
                throw new NotFoundException($"Feedback with ID {request.FeedbackId} not found.");
            }

            feedback.DeletedBy = _currentUserService.UserId;
            feedback.DeletedDay = DateTime.UtcNow;

            // Delete the feedback
            _feedbackRepository.Remove(feedback);

            // Save changes to the repository
            if (await _feedbackRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0)
            {
                return "Feedback deleted successfully!";
            }

            return "Failed to delete feedback.";
        }
    }

}
