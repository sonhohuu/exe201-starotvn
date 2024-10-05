using AutoMapper;
using Exe.Starot.Application.Common.Mappings;
using Exe.Starot.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.PackageQuestion
{
    public class PackageQuestionDTO : IMapFrom<PackageQuestionEntity>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; } // Assuming that storing the image URL in the database.
        public int Time { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<PackageQuestionEntity, PackageQuestionDTO>();
        }
    }
}
