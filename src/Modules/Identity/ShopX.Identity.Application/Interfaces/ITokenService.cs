using ShopX.Identity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopX.Identity.Application.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(ApplicationUser user);
    }
}
