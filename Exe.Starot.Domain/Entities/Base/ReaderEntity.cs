using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Domain.Entities.Base
{
    public class ReaderEntity : Entity
    {
        public string UserId { get; set; }
        public int ExperienceYears { get; set; } = 0;
        public string Quote { get; set; } = string.Empty;
        [Column(TypeName = "decimal(4,1)")]
        public decimal Rating { get; set; } = decimal.Zero;
        public string Image { get; set; } = string.Empty;
        public string LinkUrl { get; set; } = string.Empty;
        public string Introduction { get; set; } = string.Empty;
        public string Expertise {  get; set; } = string.Empty;
        public string Experience { get; set; } = string.Empty;

        public virtual UserEntity User { get; set; }
        public virtual ICollection<FeedbackEntity> Feedbacks { get; set; }
        public virtual ICollection<BookingEntity> Bookings { get; set; }
    }
}
