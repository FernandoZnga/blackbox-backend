using System;

namespace Blackbox.Server.Domain
{
    class __TextLog
    {
        public __TextLog() => CreatedAt = DateTime.Now;

        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string XmlText { get; set; }
        public string DesText { get; set; }
    }
}
