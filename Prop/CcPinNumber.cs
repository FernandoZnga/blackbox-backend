using System;

namespace Blackbox.Server
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