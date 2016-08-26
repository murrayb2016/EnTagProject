using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnTag.Services.Models
{
    public class TwitchCredModel
    {
        public string Access_token { get; set; }
        public string[] Scope { get; set; }
    }
}
