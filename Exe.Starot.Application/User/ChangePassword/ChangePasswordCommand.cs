﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Exe.Starot.Application.User.ChangePassword
{
    public class ChangePasswordCommand : IRequest<string>
    {
        public ChangePasswordCommand(string oldPassword, string newPassword)
        {
            OldPassword = oldPassword;
            NewPassword = newPassword;
        }
        public ChangePasswordCommand()
        {

        }

        public required string OldPassword { get; set; }
        public required string NewPassword { get; set; }
    }
}
