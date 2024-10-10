using AutoMapper;
using Exe.Starot.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.User
{
    public static class UserDTOMappingExtention
    {
        public static UserDTO MapToUserDTO(this UserEntity projectFrom, IMapper mapper)
          => mapper.Map<UserDTO>(projectFrom);

        public static List<UserDTO> MapToUserDTOList(this IEnumerable<UserEntity> projectFrom, IMapper mapper)
          => projectFrom.Select(x => x.MapToUserDTO(mapper)).ToList();
    }
}
