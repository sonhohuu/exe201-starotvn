using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using System;

namespace Exe.Starot.Application.Booking.Commands.CreateBooking
{
    public class CreateBookingCommandValidator : AbstractValidator<CreateBookingCommand>
    {
        public CreateBookingCommandValidator()
        {
            RuleFor(x => x.PackageId)
                .GreaterThan(0).WithMessage("PackageId must be greater than 0.");

            RuleFor(x => x.ReaderId)
                .NotEmpty().WithMessage("ReaderId is required.");

            RuleFor(x => x.StartDate)
                .Must(BeAValidStartDate).WithMessage("StartDate must be in the future.");
        }

        // Custom validator to check if StartDate is in the future
        private bool BeAValidStartDate(DateTime startDate)
        {
            return startDate > DateTime.UtcNow;
        }
    }

}
