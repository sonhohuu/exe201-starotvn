using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.User.Register
{
    public class RegisterCommand : IRequest<string>
    {
        public RegisterCommand() { }

        public RegisterCommand(string email, string password, string repassword, string role)
        {
            Email = email;
            Password = password;
            Repassword = repassword;
            Role = role;
        }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Repassword { get; set; }
        public string Username { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
