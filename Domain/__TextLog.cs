using System;

namespace Blackbox.Server.Domain
{
    public class __TextLog
    {
        public __TextLog()
        {
            CreatedAt = DateTime.Now;
            Direction = "IN";
        }

        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string XmlText { get; set; }
        public string DesText { get; set; }
        public string Direction { get; set; }
        public string Transaction { get; set; }
        public string Md5IN { get; set; }
        public string Md5OUT { get; set; }
        public string AtmId { get; set; }
    }
}
