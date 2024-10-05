using Exe.Starot.Application.Common.Interfaces;
using Exe.Starot.Domain.Entities.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.PackageQuestion.Commands.DeletePackageQuestion
{
    public record DeletePackageQuestionCommand : IRequest<string>
    {
        public int Id { get; init; }
    }

    public class DeletePackageQuestionCommandHandler : IRequestHandler<DeletePackageQuestionCommand, string>
    {
        private readonly IPackageQuestionRepository _packageQuestionRepository;
        private readonly ICurrentUserService _currentUserService;

        public DeletePackageQuestionCommandHandler(IPackageQuestionRepository packageQuestionRepository, ICurrentUserService currentUserService)
        {
            _packageQuestionRepository = packageQuestionRepository;
            _currentUserService = currentUserService;
        }

        public async Task<string> Handle(DeletePackageQuestionCommand request, CancellationToken cancellationToken)
        {
            var packageQuestion = await _packageQuestionRepository.FindAsync(x => x.ID == request.Id && !x.DeletedDay.HasValue, cancellationToken);

            if (packageQuestion == null)
            {
                throw new KeyNotFoundException("Package Question not found");
            }

            // Soft delete by setting NgayXoa (delete timestamp)
            packageQuestion.DeletedBy = _currentUserService.UserId;
            packageQuestion.DeletedDay = DateTime.UtcNow;

            _packageQuestionRepository.Update(packageQuestion);

            return await _packageQuestionRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Delete Successfully!" : "Delete Failed!";
        }
    }
}
