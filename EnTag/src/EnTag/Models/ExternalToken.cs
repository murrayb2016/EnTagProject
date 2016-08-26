using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EnTag.Models
{
    public class ExternalToken
    {
        public int Id { get; set; }

        public string Token { get; set; }

        public string Secret { get; set; }

        public string Service { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
    }
}
