using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.Order.Create
{
    public class CreateOrderCommand : IRequest<string>
    {

        public CreateOrderCommand(List<RequestItem> products)
        {
            Products = products;
           
        }
        public string Address { get; set; }
        public string PaymentMethod { get; set; }
        public List<RequestItem> Products { get; set; }
    }

    public class RequestItem
    {
        public string ProductID { get; set; }
        public int Quantity { get; set; }
      
    }

}
