using Blackbox.Server.DataConn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Blackbox.Server.Prop
{
    public static class Handle
    {
        private static DataContext _context = new DataContext();
        public static string api;
        
        public static string ReadText(string xmlText)
        {
            xmlText = xmlText.Substring(0, xmlText.IndexOf("<EOF>", 0));
            XDocument xmlNode = XDocument.Parse(xmlText);
            foreach (var head in xmlNode.Elements())
            {
                api = head.Name.ToString();
                if (api == "CcPinNumber")
                {
                    var ccNumber = Serialization.DeserializeCcPinNumber(xmlText);
                    var CreditCard = _context.CreditCards.FirstOrDefault(s => s.CcNumber == ccNumber.CcNumber);
                    if (CreditCard == null)
                    {
                        return Serialization.GeneralResponse(500).ToString();
                    }
                    else if (CreditCard.PinNumber != ccNumber.PinNumber)
                    {
                        return Serialization.GeneralResponse(500).ToString();
                    }
                    else
                    {
                        var account = _context.Accounts.FirstOrDefault(s => s.CreditCard.CcNumber == ccNumber.CcNumber);
                        //account.Id;
                        if (account == null)
                        {
                            return Serialization.GeneralResponse(500).ToString();
                        }
                        else
                        {
                            return Serialization.SerializeCcPinNumberResponse(account.Id, 200).ToString();
                        }
                    }
                }
                else if (api == "AccountBalance")
                {
                    var accountId = Serialization.DeserializeAccountBalance(xmlText);
                    var Account = _context.Accounts.FirstOrDefault(s => s.Id == accountId.AccountId);
                    if (Account == null)
                    {
                        return Serialization.GeneralResponse(501).ToString();
                    }
                    else
                    {

                        return Serialization.SerializeAccountBalanceResponse(Account.Id, Account.Balance, 201).ToString();
                    }
                }
                else
                {
                    return Serialization.GeneralResponse(501).ToString();
                }
            }
            return Serialization.GeneralResponse(501).ToString(); ;
        }
    }
}
