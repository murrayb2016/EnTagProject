using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnTag.Services.Models
{
    public class TokenDTO
    {
        public string Token { get; set; }

        public string Secret { get; set; }

        public string Service { get; set; }
    }
}
