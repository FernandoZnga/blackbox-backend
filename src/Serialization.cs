using System.Xml.Serialization;
using System.IO;
using Blackbox.Server.Prop;

namespace Blackbox.Server.src
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
		public static string SerializeCcPinNumber(string ccNumber, string pinNumber, string atmId)
		{
            CcPinNumber CardInfo = new CcPinNumber
            {
                CcNumber = ccNumber,
                PinNumber = pinNumber,
                AtmId = atmId
            };

            XmlSerializer xml = new XmlSerializer(typeof(CcPinNumber));
			using (StringWriter stringWriter = new StringWriter())
			{
                xml.Serialize(stringWriter, CardInfo);
                return stringWriter.ToString();
			};
		}
		public static CcPinNumber DeserializeCcPinNumber(string cardInfo)
		{
            XmlSerializer xml = new XmlSerializer(typeof(CcPinNumber));
			using (StringReader stringReader = new StringReader(cardInfo))
			{
                CcPinNumber ccPinNUmber = (CcPinNumber)xml.Deserialize(stringReader);
                return ccPinNUmber;
            }
		}
        public static AccountBalance DeserializeAccountBalance(string accountInfo)
        {
            XmlSerializer xml = new XmlSerializer(typeof(AccountBalance));
            using (StringReader stringReader = new StringReader(accountInfo))
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

        internal static string SerializeAccountBalance(int accountId, string atmId)
        {
            AccountBalance accountBalance = new AccountBalance
            {
                AccountId = accountId,
                AtmId = atmId
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

        internal static string SerializeWithdraw(int accountId, double amount, string atmId)
        {
            Withdraw accountBalance = new Withdraw
            {
                AccountId = accountId,
                Amount = amount,
                AtmId = atmId
            };

            XmlSerializer xml = new XmlSerializer(typeof(Withdraw));
            using (StringWriter stringWriter = new StringWriter())
            {
                xml.Serialize(stringWriter, accountBalance);
                return stringWriter.ToString();
            }
        }

        public static Withdraw DeserializeWithdraw(string accountInfo)
        {
            XmlSerializer xml = new XmlSerializer(typeof(Withdraw));
            using (StringReader stringReader = new StringReader(accountInfo))
            {
                Withdraw withdraw = (Withdraw)xml.Deserialize(stringReader);
                return withdraw;
            }
        }

        internal static string SerializeWithdrawResponse(int accountId, double newBalance, string accountTypeName, int response)
        {
            WithdrawResponse accountInfo = new WithdrawResponse
            {
                AccountId = accountId,
                NewBalance = newBalance,
                AccountTypeName = accountTypeName,
                Response = response
        };

            XmlSerializer xml = new XmlSerializer(typeof(WithdrawResponse));
            using (StringWriter stringWriter = new StringWriter())
            {
                xml.Serialize(stringWriter, accountInfo);
                accountInfo.Key = GenerateKey.MD5(stringWriter.ToString());
                StringWriter stringWriterNew = new StringWriter();
                xml.Serialize(stringWriterNew, accountInfo);
                return stringWriterNew.ToString();
            }
        }

        internal static Deposit DeserializeDeposit(string accountInfo)
        {
            XmlSerializer xml = new XmlSerializer(typeof(Deposit));
            using (StringReader stringReader = new StringReader(accountInfo))
            {
                return (Deposit)xml.Deserialize(stringReader);
            }
        }

        internal static string SerializeDeposit(int accountId, double amount, string atmId)
        {
            Deposit accountBalance = new Deposit
            {
                AccountId = accountId,
                Amount = amount,
                AtmId = atmId
            };

            XmlSerializer xml = new XmlSerializer(typeof(Deposit));
            using (StringWriter stringWriter = new StringWriter())
            {
                xml.Serialize(stringWriter, accountBalance);
                return stringWriter.ToString();
            }
        }

        internal static string SerializeDepositResponse(int accountId, double newBalance, string accountTypeName, int response)
        {
            DepositResponse accountInfo = new DepositResponse
            {
                AccountId = accountId,
                NewBalance = newBalance,
                AccountTypeName = accountTypeName,
                Response = response
            };

            XmlSerializer xml = new XmlSerializer(typeof(DepositResponse));
            using (StringWriter stringWriter = new StringWriter())
            {
                xml.Serialize(stringWriter, accountInfo);
                accountInfo.Key = GenerateKey.MD5(stringWriter.ToString());
                StringWriter stringWriterNew = new StringWriter();
                xml.Serialize(stringWriterNew, accountInfo);
                return stringWriterNew.ToString();
            }
        }

        internal static Transfer DeserializeTransfer(string accountInfo)
        {
            XmlSerializer xml = new XmlSerializer(typeof(Transfer));
            using (StringReader stringReader = new StringReader(accountInfo))
            {
                return (Transfer)xml.Deserialize(stringReader);
            }
        }

        internal static string SerializeTransfer(int accountId, double amount, int accountIdDestiny, string atmId)
        {
            Transfer accountInfo = new Transfer
            {
                AccountId = accountId,
                Amount = amount,
                AccountIdDestiny = accountIdDestiny,
                AtmId = atmId
            };

            XmlSerializer xml = new XmlSerializer(typeof(Transfer));
            using (StringWriter stringWriter = new StringWriter())
            {
                xml.Serialize(stringWriter, accountInfo);
                return stringWriter.ToString();
            }
        }

        internal static string SerializeTransferResponse(int accountId, double newBalance, string accountTypeName, int accountIdDestiny, int response)
        {
            TransferResponse accountInfo = new TransferResponse
            {
                AccountId = accountId,
                NewBalance = newBalance,
                AccountTypeName = accountTypeName,
                AccountIdDestiny = accountIdDestiny,
                Response = response
            };

            XmlSerializer xml = new XmlSerializer(typeof(TransferResponse));
            using (StringWriter stringWriter = new StringWriter())
            {
                xml.Serialize(stringWriter, accountInfo);
                accountInfo.Key = GenerateKey.MD5(stringWriter.ToString());
                StringWriter stringWriterNew = new StringWriter();
                xml.Serialize(stringWriterNew, accountInfo);
                return stringWriterNew.ToString();
            }
        }

        internal static ChangePin DeserializeChangePin(string xmlText)
        {
            XmlSerializer xml = new XmlSerializer(typeof(ChangePin));
            using (StringReader stringReader = new StringReader(xmlText))
            {
                return (ChangePin)xml.Deserialize(stringReader);
            }
        }

        internal static string SerializeChangePin(int account, string currentPin, string newPin, string atmId)
        {
            ChangePin changePin = new ChangePin
            {
                Account = account,
                CurrentPin = currentPin,
                NewPin = newPin,
                AtmId = atmId
            };

            XmlSerializer xml = new XmlSerializer(typeof(ChangePin));
            using (StringWriter stringWriter = new StringWriter())
            {
                xml.Serialize(stringWriter, changePin);
                return stringWriter.ToString();
            }
        }

        internal static object SerializeChangePinResponse(int accountId, int response)
        {
            ChangePinResponse accountInfo = new ChangePinResponse
            {
                AccountId = accountId,
                Response = response
            };

            XmlSerializer xml = new XmlSerializer(typeof(ChangePinResponse));
            using (StringWriter stringWriter = new StringWriter())
            {
                xml.Serialize(stringWriter, accountInfo);
                accountInfo.Key = GenerateKey.MD5(stringWriter.ToString());
                StringWriter stringWriterNew = new StringWriter();
                xml.Serialize(stringWriterNew, accountInfo);
                return stringWriterNew.ToString();
            }
        }

        internal static PayEnee DeserializePayEnee(string xmlText)
        {
            XmlSerializer xml = new XmlSerializer(typeof(PayEnee));
            using (StringReader stringReader = new StringReader(xmlText))
            {
                return (PayEnee)xml.Deserialize(stringReader);
            }
        }

        internal static string SerializePayEnee(int accountId, int billId, string atmId)
        {
            PayEnee payment = new PayEnee
            {
                AccountId = accountId,
                BillId = billId,
                AtmId = atmId
            };

            XmlSerializer xml = new XmlSerializer(typeof(PayEnee));
            using (StringWriter stringWriter = new StringWriter())
            {
                xml.Serialize(stringWriter, payment);
                return stringWriter.ToString();
            }
        }

        internal static string SerializePayEneeResponse(int accountId, double newBalance, string accountTypeName, int response)
        {
            PayEneeResponse accountInfo = new PayEneeResponse
            {
                AccountId = accountId,
                NewBalance = newBalance,
                AccountTypeName = accountTypeName,
                Response = response
            };

            XmlSerializer xml = new XmlSerializer(typeof(PayEneeResponse));
            using (StringWriter stringWriter = new StringWriter())
            {
                xml.Serialize(stringWriter, accountInfo);
                accountInfo.Key = GenerateKey.MD5(stringWriter.ToString());
                StringWriter stringWriterNew = new StringWriter();
                xml.Serialize(stringWriterNew, accountInfo);
                return stringWriterNew.ToString();
            }
        }

        internal static PayHondutel DeserializePayHondutel(string xmlText)
        {
            XmlSerializer xml = new XmlSerializer(typeof(PayHondutel));
            using (StringReader stringReader = new StringReader(xmlText))
            {
                return (PayHondutel)xml.Deserialize(stringReader);
            }
        }

        internal static string SerializePayHondutel(int accountId, int billId, string atmId)
        {
            PayHondutel payment = new PayHondutel
            {
                AccountId = accountId,
                BillId = billId,
                AtmId = atmId
            };

            XmlSerializer xml = new XmlSerializer(typeof(PayHondutel));
            using (StringWriter stringWriter = new StringWriter())
            {
                xml.Serialize(stringWriter, payment);
                return stringWriter.ToString();
            }
        }

        internal static string SerializePayHondutelResponse(int accountId, double newBalance, string accountTypeName, int response)
        {
            PayHondutelResponse accountInfo = new PayHondutelResponse
            {
                AccountId = accountId,
                NewBalance = newBalance,
                AccountTypeName = accountTypeName,
                Response = response
            };

            XmlSerializer xml = new XmlSerializer(typeof(PayHondutelResponse));
            using (StringWriter stringWriter = new StringWriter())
            {
                xml.Serialize(stringWriter, accountInfo);
                accountInfo.Key = GenerateKey.MD5(stringWriter.ToString());
                StringWriter stringWriterNew = new StringWriter();
                xml.Serialize(stringWriterNew, accountInfo);
                return stringWriterNew.ToString();
            }
        }

        internal static PaySanaa DeserializePaySanaa(string xmlText)
        {
            XmlSerializer xml = new XmlSerializer(typeof(PaySanaa));
            using (StringReader stringReader = new StringReader(xmlText))
            {
                return (PaySanaa)xml.Deserialize(stringReader);
            }
        }

        internal static string SerializePaySanaa(int accountId, int billId, string atmId)
        {
            PaySanaa payment = new PaySanaa
            {
                AccountId = accountId,
                BillId = billId,
                AtmId = atmId
            };

            XmlSerializer xml = new XmlSerializer(typeof(PaySanaa));
            using (StringWriter stringWriter = new StringWriter())
            {
                xml.Serialize(stringWriter, payment);
                return stringWriter.ToString();
            }
        }

        internal static string SerializePaySanaaResponse(int accountId, double newBalance, string accountTypeName, int response)
        {
            PaySanaaResponse accountInfo = new PaySanaaResponse
            {
                AccountId = accountId,
                NewBalance = newBalance,
                AccountTypeName = accountTypeName,
                Response = response
            };

            XmlSerializer xml = new XmlSerializer(typeof(PaySanaaResponse));
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