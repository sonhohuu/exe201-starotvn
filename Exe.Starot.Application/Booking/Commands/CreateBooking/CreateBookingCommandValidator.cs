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
                .Must(BeAValidStartDate)
                .WithMessage("StartDate must be at least one day in the future and within the time range of 09:00 to 21:00.");

        }

        // Custom validator to check if StartDate is in the future
        private bool BeAValidStartDate(DateTime startDate)
        {
            // Ensure the date is at least one day in the future
            if (startDate <= DateTime.UtcNow.AddDays(1))
                return false;

            // Ensure the time is between 09:00 and 21:00
            var startHour = startDate.TimeOfDay;
            var validStart = new TimeSpan(18, 0, 0);  // 09:00
            var validEnd = new TimeSpan(23, 0, 0);   // 21:00

            return startHour >= validStart && startHour <= validEnd;
        }

    }

}
