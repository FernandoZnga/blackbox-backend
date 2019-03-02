﻿using Blackbox.Client.src;
using Blackbox.Server.DataConn;
using Blackbox.Server.Domain;
using System.Linq;
using System.Xml.Linq;

namespace Blackbox.Server.Prop
{
    public static class Handle
    {
        private static DataContext _context = new DataContext();
        public static string api;
        
        public static string ReadText(string xmlText, __TextLog logText)
        {
            xmlText = xmlText.Substring(0, xmlText.IndexOf("<EOF>", 0));
            XDocument xmlNode = XDocument.Parse(xmlText);
            foreach (var head in xmlNode.Elements())
            {
                api = head.Name.ToString();
                if (api == "CcPinNumber")
                {
                    logText.Transaction = api;
                    var ccNumber = Serialization.DeserializeCcPinNumber(xmlText);
                    CcPinNumber ccPinNumber = new CcPinNumber
                    {
                        CcNumber = ccNumber.CcNumber,
                        PinNumber = ccNumber.PinNumber
                    };
                    var md5OUT = GenerateKey.MD5(Serialization.SerializeCcPinNumber(ccNumber.CcNumber, ccNumber.PinNumber));
                    logText.Md5OUT = md5OUT;
                    Log.Save(logText);

                    if (md5OUT != ccNumber.Key)
                    {
                        return Serialization.GeneralResponse(601).ToString();
                    }
                    else
                    {
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
                }
                else if (api == "AccountBalance")
                {
                    logText.Transaction = api;
                    var accountId = Serialization.DeserializeAccountBalance(xmlText);
                    AccountBalance account = new AccountBalance
                    {
                        AccountId = accountId.AccountId
                    };
                    var md5OUT = GenerateKey.MD5(Serialization.SerializeAccountBalance(account.AccountId));
                    logText.Md5OUT = md5OUT;
                    Log.Save(logText);

                    if (md5OUT != accountId.Key)
                    {
                        return Serialization.GeneralResponse(601).ToString();
                    }
                    else
                    {
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