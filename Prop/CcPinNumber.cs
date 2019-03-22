using System;

namespace Blackbox.Server.Prop
{
    [Serializable()]
	public class CcPinNumber
	{
		public string CcNumber { get; set; }
		public string PinNumber { get; set; }
        public string AtmId { get; set; }
        public string Key { get; set; }

        public CcPinNumber() { }
		public CcPinNumber(string ccNumber, string pinNumber, string atmId)
		{
			CcNumber = ccNumber;
			PinNumber = pinNumber;
            AtmId = AtmId;
		}
        public CcPinNumber(string ccNumber, string pinNumber, string atmId, string key)
        {
            CcNumber = ccNumber;
            PinNumber = pinNumber;
            AtmId = AtmId;
            Key = key;
        }
	}
}