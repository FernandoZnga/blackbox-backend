using System.Xml.Serialization;
using System.IO;
using Blackbox.Server.Prop;
using Blackbox.Client.src;
using System;

namespace Blackbox.Server
{
    class Serialization
	{
        // This a General Response when we need a simple Ok = 200 or Error = 500
        public static string GeneralResponse (int response)
        {
            GeneralResponse generalResponse = new GeneralResponse
            {
                Response = response
            };
            XmlSerializer xml = new XmlSerializer(typeof(GeneralResponse));
            using (StringWriter stringWriter = new StringWriter())
            {
                xml.Serialize(stringWriter, generalResponse);
                generalResponse.Key = GenerateKey.MD5(stringWriter.ToString());
                StringWriter stringWriterNew = new StringWriter();
                xml.Serialize(stringWriterNew, generalResponse);
                return stringWriterNew.ToString();
            };
        }
        // This section serialize the Credit Card Number and Pin Number -- not use for server
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
        public static AccountBalance DeserializeAccountBalance(string AccountInfo)
        {
            XmlSerializer xml = new XmlSerializer(typeof(AccountBalance));
            using (StringReader stringReader = new StringReader(AccountInfo))
            {
                AccountBalance accountBalance = (AccountBalance)xml.Deserialize(stringReader);
                return accountBalance;
            }
        }
        public static string SerializeCcPinNumberResponse(int account, int response)
        {
            CcPinNumberResponse accountInfo = new CcPinNumberResponse
            {
                Account = account,
                Response = response
            };

            XmlSerializer xml = new XmlSerializer(typeof(CcPinNumberResponse));
            using (StringWriter stringWriter = new StringWriter())
            {
                xml.Serialize(stringWriter, accountInfo);
                accountInfo.Key = GenerateKey.MD5(stringWriter.ToString());
                StringWriter stringWriterNew = new StringWriter();
                xml.Serialize(stringWriterNew, accountInfo);
                return stringWriterNew.ToString();
            }
        }

        internal static string SerializeAccountBalance(int accountId)
        {
            AccountBalance accountBalance = new AccountBalance
            {
                AccountId = accountId
            };

            XmlSerializer xml = new XmlSerializer(typeof(AccountBalance));
            using (StringWriter stringWriter = new StringWriter())
            {
                xml.Serialize(stringWriter, accountBalance);
                return stringWriter.ToString();
            }
        }

        public static string SerializeAccountBalanceResponse(int accountId, double balance, int response)
        {
            AccountBalanceResponse accountInfo = new AccountBalanceResponse
            {
                AccountId = accountId,
                Balance = balance,
                Response = response
            };

            XmlSerializer xml = new XmlSerializer(typeof(AccountBalanceResponse));
            using (StringWriter stringWriter = new StringWriter())
            {
                xml.Serialize(stringWriter, accountInfo);
                accountInfo.Key = GenerateKey.MD5(stringWriter.ToString());
                StringWriter stringWriterNew = new StringWriter();
                xml.Serialize(stringWriterNew, accountInfo);
                return stringWriterNew.ToString();
            }
        }
	}
}