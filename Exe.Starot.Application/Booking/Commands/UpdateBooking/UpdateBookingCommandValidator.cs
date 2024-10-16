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

            RuleFor(command => command.Status)
                .NotEmpty().WithMessage("Status can't be empty or null")
                .Must(status => new[] { "Sắp diễn ra", "Đang diễn ra", "Hoàn thành", "Đã hủy" }
                .Contains(status))
                .WithMessage("Status must be one of the following values: Sắp diễn ra,Đang diễn ra, Hoàn thành, Đã hủy");
        }
    }


}
