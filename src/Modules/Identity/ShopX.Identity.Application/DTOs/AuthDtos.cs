using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopX.Identity.Application.DTOs
{

    public record RegisterDto(string FullName, string Email, string Password);
    public record LoginDto(string Email, string Password);
    public record AuthResponseDto(string Token, string Email, string FullName);
}
