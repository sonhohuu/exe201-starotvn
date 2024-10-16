using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.FavoriteProduct.Create
{
    public class AddFavoriteProductCommand : IRequest<string>
    {
        public string ProductId { get; set; }
    }
}
