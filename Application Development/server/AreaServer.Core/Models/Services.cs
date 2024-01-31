using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AreaServer.Core.Models
{
    public class Services
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string LogoUrl { get; set; } = string.Empty;

        public DateTime DateCreation { get; set; }
    }
}
