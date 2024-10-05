using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.PackageQuestion.Commands.UpdatePackageQuestion
{
    public class UpdatePackageQuestionCommandValidator : AbstractValidator<UpdatePackageQuestionCommand>
    {
        public UpdatePackageQuestionCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Id must be greater than 0.");

            RuleFor(x => x.Name)
                .MaximumLength(255).WithMessage("Name must not exceed 255 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters.");

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .When(x => x.Price.HasValue && x.Price > 0)
                .WithMessage("Time must be greater than 0.");

            RuleFor(x => x.Time)
                .GreaterThan(0)
                .When(x => x.Time.HasValue && x.Time > 0)
                .WithMessage("Time must be greater than 0.");


            RuleFor(x => x.Image)
                .Must(BeAValidFile).When(x => x.Image != null).WithMessage("Invalid image format or size.");
        }

        // Example custom validator for image
        private bool BeAValidFile(IFormFile file)
        {
            if (file == null) return true;

            // Check for valid image types and size (example max size: 5MB)
            var allowedTypes = new[] { "image/jpeg", "image/png", "image/gif" };
            return allowedTypes.Contains(file.ContentType) && file.Length <= 5 * 1024 * 1024;
        }
    }
}
