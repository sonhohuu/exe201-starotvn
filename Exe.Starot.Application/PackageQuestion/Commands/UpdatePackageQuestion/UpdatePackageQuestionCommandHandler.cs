using Exe.Starot.Application.Common.Interfaces;
using Exe.Starot.Application.FileUpload;
using Exe.Starot.Domain.Entities.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.PackageQuestion.Commands.UpdatePackageQuestion
{
    public record UpdatePackageQuestionCommand : IRequest<string>
    {
        public int Id { get; init; }
        public string? Name { get; init; }
        public string? Description { get; init; }
        public decimal? Price { get; init; }
        public IFormFile? Image { get; init; } // Optional
        public int? Time { get; init; }
    }

    public class UpdatePackageQuestionCommandHandler : IRequestHandler<UpdatePackageQuestionCommand, string>
    {
        private readonly IPackageQuestionRepository _packageQuestionRepository;
        private readonly FileUploadService _fileUploadService;
        private readonly ICurrentUserService _currentUserService;

        public UpdatePackageQuestionCommandHandler(IPackageQuestionRepository packageQuestionRepository, FileUploadService fileUploadService, ICurrentUserService currentUserService)
        {
            _packageQuestionRepository = packageQuestionRepository;
            _fileUploadService = fileUploadService;
            _currentUserService = currentUserService;
        }

        public async Task<string> Handle(UpdatePackageQuestionCommand request, CancellationToken cancellationToken)
        {
            var packageQuestion = await _packageQuestionRepository.FindAsync(x => x.ID == request.Id && !x.DeletedDay.HasValue, cancellationToken);

            if (packageQuestion == null)
            {
                throw new KeyNotFoundException("Package Question not found");
            }

            // Check if there's a duplicate name in another entry
            var duplicate = await _packageQuestionRepository.FindAsync(x => x.Name == request.Name && x.ID != request.Id && !x.DeletedDay.HasValue, cancellationToken);
            if (duplicate != null)
            {
                throw new DuplicateWaitObjectException("Another package question with the same name already exists.");
            }

            // Upload new image if provided
            string imageUrl = packageQuestion.Image;
            if (request.Image != null)
            {
                using (var stream = request.Image.OpenReadStream())
                {
                    imageUrl = await _fileUploadService.UploadFileAsync(stream, $"{Guid.NewGuid()}.jpg");
                }
                packageQuestion.Image = imageUrl;
            }

            // Update fields
            packageQuestion.Name = request.Name ?? packageQuestion.Name;
            packageQuestion.Description = request.Description ?? packageQuestion.Description;
            // Update Price only if request.Price has a value greater than 0
            if (request.Price.HasValue && request.Price.Value > 0)
            {
                packageQuestion.Price = request.Price.Value;
            }

            // Update Time only if request.Time has a value greater than 0
            if (request.Time.HasValue && request.Time.Value > 0)
            {
                packageQuestion.Time = request.Time.Value;
            }

            packageQuestion.UpdatedBy = _currentUserService.UserId;
            packageQuestion.UpdatedDay = DateTime.UtcNow;

            _packageQuestionRepository.Update(packageQuestion);

            return await _packageQuestionRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Update Successfully!" : "Update Failed!";
        }
    }
}
