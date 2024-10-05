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
        public int MemberShip { get; init; } = 0;
    }
    internal class UpdateReaderCommandHandler
    {
    }
}
