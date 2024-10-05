using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Domain.Entities.Base
{
    public class PackageQuestionEntity : BangMaGocEntity
    {
        public string Description { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public int Time { get; set; }
        public string Image {  get; set; }
        public virtual ICollection<BookingEntity> Bookings { get; set; }
    }
}
