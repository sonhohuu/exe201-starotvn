using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.Feedback.Create
{
    public record CreateFeedbackCommand : IRequest<string>
    {
        public string ReaderId { get; init; }
        public int Rating { get; init; }
        public string Comment { get; init; }
    }
}
