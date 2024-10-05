using AutoMapper;
using Exe.Starot.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.PackageQuestion
{
    public static class PackageQuestionDTOMappingExtension
    {
        public static PackageQuestionDTO MapToPackageQuestionDTO(this PackageQuestionEntity projectFrom, IMapper mapper)
          => mapper.Map<PackageQuestionDTO>(projectFrom);

        public static List<PackageQuestionDTO> MapToPackageQuestionDTOList(this IEnumerable<PackageQuestionEntity> projectFrom, IMapper mapper)
          => projectFrom.Select(x => x.MapToPackageQuestionDTO(mapper)).ToList();
    }
}
