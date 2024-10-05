using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.PackageQuestion.Commands.CreatePackageQuestion
{
    public class CreatePackageQuestionCommandValidator : AbstractValidator<CreatePackageQuestionCommand>
    {
        public CreatePackageQuestionCommandValidator()
        {
            // Name validation
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(255).WithMessage("Name cannot exceed 255 characters.");

            // Description validation
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters.");

            // Price validation
            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero.");

            // Image validation (example: ensure the file is not null and has a valid extension)
            RuleFor(x => x.Image)
                .NotNull().WithMessage("Image is required.")
                .Must(IsValidImageExtension).WithMessage("Image must be a valid file type (jpg, png).");

            // Time validation (example: must be a positive integer)
            RuleFor(x => x.Time)
                .GreaterThan(0).WithMessage("Time must be greater than zero.");
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
