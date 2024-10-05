using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.Booking.Commands.UpdateBooking
{
    public class UpdateBookingCommandValidator : AbstractValidator<UpdateBookingCommand>
    {
        public UpdateBookingCommandValidator()
        {
            RuleFor(x => x.BookingId)
                .NotEmpty().WithMessage("BookingId is required.");

            RuleFor(x => x.PackageId)
                .GreaterThan(0).WithMessage("PackageId must be greater than 0.");

            RuleFor(x => x.ReaderId)
                .NotEmpty().WithMessage("ReaderId is required.");

            RuleFor(x => x.StartDate)
                .Must(BeAValidStartDate).WithMessage("StartDate must be in the future.");

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Status is required.");
        }

        private bool BeAValidStartDate(DateTime? startDate)
        {
            return startDate > DateTime.UtcNow;
        }
    }


}
