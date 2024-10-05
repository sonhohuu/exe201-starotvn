﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Domain.Entities.Base
{
    public abstract class BangMaGocEntity
    {
        protected BangMaGocEntity()
        {
            CreatedDate = UpdatedDay = DateTime.UtcNow;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }
        public required string Name { get; set; }

        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }

        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDay { get; set; }

        public string? DeletedBy { get; set; }
        public DateTime? DeletedDay { get; set; }
    }
}
