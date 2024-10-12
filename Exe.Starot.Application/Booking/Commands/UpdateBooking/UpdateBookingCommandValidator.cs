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

            RuleFor(x => x.Status)
                .Must(x => string.IsNullOrEmpty(x) ||
                    x.Equals("Rejected", StringComparison.OrdinalIgnoreCase) ||
                    x.Equals("Approved", StringComparison.OrdinalIgnoreCase) ||
                    x.Equals("Pending", StringComparison.OrdinalIgnoreCase))
                .WithMessage("Status must be either 'Rejected', 'Approved', 'Pending' or left empty.");
        }

        private bool BeAValidStartDate(DateTime? startDate)
        {
            return startDate > DateTime.UtcNow;
        }
    }


}
