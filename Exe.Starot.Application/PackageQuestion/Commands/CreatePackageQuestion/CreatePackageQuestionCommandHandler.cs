using Exe.Starot.Application.Common.Interfaces;
using Exe.Starot.Application.FileUpload;
using Exe.Starot.Domain.Entities.Base;
using Exe.Starot.Domain.Entities.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.PackageQuestion.Commands.CreatePackageQuestion
{
    public record CreatePackageQuestionCommand : IRequest<string>
    {
        public string Name { get; init; }
        public string Description { get; init; }
        public decimal Price { get; init; }
        public IFormFile Image { get; init; }
        public int Time { get; init; }
    }

    public class CreatePackageQuestionCommandHandler : IRequestHandler<CreatePackageQuestionCommand, string>
    {
        private readonly IPackageQuestionRepository _packageQuestionRepository;
        private readonly FileUploadService _fileUploadService;
        private readonly ICurrentUserService _currentUserService;

        public CreatePackageQuestionCommandHandler(IPackageQuestionRepository packageQuestionRepository, FileUploadService fileUploadService, ICurrentUserService currentUserService)
        {
            _packageQuestionRepository = packageQuestionRepository;
            _fileUploadService = fileUploadService;
            _currentUserService = currentUserService;
        }

        public async Task<string> Handle(CreatePackageQuestionCommand request, CancellationToken cancellationToken)
        {
            var exist = await _packageQuestionRepository.FindAsync(x => x.Name == request.Name && !x.DeletedDay.HasValue, cancellationToken);

            if (exist != null)
            {
                throw new DuplicateWaitObjectException("");
            }

            string imageUrl = string.Empty;
            using (var stream = request.Image.OpenReadStream())
            {
                imageUrl = await _fileUploadService.UploadFileAsync(stream, $"{Guid.NewGuid()}.jpg");
            }

            var pq = new PackageQuestionEntity()
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                Time = request.Time,
                Image = imageUrl,

                CreatedBy = _currentUserService.UserId
            };

            
            _packageQuestionRepository.Add(pq);


            return await _packageQuestionRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Create Sucessfully!" : "Create Failed!"; ;
        }
    }
}
