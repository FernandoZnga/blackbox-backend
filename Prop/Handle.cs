using Blackbox.Client.src;
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
            //xmlText = xmlText.Substring(0, xmlText.IndexOf("<EOF>", 0));
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
                else if (api == "Withdraw")
                {
                    logText.Transaction = api;
                    var accountId = Serialization.DeserializeWithdraw(xmlText);
                    Withdraw withdraw = new Withdraw
                    {
                        AccountId = accountId.AccountId,
                        Amount = accountId.Amount
                    };
                    var md5OUT = GenerateKey.MD5(Serialization.SerializeWithdraw(accountId.AccountId, accountId.Amount));
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
                            if (Account.CcTypeId == 1) // Credit
                            {
                                Transaction transaction = new Transaction()
                                {
                                    Account = Account,
                                    BalanceBefore = Account.Balance,
                                    BalanceAfter = Account.Balance += accountId.Amount,
                                    Amount = accountId.Amount,
                                    TxTypeId = 1
                                };

                                //Account.Balance += accountId.Amount;
                                _context.Transactions.Add(transaction);
                                _context.SaveChanges();
                                Account = _context.Accounts.FirstOrDefault(s => s.Id == accountId.AccountId);

                                return Serialization.SerializeWithdrawResponse(Account.Id, Account.Balance, "Credit", 200);
                            }
                            else if (Account.CcTypeId == 2) // Debit
                            {
                                if (Account.Balance >= accountId.Amount)
                                {
                                    Transaction transaction = new Transaction()
                                    {
                                        Account = Account,
                                        BalanceBefore = Account.Balance,
                                        BalanceAfter = Account.Balance -= accountId.Amount,
                                        Amount = accountId.Amount,
                                        TxTypeId = 1
                                    };

                                    //Account.Balance -= accountId.Amount;
                                    _context.Transactions.Add(transaction);
                                    _context.SaveChanges();

                                    Account = _context.Accounts.FirstOrDefault(s => s.Id == accountId.AccountId);
                                    return Serialization.SerializeWithdrawResponse(Account.Id, Account.Balance, "Debit", 200);
                                }
                                else
                                {
                                    return Serialization.GeneralResponse(701).ToString();
                                }
                            }
                            else
                            {
                                return Serialization.GeneralResponse(701).ToString();
                            }
                        }
                    }
                }
                else if (api == "Deposit")
                {
                    logText.Transaction = api;
                    var accountId = Serialization.DeserializeDeposit(xmlText);
                    Deposit deposit = new Deposit
                    {
                        AccountId = accountId.AccountId,
                        Amount = accountId.Amount
                    };
                    var md5OUT = GenerateKey.MD5(Serialization.SerializeDeposit(accountId.AccountId, accountId.Amount));
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
                            if (Account.CcTypeId == 1) // Credit
                            {
                                Transaction transaction = new Transaction()
                                {
                                    Account = Account,
                                    BalanceBefore = Account.Balance,
                                    BalanceAfter = Account.Balance -= accountId.Amount,
                                    Amount = accountId.Amount,
                                    TxTypeId = 2
                                };

                                //Account.Balance += accountId.Amount;
                                _context.Transactions.Add(transaction);
                                _context.SaveChanges();
                                Account = _context.Accounts.FirstOrDefault(s => s.Id == accountId.AccountId);

                                return Serialization.SerializeDepositResponse(Account.Id, Account.Balance, "Credit", 200);
                            }
                            else if (Account.CcTypeId == 2) // Debit
                            {
                                Transaction transaction = new Transaction()
                                {
                                    Account = Account,
                                    BalanceBefore = Account.Balance,
                                    BalanceAfter = Account.Balance += accountId.Amount,
                                    Amount = accountId.Amount,
                                    TxTypeId = 2
                                };

                                //Account.Balance -= accountId.Amount;
                                _context.Transactions.Add(transaction);
                                _context.SaveChanges();

                                Account = _context.Accounts.FirstOrDefault(s => s.Id == accountId.AccountId);
                                return Serialization.SerializeDepositResponse(Account.Id, Account.Balance, "Debit", 200);
                            }
                            else
                            {
                                return Serialization.GeneralResponse(701).ToString();
                            }
                        }
                    }
                }
                else if (api == "Transfer")
                {
                    logText.Transaction = api;
                    var accountId = Serialization.DeserializeTransfer(xmlText);
                    Transfer transfer = new Transfer
                    {
                        AccountId = accountId.AccountId,
                        Amount = accountId.Amount,
                        AccountIdDestiny = accountId.AccountIdDestiny
                    };
                    var md5OUT = GenerateKey.MD5(Serialization.SerializeTransfer(accountId.AccountId, accountId.Amount, accountId.AccountIdDestiny));
                    logText.Md5OUT = md5OUT;
                    Log.Save(logText);

                    if (md5OUT != accountId.Key)
                    {
                        return Serialization.GeneralResponse(601).ToString();
                    }
                    else
                    {
                        var Account = _context.Accounts.FirstOrDefault(s => s.Id == accountId.AccountId);
                        var AccountDestiny = _context.Accounts.FirstOrDefault(s => s.Id == accountId.AccountIdDestiny);
                        if (Account == null || AccountDestiny == null)
                        {
                            return Serialization.GeneralResponse(501).ToString();
                        }
                        else
                        {
                            if (Account.CcTypeId == 1 && AccountDestiny.CcTypeId == 1) // Credit // Credit
                            {
                                Transaction transaction1 = new Transaction()
                                {
                                    Account = Account,
                                    BalanceBefore = Account.Balance,
                                    BalanceAfter = Account.Balance += accountId.Amount,
                                    Amount = accountId.Amount,
                                    TxTypeId = 4
                                };
                                Transaction transaction2 = new Transaction()
                                {
                                    Account = AccountDestiny,
                                    BalanceBefore = Account.Balance,
                                    BalanceAfter = Account.Balance -= accountId.Amount,
                                    Amount = accountId.Amount,
                                    TxTypeId = 3
                                };

                                //Account.Balance += accountId.Amount;
                                _context.Transactions.Add(transaction1);
                                _context.Transactions.Add(transaction2);
                                _context.SaveChanges();
                                Account = _context.Accounts.FirstOrDefault(s => s.Id == accountId.AccountId);
                                AccountDestiny = _context.Accounts.FirstOrDefault(s => s.Id == accountId.AccountIdDestiny);

                                return Serialization.SerializeTransferResponse(Account.Id, Account.Balance, "Credit", AccountDestiny.Id, 200);
                            }
                            else if (Account.CcTypeId == 1 && AccountDestiny.CcTypeId == 2) // Credit // Debit
                            {
                                Transaction transaction1 = new Transaction()
                                {
                                    Account = Account,
                                    BalanceBefore = Account.Balance,
                                    BalanceAfter = Account.Balance += accountId.Amount,
                                    Amount = accountId.Amount,
                                    TxTypeId = 4
                                };
                                Transaction transaction2 = new Transaction()
                                {
                                    Account = AccountDestiny,
                                    BalanceBefore = Account.Balance,
                                    BalanceAfter = Account.Balance += accountId.Amount,
                                    Amount = accountId.Amount,
                                    TxTypeId = 3
                                };

                                //Account.Balance += accountId.Amount;
                                _context.Transactions.Add(transaction1);
                                _context.Transactions.Add(transaction2);
                                _context.SaveChanges();
                                Account = _context.Accounts.FirstOrDefault(s => s.Id == accountId.AccountId);
                                AccountDestiny = _context.Accounts.FirstOrDefault(s => s.Id == accountId.AccountIdDestiny);

                                return Serialization.SerializeTransferResponse(Account.Id, Account.Balance, "Credit", AccountDestiny.Id, 200);
                            }
                            //------
                            if (Account.CcTypeId == 2 && AccountDestiny.CcTypeId == 2) // Debit // Debit
                            {
                                Transaction transaction1 = new Transaction()
                                {
                                    Account = Account,
                                    BalanceBefore = Account.Balance,
                                    BalanceAfter = Account.Balance -= accountId.Amount,
                                    Amount = accountId.Amount,
                                    TxTypeId = 4
                                };
                                Transaction transaction2 = new Transaction()
                                {
                                    Account = AccountDestiny,
                                    BalanceBefore = Account.Balance,
                                    BalanceAfter = Account.Balance += accountId.Amount,
                                    Amount = accountId.Amount,
                                    TxTypeId = 3
                                };

                                //Account.Balance += accountId.Amount;
                                _context.Transactions.Add(transaction1);
                                _context.Transactions.Add(transaction2);
                                _context.SaveChanges();
                                Account = _context.Accounts.FirstOrDefault(s => s.Id == accountId.AccountId);
                                AccountDestiny = _context.Accounts.FirstOrDefault(s => s.Id == accountId.AccountIdDestiny);

                                return Serialization.SerializeTransferResponse(Account.Id, Account.Balance, "Credit", AccountDestiny.Id, 200);
                            }
                            else if (Account.CcTypeId == 2 && AccountDestiny.CcTypeId == 1) // Debit // Credit
                            {
                                Transaction transaction1 = new Transaction()
                                {
                                    Account = Account,
                                    BalanceBefore = Account.Balance,
                                    BalanceAfter = Account.Balance -= accountId.Amount,
                                    Amount = accountId.Amount,
                                    TxTypeId = 4
                                };
                                Transaction transaction2 = new Transaction()
                                {
                                    Account = AccountDestiny,
                                    BalanceBefore = Account.Balance,
                                    BalanceAfter = Account.Balance -= accountId.Amount,
                                    Amount = accountId.Amount,
                                    TxTypeId = 3
                                };

                                //Account.Balance += accountId.Amount;
                                _context.Transactions.Add(transaction1);
                                _context.Transactions.Add(transaction2);
                                _context.SaveChanges();
                                Account = _context.Accounts.FirstOrDefault(s => s.Id == accountId.AccountId);
                                AccountDestiny = _context.Accounts.FirstOrDefault(s => s.Id == accountId.AccountIdDestiny);

                                return Serialization.SerializeTransferResponse(Account.Id, Account.Balance, "Credit", AccountDestiny.Id, 200);
                            }
                            else
                            {
                                return Serialization.GeneralResponse(701).ToString();
                            }
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
