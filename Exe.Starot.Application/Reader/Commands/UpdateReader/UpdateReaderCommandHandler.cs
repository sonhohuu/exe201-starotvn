using Exe.Starot.Application.Common.Interfaces;
using Exe.Starot.Application.FileUpload;
using Exe.Starot.Domain.Common.Exceptions;
using Exe.Starot.Domain.Entities.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.Reader.Commands.UpdateReader
{
    public record class UpdateReaderCommand() : IRequest<string>
    {
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        public IFormFile? Image { get; init; }
        public string? Phone { get; init; }
        public DateTime? DateOfBirth { get; init; }
        public string? Introduction {  get; init; }
        public string? Expertise { get; init; }
        public string? Gender { get; init; }
        public string? Quote { get; init; }
        public string? Experience { get; init; }
        public string? ExperienceYear { get; init; }
        public decimal? Rating { get; init; }
        public string? LinkUrl { get; init; }
    }
    public class UpdateReaderCommandHandler : IRequestHandler<UpdateReaderCommand, string>
    {
        private readonly IReaderRepository _readerRepository;
        private readonly FileUploadService _fileUploadService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserRepository _userRepository;

        public UpdateReaderCommandHandler(IReaderRepository readerRepository, FileUploadService fileUploadService, ICurrentUserService currentUserService, IUserRepository userRepository)
        {
            _readerRepository = readerRepository;
            _fileUploadService = fileUploadService;
            _currentUserService = currentUserService;
            _userRepository = userRepository;
        }

        public async Task<string> Handle(UpdateReaderCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            // Find the existing user by Id
            var user = await _userRepository.FindAsync(x => x.ID == userId && !x.DeletedDay.HasValue, cancellationToken);

            if (user == null)
            {
                throw new NotFoundException("User not found.");
            }

            var reader = await _readerRepository.FindAsync(x => x.UserId == userId && !x.DeletedDay.HasValue, cancellationToken);

            if (reader == null)
            {
                throw new NotFoundException("Reader not found.");
            }

            // Update fields if they are provided
            if (!string.IsNullOrEmpty(request.FirstName)) user.FirstName = request.FirstName;
            if (!string.IsNullOrEmpty(request.LastName)) user.LastName = request.LastName;
            if (!string.IsNullOrEmpty(request.Phone)) user.Phone = request.Phone;
            if (request.DateOfBirth.HasValue) user.DateOfBirth = request.DateOfBirth?.ToString("dd/MM/yyyy");
            if (!string.IsNullOrEmpty(request.Expertise)) reader.Expertise = request.Expertise;
            if (!string.IsNullOrEmpty(request.Introduction)) reader.Introduction = request.Introduction;
            if (!string.IsNullOrEmpty(request.ExperienceYear)) reader.ExperienceYear = request.ExperienceYear;
            if (!string.IsNullOrEmpty(request.Quote)) reader.Quote = request.Quote;
            if (!string.IsNullOrEmpty(request.Experience)) reader.Experience = request.Experience;
            if (request.Rating.HasValue) reader.Rating = request.Rating.Value;
            if (!string.IsNullOrEmpty(request.LinkUrl)) reader.LinkUrl = request.LinkUrl;
            if (!string.IsNullOrEmpty(request.Gender)) user.Gender = CapitalizeFirstLetter(request.Gender.ToLower());

            // Handle image upload if a new image is provided
            if (request.Image != null)
            {
                string imageUrl = string.Empty;
                using (var stream = request.Image.OpenReadStream())
                {
                    imageUrl = await _fileUploadService.UploadFileAsync(stream, $"{Guid.NewGuid()}.jpg");
                }
                user.Image = imageUrl;
            }

            // Update the modified by field
            user.UpdatedBy = _currentUserService.UserId;
            user.LastUpdated = DateTime.UtcNow;

            // Save the changes
            _readerRepository.Update(reader);
            _userRepository.Update(user);
            return await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Update Successfully!" : "Update Failed!";
        }

        public string CapitalizeFirstLetter(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            return char.ToUpper(input[0]) + input.Substring(1);
        }
    }

}
