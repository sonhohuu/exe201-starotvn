using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.Order.UpdateOrder
{
    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(command => command.ID)
                .NotEmpty().WithMessage("OrderID can't be empty or null");

            RuleFor(command => command.Status)
                .NotEmpty().WithMessage("Status can't be empty or null")
                .Must(status => new[] { "Đang xác nhận đơn hàng", "Đang giao hàng", "Đã giao hàng", "Đã hủy" }
                .Contains(status))
                .WithMessage("Status must be one of the following values: Đang xác nhận đơn hàng, Đang giao hàng, Đã giao hàng, Đã hủy");
        }
    }
}
