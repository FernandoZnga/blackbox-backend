using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Blackbox.Server.Prop
{
    public class Handler
    {
        public static string api;
        
        public static void ReadText(string xmlText)
        {
            xmlText = xmlText.Substring(0, xmlText.IndexOf("<EOF>", 0));
            XDocument xmlNode = XDocument.Parse(xmlText);
            foreach (var head in xmlNode.Elements())
            {
                api = head.Name.ToString();
                if (api == "CcPinNumber")
                {
                    Serialization.DeserializeCcPinNumber(xmlText);
                }
            }
        }
    }
}
