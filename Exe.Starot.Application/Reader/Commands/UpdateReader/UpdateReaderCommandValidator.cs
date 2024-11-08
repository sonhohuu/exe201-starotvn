﻿using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.Reader.Commands.UpdateReader
{
    public class UpdateReaderCommandValidator : AbstractValidator<UpdateReaderCommand>
    {
        public UpdateReaderCommandValidator()
        {
            // Validate FirstName (if provided)
            RuleFor(x => x.FirstName)
                .MaximumLength(100)
                .When(x => !string.IsNullOrEmpty(x.FirstName))
                .WithMessage("First name cannot exceed 100 characters.");

            RuleFor(x => x.Gender)
                .Must(gender => string.IsNullOrEmpty(gender) ||
                    gender.Equals("Female", StringComparison.OrdinalIgnoreCase) ||
                    gender.Equals("Male", StringComparison.OrdinalIgnoreCase))
                .WithMessage("Gender must be either 'Female', 'Male', or left empty.");

            // Validate LastName (if provided)
            RuleFor(x => x.LastName)
                .MaximumLength(100)
                .When(x => !string.IsNullOrEmpty(x.LastName))
                .WithMessage("Last name cannot exceed 100 characters.");

            // Validate Phone (if provided)
            RuleFor(x => x.Phone)
                .Matches(@"^0\d{9,13}$") // Regex to match a phone number starting with 0 and containing 10 to 14 digits
                .When(x => !string.IsNullOrEmpty(x.Phone)) // Only validate if the phone number is provided
                .WithMessage("Phone number is invalid. Must start with 0 and contain 10 to 14 digits.");

            // Validate DateOfBirth (if provided)
            RuleFor(x => x.DateOfBirth)
                .LessThan(DateTime.Today)
                .When(x => x.DateOfBirth.HasValue)
                .WithMessage("Date of birth must be in the past.");

            // Validate Expertise (if provided)
            RuleFor(x => x.Expertise)
                .MaximumLength(200)
                .When(x => !string.IsNullOrEmpty(x.Expertise))
                .WithMessage("Expertise description cannot exceed 200 characters.");

            // Validate Quote (if provided)
            RuleFor(x => x.Quote)
                .MaximumLength(500)
                .When(x => !string.IsNullOrEmpty(x.Quote))
                .WithMessage("Quote cannot exceed 500 characters.");

            // Validate Experience (if provided)
            RuleFor(x => x.Experience)
                .MaximumLength(1000)
                .When(x => !string.IsNullOrEmpty(x.Experience))
                .WithMessage("Experience description cannot exceed 1000 characters.");

            // Validate Rating (if provided)
            RuleFor(x => x.Rating)
                .InclusiveBetween(0, 5)
                .When(x => x.Rating.HasValue)
                .WithMessage("Rating must be between 0 and 5.");

            // Validate LinkUrl (if provided)
            RuleFor(x => x.LinkUrl)
                .Must(BeAValidUrl)
                .When(x => !string.IsNullOrEmpty(x.LinkUrl))
                .WithMessage("Link URL is not a valid URL.");

            // Validate Image (if provided)
            RuleFor(x => x.Image)
                .Must(BeAValidImage)
                .When(x => x.Image != null)
                .WithMessage("Only image files (jpg, png) are allowed.");
        }

        private bool BeAValidUrl(string linkUrl)
        {
            return Uri.TryCreate(linkUrl, UriKind.Absolute, out var _);
        }

        private bool BeAValidImage(IFormFile file)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var extension = Path.GetExtension(file.FileName).ToLower();
            return allowedExtensions.Contains(extension);
        }
    }

}
