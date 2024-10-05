﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.User.Reader.CreateReader
{
    public class CreateReaderCommandValidator : AbstractValidator<CreateReaderCommand>
    {
        public CreateReaderCommandValidator() {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
            RuleFor(x => x.Role).NotEmpty().Equal("Reader");

            RuleFor(x => x.Phone).NotEmpty().Matches(@"^\+?\d+$").WithMessage("Invalid phone number format");

        }
    }
}
