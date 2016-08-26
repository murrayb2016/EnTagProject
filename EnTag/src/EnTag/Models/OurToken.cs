using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnTag.Models
{
    public class OurToken
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public string Secret { get; set; }
        public string Service { get; set; }
    }
}
