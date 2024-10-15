using AutoMapper;
using Exe.Starot.Application.Customer;
using Exe.Starot.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.Reader
{
    public static class ReaderDTOMappingExtention
    {
        public static ReaderDTO MapToReaderDTO(this ReaderEntity projectFrom, IMapper mapper)
          => mapper.Map<ReaderDTO>(projectFrom);

        public static ReaderWithInfoDTO MapToReaderWithInfoDTO(this ReaderEntity projectFrom, IMapper mapper)
          => mapper.Map<ReaderWithInfoDTO>(projectFrom);

        public static List<ReaderDTO> MapToReaderDTOList(this IEnumerable<ReaderEntity> projectFrom, IMapper mapper)
          => projectFrom.Select(x => x.MapToReaderDTO(mapper)).ToList();
    }
}
