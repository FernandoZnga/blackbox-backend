using System;

namespace Blackbox.Server.Prop
{
    [Serializable()]
    public class ChangePin
    {
        public int Account { get; set; }
        public string CurrentPin { get; set; }
        public string NewPin { get; set; }
        public string AtmId { get; set; }
        public string Key { get; set; }

        public ChangePin() { }
        public ChangePin(int account, string currentPin, string newPin, string atmId)
        {
            Account = account;
            CurrentPin = currentPin;
            NewPin = newPin;
            AtmId = atmId;
        }
        public ChangePin(int account, string currentPin, string newPin, string atmId, string key)
        {
            Account = account;
            CurrentPin = currentPin;
            NewPin = newPin;
            AtmId = atmId;
            Key = key;
        }
    }
}
