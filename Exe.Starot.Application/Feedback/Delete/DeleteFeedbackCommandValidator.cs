using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Exe.Starot.Application.Feedback.Delete
{
    public class DeleteFeedbackCommandValidator : AbstractValidator<DeleteFeedbackCommand>
    {
        public DeleteFeedbackCommandValidator()
        {
            RuleFor(x => x.FeedbackId)
                .NotEmpty().WithMessage("FeedbackId is required.")
                .Length(1, 50).WithMessage("FeedbackId must be between 1 and 50 characters.");
        }
    }

}
