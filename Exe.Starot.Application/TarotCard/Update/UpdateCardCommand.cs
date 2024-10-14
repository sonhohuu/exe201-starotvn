using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.TarotCard.Update
{
    public record UpdateCardCommand : IRequest<string>
    {
        public int Id { get; init; }
        public string? Name { get; init; }
        public string? Type {  get; init; }
        public string ? Content {  get; init; }
        public IFormFile? imagefile { get; init; }
    
            public UpdateCardCommand() { }  
 
    }
}
