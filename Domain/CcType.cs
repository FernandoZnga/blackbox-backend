using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackbox.Server.Domain
{
    public class CcType
    {
        public CcType() => CreatedAt = DateTime.Now;

        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
