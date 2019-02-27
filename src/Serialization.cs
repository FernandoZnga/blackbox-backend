using System.Xml.Serialization;
using System.IO;

namespace Blackbox.Server
{
    class Serialization
	{
		public static string SerializeCcPinNumber(string ccNumber, string pinNumber)
		{
            CcPinNumber CardInfo = new CcPinNumber
            {
                CcNumber = ccNumber,
                PinNumber = pinNumber
            };

            XmlSerializer xml = new XmlSerializer(typeof(CcPinNumber));
			using (StringWriter stringWriter = new StringWriter())
			{
                xml.Serialize(stringWriter, CardInfo);
                return stringWriter.ToString();
			};
		}
		public static CcPinNumber DeserializeCcPinNumber(string CardInfo)
		{
            XmlSerializer xml = new XmlSerializer(typeof(CcPinNumber));
			using (StringReader stringReader = new StringReader(CardInfo))
			{
                CcPinNumber ccPinNUmber = (CcPinNumber)xml.Deserialize(stringReader);
                return ccPinNUmber;
            }
		}
	}
}