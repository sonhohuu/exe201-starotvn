using AutoMapper;
using Exe.Starot.Application.PackageQuestion;
using Exe.Starot.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.Customer
{
    public static class CustomerDTOMappingExtention
    {
        public static CustomerDTO MapToCustomerDTO(this CustomerEntity projectFrom, IMapper mapper)
          => mapper.Map<CustomerDTO>(projectFrom);

        public static CustomerWithInfoDTO MapToCustomerWithInfoDTO(this CustomerEntity projectFrom, IMapper mapper)
          => mapper.Map<CustomerWithInfoDTO>(projectFrom);

        public static List<CustomerDTO> MapToCustomerDTOList(this IEnumerable<CustomerEntity> projectFrom, IMapper mapper)
          => projectFrom.Select(x => x.MapToCustomerDTO(mapper)).ToList();
    }
}
