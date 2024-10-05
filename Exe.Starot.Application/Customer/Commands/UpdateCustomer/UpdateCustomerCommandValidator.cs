using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.Customer.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
    {
        public UpdateCustomerCommandValidator()
        {
            // FirstName validation
            RuleFor(x => x.FirstName)
                .MaximumLength(255).WithMessage("First Name cannot exceed 255 characters.");

            // LastName validation
            RuleFor(x => x.LastName)
                .MaximumLength(255).WithMessage("Last Name cannot exceed 255 characters.");

            // Phone validation (example: ensuring it's a valid phone number format)
            RuleFor(x => x.Phone)
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Phone number must be valid.");

            // DateOfBirth validation (example: optional, but must be in the past)
            RuleFor(x => x.DateOfBirth)
                .LessThanOrEqualTo(DateTime.Today).WithMessage("Date of Birth must be in the past.");

            // Image validation (if provided, it must be a valid image type)
            RuleFor(x => x.Image)
                .Must(IsValidImageExtension).When(x => x.Image != null).WithMessage("Image must be a valid file type (jpg, png).");

            // Membership validation (example: optional but if provided must be a valid value)
            RuleFor(x => x.MemberShip)
                .GreaterThan(0).WithMessage("Membership must be greater than 0.");
        }

        // Helper method to check for valid image file extensions
        private bool IsValidImageExtension(IFormFile file)
        {
            var validExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var fileExtension = Path.GetExtension(file.FileName).ToLower();
            return validExtensions.Contains(fileExtension);
        }
    }

}
