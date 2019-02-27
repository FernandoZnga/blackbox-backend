using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
 
// Used to serialize an object to binary format
using System.Runtime.Serialization.Formatters.Binary;
 
// Used to serialize into XML
using System.Xml.Serialization;

namespace Blackbox.Client
{
	class Serialization
	{
		static void SerializeCcPinNumber(string ccNumber, string pinNumber)
		{
			CcPinNumber ccPinNumber = new CcPinNumber
			{
				ccPinNumber.CcNumber = ccNumber;
				ccPinNumber.PinNumber = pinNumber;
			};

			XmlSerializer xml = new XmlSerializar(typeof(CcPinNumber));
			using (StringWriter stringWriter = new StringWriter())
			{
				xml.Serialize(stringWriter, ccPinNumber)
			}
		}
		static void DeserializeCcPinNumber(string ccPinNumber)
		{
			XmlSerializar xml = new XmlSerializer(typeof(CcPinNumber));
			using (StringReader stringReader = new StringReader(ccPinNumber))
			{
				CcPinNumber = (CcPinNumber)(xml.Deserialize(stringReader));
			}
		}
	}
}