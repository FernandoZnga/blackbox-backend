using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Blackbox.Client
{
	[Serializable()]
	public class CcPinNumber
	{
		public string CcNumber { get; set; }
		public string PinNumber { get; set; }

		public CcPinNumber(){}
		public CcPinNumber(string ccNumber, string pinNumber)
		{
			CcNumber = ccNumber;
			PinNumber = pinNumber;
		}

	}
}