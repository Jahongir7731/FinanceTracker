using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTracker.Domain.DTOs
{
    public class LoginDto
    {
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
