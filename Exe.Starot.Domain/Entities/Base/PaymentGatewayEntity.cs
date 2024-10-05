using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Domain.Entities.Base
{
    public class PaymentGatewayEntity : BangMaGocEntity
    {
        public virtual ICollection<PaymentEntity> Payments { get; set; }
    }
}
