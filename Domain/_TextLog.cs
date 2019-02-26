using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackbox.Server.Domain
{
    class _TextLog
    {
        public _TextLog() => CreatedAt = DateTime.Now;

        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string XmlText { get; set; }
        public string DesText { get; set; }
    }
}
