using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.PackageQuestion.Queries.GetPackageQuestion
{
    public class GetPackageQuestionByIdQueryValidator : AbstractValidator<GetPackageQuestionByIdQuery>
    {
        public GetPackageQuestionByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("PackageQuestion ID must be greater than 0.");
        }
    }

}
