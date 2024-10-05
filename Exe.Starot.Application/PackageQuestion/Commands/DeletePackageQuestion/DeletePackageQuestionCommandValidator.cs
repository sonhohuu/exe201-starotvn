using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.PackageQuestion.Commands.DeletePackageQuestion
{
    public class DeletePackageQuestionCommandValidator : AbstractValidator<DeletePackageQuestionCommand>
    {
        public DeletePackageQuestionCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Id must be greater than 0.");
        }
    }
}
