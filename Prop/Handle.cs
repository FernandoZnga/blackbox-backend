using Blackbox.Server.src;
using Blackbox.Server.DataConn;
using Blackbox.Server.Domain;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;

namespace Blackbox.Server.Prop
{
    public static class Handle
    {
        private static DataContext _context = new DataContext();
        public static string api;
        
        public static string ReadText(string xmlText, __TextLog logText)
        {
            if (xmlText == "999")
            {
                logText.Transaction = "<Security Bridge>";
                logText.Md5OUT = "Security Bridge";
                Log.SaveIn(logText);
                return Serialization.GeneralResponse(601).ToString();
            }
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
                        PinNumber = ccNumber.PinNumber,
                        AtmId = ccNumber.AtmId
                    };
                    var md5OUT = GenerateKey.MD5(Serialization.SerializeCcPinNumber(ccNumber.CcNumber, ccNumber.PinNumber, ccNumber.AtmId));
                    logText.Md5OUT = md5OUT;
                    Log.SaveIn(logText);

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
                                var customer = _context.Customers.FirstOrDefault(s => s.Id == account.CustomerId);
                                Mail.CcPinNumber(customer);
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
                        AccountId = accountId.AccountId,
                        AtmId = accountId.AtmId
                    };
                    var md5OUT = GenerateKey.MD5(Serialization.SerializeAccountBalance(account.AccountId, account.AtmId));
                    logText.Md5OUT = md5OUT;
                    Log.SaveIn(logText);

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
                        Amount = accountId.Amount,
                        AtmId = accountId.AtmId
                    };
                    var md5OUT = GenerateKey.MD5(Serialization.SerializeWithdraw(accountId.AccountId, accountId.Amount, accountId.AtmId));
                    logText.Md5OUT = md5OUT;
                    Log.SaveIn(logText);

                    if (md5OUT != accountId.Key)
                    {
                        return Serialization.GeneralResponse(601).ToString();
                    }
                    else
                    {
                        var account = _context.Accounts.FirstOrDefault(s => s.Id == accountId.AccountId);
                        if (account == null)
                        {
                            return Serialization.GeneralResponse(501).ToString();
                        }
                        else
                        {
                            if (account.CcTypeId == 1) // Credit
                            {
                                Transaction transaction = new Transaction()
                                {
                                    Account = account,
                                    BalanceBefore = account.Balance,
                                    BalanceAfter = account.Balance += accountId.Amount,
                                    Amount = accountId.Amount,
                                    TxTypeId = 1,
                                    AccountTypeName = "Credit",
                                    AtmId = accountId.AtmId
                                };

                                //Account.Balance += accountId.Amount;
                                _context.Transactions.Add(transaction);
                                _context.SaveChanges();

                                account = _context.Accounts.FirstOrDefault(s => s.Id == accountId.AccountId);
                                Customer customer = _context.Customers.FirstOrDefault(s => s.Id == account.CustomerId);
                                Mail.Withdraw(customer, transaction);
                                return Serialization.SerializeWithdrawResponse(account.Id, account.Balance, "Credit", 200);
                            }
                            else if (account.CcTypeId == 2) // Debit
                            {
                                if (account.Balance >= accountId.Amount)
                                {
                                    Transaction transaction = new Transaction()
                                    {
                                        Account = account,
                                        BalanceBefore = account.Balance,
                                        BalanceAfter = account.Balance -= accountId.Amount,
                                        Amount = accountId.Amount,
                                        TxTypeId = 1,
                                        AccountTypeName = "Debit",
                                        AtmId = accountId.AtmId
                                    };

                                    //Account.Balance -= accountId.Amount;
                                    _context.Transactions.Add(transaction);
                                    _context.SaveChanges();

                                    account = _context.Accounts.FirstOrDefault(s => s.Id == accountId.AccountId);
                                    Customer customer = _context.Customers.FirstOrDefault(s => s.Id == account.CustomerId);
                                    Mail.Withdraw(customer, transaction);
                                    return Serialization.SerializeWithdrawResponse(account.Id, account.Balance, "Debit", 200);
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
                    var md5OUT = GenerateKey.MD5(Serialization.SerializeDeposit(accountId.AccountId, accountId.Amount, accountId.AtmId));
                    logText.Md5OUT = md5OUT;
                    Log.SaveIn(logText);

                    if (md5OUT != accountId.Key)
                    {
                        return Serialization.GeneralResponse(601).ToString();
                    }
                    else
                    {
                        var account = _context.Accounts.FirstOrDefault(s => s.Id == accountId.AccountId);
                        if (account == null)
                        {
                            return Serialization.GeneralResponse(501).ToString();
                        }
                        else
                        {
                            if (account.CcTypeId == 1) // Credit
                            {
                                Transaction transaction = new Transaction()
                                {
                                    Account = account,
                                    BalanceBefore = account.Balance,
                                    BalanceAfter = account.Balance -= accountId.Amount,
                                    Amount = accountId.Amount,
                                    TxTypeId = 2,
                                    AccountTypeName = "Credit",
                                    AtmId = accountId.AtmId
                                };

                                //Account.Balance += accountId.Amount;
                                _context.Transactions.Add(transaction);
                                _context.SaveChanges();

                                account = _context.Accounts.FirstOrDefault(s => s.Id == accountId.AccountId);
                                Customer customer = _context.Customers.FirstOrDefault(s => s.Id == account.CustomerId);
                                Mail.Deposit(customer, transaction);
                                return Serialization.SerializeDepositResponse(account.Id, account.Balance, "Credit", 200);
                            }
                            else if (account.CcTypeId == 2) // Debit
                            {
                                Transaction transaction = new Transaction()
                                {
                                    Account = account,
                                    BalanceBefore = account.Balance,
                                    BalanceAfter = account.Balance += accountId.Amount,
                                    Amount = accountId.Amount,
                                    TxTypeId = 2,
                                    AccountTypeName = "Debit",
                                    AtmId = accountId.AtmId
                                };

                                //Account.Balance -= accountId.Amount;
                                _context.Transactions.Add(transaction);
                                _context.SaveChanges();

                                account = _context.Accounts.FirstOrDefault(s => s.Id == accountId.AccountId);
                                Customer customer = _context.Customers.FirstOrDefault(s => s.Id == account.CustomerId);
                                Mail.Deposit(customer, transaction);
                                return Serialization.SerializeDepositResponse(account.Id, account.Balance, "Debit", 200);
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
                    var md5OUT = GenerateKey.MD5(Serialization.SerializeTransfer(accountId.AccountId, accountId.Amount, accountId.AccountIdDestiny, accountId.AtmId));
                    logText.Md5OUT = md5OUT;
                    Log.SaveIn(logText);

                    if (md5OUT != accountId.Key)
                    {
                        return Serialization.GeneralResponse(601).ToString();
                    }
                    else
                    {
                        var account = _context.Accounts.FirstOrDefault(s => s.Id == accountId.AccountId);
                        var accountDestiny = _context.Accounts.FirstOrDefault(s => s.Id == accountId.AccountIdDestiny);
                        if (account == null || accountDestiny == null)
                        {
                            return Serialization.GeneralResponse(501).ToString();
                        }
                        else
                        {
                            if (account.CcTypeId == 1 && accountDestiny.CcTypeId == 1) // Credit // Credit
                            {
                                Transaction transaction1 = new Transaction()
                                {
                                    Account = account,
                                    BalanceBefore = account.Balance,
                                    BalanceAfter = account.Balance += accountId.Amount,
                                    Amount = accountId.Amount,
                                    TxTypeId = 4,
                                    AccountTypeName = "Credit",
                                    AtmId = accountId.AtmId
                                };
                                Transaction transaction2 = new Transaction()
                                {
                                    Account = accountDestiny,
                                    BalanceBefore = accountDestiny.Balance,
                                    BalanceAfter = accountDestiny.Balance -= accountId.Amount,
                                    Amount = accountId.Amount,
                                    TxTypeId = 3,
                                    AccountTypeName = "Credit",
                                    AtmId = accountId.AtmId
                                };

                                //Account.Balance += accountId.Amount;
                                _context.Transactions.Add(transaction1);
                                _context.Transactions.Add(transaction2);
                                _context.SaveChanges();

                                account = _context.Accounts.FirstOrDefault(s => s.Id == accountId.AccountId);
                                accountDestiny = _context.Accounts.FirstOrDefault(s => s.Id == accountId.AccountIdDestiny);
                                Customer customer1 = _context.Customers.FirstOrDefault(s => s.Id == account.CustomerId);
                                Customer customer2 = _context.Customers.FirstOrDefault(s => s.Id == accountDestiny.CustomerId);
                                Mail.TransferOut(customer1, transaction1);
                                Mail.TransferIn(customer2, transaction2);
                                return Serialization.SerializeTransferResponse(account.Id, account.Balance, "Credit", accountDestiny.Id, 200);
                            }
                            else if (account.CcTypeId == 1 && accountDestiny.CcTypeId == 2) // Credit // Debit
                            {
                                Transaction transaction1 = new Transaction()
                                {
                                    Account = account,
                                    BalanceBefore = account.Balance,
                                    BalanceAfter = account.Balance += accountId.Amount,
                                    Amount = accountId.Amount,
                                    TxTypeId = 4,
                                    AccountTypeName = "Credit",
                                    AtmId = accountId.AtmId
                                };
                                Transaction transaction2 = new Transaction()
                                {
                                    Account = accountDestiny,
                                    BalanceBefore = accountDestiny.Balance,
                                    BalanceAfter = accountDestiny.Balance += accountId.Amount,
                                    Amount = accountId.Amount,
                                    TxTypeId = 3,
                                    AccountTypeName = "Debit",
                                    AtmId = accountId.AtmId
                                };

                                //Account.Balance += accountId.Amount;
                                _context.Transactions.Add(transaction1);
                                _context.Transactions.Add(transaction2);
                                _context.SaveChanges();
                                account = _context.Accounts.FirstOrDefault(s => s.Id == accountId.AccountId);
                                accountDestiny = _context.Accounts.FirstOrDefault(s => s.Id == accountId.AccountIdDestiny);
                                Customer customer1 = _context.Customers.FirstOrDefault(s => s.Id == account.CustomerId);
                                Customer customer2 = _context.Customers.FirstOrDefault(s => s.Id == accountDestiny.CustomerId);
                                Mail.TransferOut(customer1, transaction1);
                                Mail.TransferIn(customer2, transaction2);
                                return Serialization.SerializeTransferResponse(account.Id, account.Balance, "Credit", accountDestiny.Id, 200);
                            }
                            //------
                            if (account.CcTypeId == 2 && accountDestiny.CcTypeId == 2 && account.Balance >= accountId.Amount) // Debit // Debit
                            {
                                Transaction transaction1 = new Transaction()
                                {
                                    Account = account,
                                    BalanceBefore = account.Balance,
                                    BalanceAfter = account.Balance -= accountId.Amount,
                                    Amount = accountId.Amount,
                                    TxTypeId = 4,
                                    AccountTypeName = "Debit",
                                    AtmId = accountId.AtmId
                                };
                                Transaction transaction2 = new Transaction()
                                {
                                    Account = accountDestiny,
                                    BalanceBefore = accountDestiny.Balance,
                                    BalanceAfter = accountDestiny.Balance += accountId.Amount,
                                    Amount = accountId.Amount,
                                    TxTypeId = 3,
                                    AccountTypeName = "Debit",
                                    AtmId = accountId.AtmId
                                };

                                //Account.Balance += accountId.Amount;
                                _context.Transactions.Add(transaction1);
                                _context.Transactions.Add(transaction2);
                                _context.SaveChanges();
                                account = _context.Accounts.FirstOrDefault(s => s.Id == accountId.AccountId);
                                accountDestiny = _context.Accounts.FirstOrDefault(s => s.Id == accountId.AccountIdDestiny);
                                Customer customer1 = _context.Customers.FirstOrDefault(s => s.Id == account.CustomerId);
                                Customer customer2 = _context.Customers.FirstOrDefault(s => s.Id == accountDestiny.CustomerId);
                                Mail.TransferOut(customer1, transaction1);
                                Mail.TransferIn(customer2, transaction2);
                                return Serialization.SerializeTransferResponse(account.Id, account.Balance, "Debit", accountDestiny.Id, 200);
                            }
                            else if (account.CcTypeId == 2 && accountDestiny.CcTypeId == 1 && account.Balance >= accountId.Amount) // Debit // Credit
                            {
                                Transaction transaction1 = new Transaction()
                                {
                                    Account = account,
                                    BalanceBefore = account.Balance,
                                    BalanceAfter = account.Balance -= accountId.Amount,
                                    Amount = accountId.Amount,
                                    TxTypeId = 4,
                                    AccountTypeName = "Debit",
                                    AtmId = accountId.AtmId
                                };
                                Transaction transaction2 = new Transaction()
                                {
                                    Account = accountDestiny,
                                    BalanceBefore = accountDestiny.Balance,
                                    BalanceAfter = accountDestiny.Balance -= accountId.Amount,
                                    Amount = accountId.Amount,
                                    TxTypeId = 3,
                                    AccountTypeName = "Credit",
                                    AtmId = accountId.AtmId
                                };

                                //Account.Balance += accountId.Amount;
                                _context.Transactions.Add(transaction1);
                                _context.Transactions.Add(transaction2);
                                _context.SaveChanges();
                                account = _context.Accounts.FirstOrDefault(s => s.Id == accountId.AccountId);
                                accountDestiny = _context.Accounts.FirstOrDefault(s => s.Id == accountId.AccountIdDestiny);
                                Customer customer1 = _context.Customers.FirstOrDefault(s => s.Id == account.CustomerId);
                                Customer customer2 = _context.Customers.FirstOrDefault(s => s.Id == accountDestiny.CustomerId);
                                Mail.TransferOut(customer1, transaction1);
                                Mail.TransferIn(customer2, transaction2);
                                return Serialization.SerializeTransferResponse(account.Id, account.Balance, "Debit", accountDestiny.Id, 200);
                            }
                            else
                            {
                                return Serialization.GeneralResponse(701).ToString();
                            }
                        }
                    }
                }
                else if (api == "ChangePin")
                {
                    logText.Transaction = api;
                    ChangePin change = Serialization.DeserializeChangePin(xmlText);
                    ChangePin changePin = new ChangePin
                    {
                        Account = change.Account,
                        CurrentPin = change.CurrentPin,
                        NewPin = change.NewPin,
                        AtmId = change.AtmId
                    };
                    var md5OUT = GenerateKey.MD5(Serialization.SerializeChangePin(changePin.Account, changePin.CurrentPin, changePin.NewPin, changePin.AtmId));
                    logText.Md5OUT = md5OUT;
                    Log.SaveIn(logText);

                    if (md5OUT != change.Key)
                    {
                        return Serialization.GeneralResponse(601).ToString();
                    }
                    else
                    {
                        var creditCard = _context.CreditCards.FirstOrDefault(s => s.AccountId == changePin.Account);
                        if (creditCard == null)
                        {
                            return Serialization.GeneralResponse(801).ToString();
                        }
                        else if (creditCard.PinNumber != changePin.CurrentPin)
                        {
                            return Serialization.GeneralResponse(801).ToString();
                        }
                        else
                        {
                            creditCard.PinNumber = changePin.NewPin;
                            _context.SaveChanges();

                            Account account = _context.Accounts.FirstOrDefault(s => s.Id == creditCard.AccountId);
                            Customer customer = _context.Customers.FirstOrDefault(s => s.Id == account.CustomerId);
                            Mail.ChangePin(customer);
                            return Serialization.SerializeChangePinResponse(creditCard.AccountId, 200).ToString();
                        }
                    }
                }
                else if (api == "PayEnee")
                {
                    logText.Transaction = api;
                    var enee = Serialization.DeserializePayEnee(xmlText);
                    PayEnee payEnee = new PayEnee
                    {
                        AccountId = enee.AccountId,
                        BillId = enee.BillId,
                        AtmId = enee.AtmId
                    };
                    var md5OUT = GenerateKey.MD5(Serialization.SerializePayEnee(payEnee.AccountId, payEnee.BillId, payEnee.AtmId));
                    logText.Md5OUT = md5OUT;
                    Log.SaveIn(logText);

                    if (md5OUT != enee.Key)
                    {
                        return Serialization.GeneralResponse(601).ToString();
                    }
                    else
                    {
                        Account account = _context.Accounts.FirstOrDefault(s => s.Id == enee.AccountId);
                        var Enee = _context.Enee.FirstOrDefault(s => s.Id == enee.BillId);
                        if (account == null)
                        {
                            return Serialization.GeneralResponse(901).ToString();
                        }
                        else if (Enee == null)
                        {
                            return Serialization.GeneralResponse(902).ToString();
                        }
                        else
                        {
                            if (Enee.BillAmount > account.Balance && account.CcTypeId == 2) // Debito y no tengo saldo suficiente
                            {
                                return Serialization.GeneralResponse(903).ToString();
                            }
                            else if (Enee.Status == 1)
                            {
                                return Serialization.GeneralResponse(904).ToString();
                            }
                            else if (Enee.BillAmount <= account.Balance && account.CcTypeId == 2) // Debito y tengo saldo suficiente
                            {
                                Enee.Status = 1;
                                Enee.AccountId = account.Id;
                                Transaction transaction = new Transaction()
                                {
                                    Account = account,
                                    BalanceBefore = account.Balance,
                                    BalanceAfter = account.Balance -= Enee.BillAmount,
                                    Amount = Enee.BillAmount,
                                    TxTypeId = 5,
                                    AccountTypeName = "Debit",
                                    BillingName = "Enee",
                                    BillingId = Enee.Id,
                                    AtmId = enee.AtmId
                                };

                                //Account.Balance -= accountId.Amount;
                                _context.Transactions.Add(transaction);
                                _context.SaveChanges();
                                account = _context.Accounts.FirstOrDefault(s => s.Id == enee.AccountId);
                                Customer customer = _context.Customers.FirstOrDefault(s => s.Id == account.CustomerId);
                                Mail.PayService(customer, transaction);
                                return Serialization.SerializePayEneeResponse(account.Id, account.Balance, "Debit", 200);
                            }
                            else if (account.CcTypeId == 1) // Credit
                            {
                                Enee.Status = 1;
                                Enee.AccountId = account.Id;
                                Transaction transaction = new Transaction()
                                {
                                    Account = account,
                                    BalanceBefore = account.Balance,
                                    BalanceAfter = account.Balance += Enee.BillAmount,
                                    Amount = Enee.BillAmount,
                                    TxTypeId = 5,
                                    AccountTypeName = "Credit",
                                    BillingName = "Enee",
                                    BillingId = Enee.Id,
                                    AtmId = enee.AtmId
                                };

                                //Account.Balance -= accountId.Amount;
                                _context.Transactions.Add(transaction);
                                _context.SaveChanges();

                                account = _context.Accounts.FirstOrDefault(s => s.Id == enee.AccountId);
                                Customer customer = _context.Customers.FirstOrDefault(s => s.Id == account.CustomerId);
                                Mail.PayService(customer, transaction);
                                return Serialization.SerializePayEneeResponse(account.Id, account.Balance, "Credit", 200);
                            }
                        }
                    }
                }
                else if (api == "PayHondutel")
                {
                    logText.Transaction = api;
                    var hondutel = Serialization.DeserializePayHondutel(xmlText);
                    PayHondutel payHondutel = new PayHondutel
                    {
                        AccountId = hondutel.AccountId,
                        BillId = hondutel.BillId,
                        AtmId = hondutel.AtmId
                    };
                    var md5OUT = GenerateKey.MD5(Serialization.SerializePayHondutel(payHondutel.AccountId, payHondutel.BillId, payHondutel.AtmId));
                    logText.Md5OUT = md5OUT;
                    Log.SaveIn(logText);

                    if (md5OUT != hondutel.Key)
                    {
                        return Serialization.GeneralResponse(601).ToString();
                    }
                    else
                    {
                        Account account = _context.Accounts.FirstOrDefault(s => s.Id == hondutel.AccountId);
                        var Hondutel = _context.Hondutel.FirstOrDefault(s => s.Id == hondutel.BillId);
                        if (account == null)
                        {
                            return Serialization.GeneralResponse(901).ToString();
                        }
                        else if (Hondutel == null)
                        {
                            return Serialization.GeneralResponse(902).ToString();
                        }
                        else
                        {
                            if (Hondutel.BillAmount > account.Balance && account.CcTypeId == 2) // Debito y no tengo saldo suficiente
                            {
                                return Serialization.GeneralResponse(903).ToString();
                            }
                            else if (Hondutel.Status == 1)
                            {
                                return Serialization.GeneralResponse(904).ToString();
                            }
                            else if (Hondutel.BillAmount <= account.Balance && account.CcTypeId == 2) // Debito y tengo saldo suficiente
                            {
                                Hondutel.Status = 1;
                                Hondutel.AccountId = account.Id;
                                Transaction transaction = new Transaction()
                                {
                                    Account = account,
                                    BalanceBefore = account.Balance,
                                    BalanceAfter = account.Balance -= Hondutel.BillAmount,
                                    Amount = Hondutel.BillAmount,
                                    TxTypeId = 5,
                                    AccountTypeName = "Debit",
                                    BillingName = "Hondutel",
                                    BillingId = Hondutel.Id,
                                    AtmId = hondutel.AtmId
                                };

                                //Account.Balance -= accountId.Amount;
                                _context.Transactions.Add(transaction);
                                _context.SaveChanges();

                                account = _context.Accounts.FirstOrDefault(s => s.Id == hondutel.AccountId);
                                Customer customer = _context.Customers.FirstOrDefault(s => s.Id == account.CustomerId);
                                Mail.PayService(customer, transaction);
                                return Serialization.SerializePayHondutelResponse(account.Id, account.Balance, "Debit", 200);
                            }
                            else if (account.CcTypeId == 1) // Credit
                            {
                                Hondutel.Status = 1;
                                Hondutel.AccountId = account.Id;
                                Transaction transaction = new Transaction()
                                {
                                    Account = account,
                                    BalanceBefore = account.Balance,
                                    BalanceAfter = account.Balance += Hondutel.BillAmount,
                                    Amount = Hondutel.BillAmount,
                                    TxTypeId = 5,
                                    AccountTypeName = "Credit",
                                    BillingName = "Hondutel",
                                    BillingId = Hondutel.Id,
                                    AtmId = hondutel.AtmId
                                };

                                //Account.Balance -= accountId.Amount;
                                _context.Transactions.Add(transaction);
                                _context.SaveChanges();

                                account = _context.Accounts.FirstOrDefault(s => s.Id == hondutel.AccountId);
                                Customer customer = _context.Customers.FirstOrDefault(s => s.Id == account.CustomerId);
                                Mail.PayService(customer, transaction);
                                return Serialization.SerializePayHondutelResponse(account.Id, account.Balance, "Credit", 200);
                            }
                        }
                    }
                }
                else if (api == "PaySanaa")
                {
                    logText.Transaction = api;
                    var sanaa = Serialization.DeserializePaySanaa(xmlText);
                    PaySanaa paySanaa = new PaySanaa
                    {
                        AccountId = sanaa.AccountId,
                        BillId = sanaa.BillId,
                        AtmId = sanaa.AtmId
                    };
                    var md5OUT = GenerateKey.MD5(Serialization.SerializePaySanaa(paySanaa.AccountId, paySanaa.BillId, paySanaa.AtmId));
                    logText.Md5OUT = md5OUT;
                    Log.SaveIn(logText);

                    if (md5OUT != sanaa.Key)
                    {
                        return Serialization.GeneralResponse(601).ToString();
                    }
                    else
                    {
                        Account account = _context.Accounts.FirstOrDefault(s => s.Id == sanaa.AccountId);
                        var Sanaa = _context.Sanaa.FirstOrDefault(s => s.Id == sanaa.BillId);
                        if (account == null)
                        {
                            return Serialization.GeneralResponse(901).ToString();
                        }
                        else if (Sanaa == null)
                        {
                            return Serialization.GeneralResponse(902).ToString();
                        }
                        else
                        {
                            if (Sanaa.BillAmount > account.Balance && account.CcTypeId == 2) // Debito y no tengo saldo suficiente
                            {
                                return Serialization.GeneralResponse(903).ToString();
                            }
                            else if (Sanaa.Status == 1)
                            {
                                return Serialization.GeneralResponse(904).ToString();
                            }
                            else if (Sanaa.BillAmount <= account.Balance && account.CcTypeId == 2) // Debito y tengo saldo suficiente
                            {
                                Sanaa.Status = 1;
                                Sanaa.AccountId = account.Id;
                                Transaction transaction = new Transaction()
                                {
                                    Account = account,
                                    BalanceBefore = account.Balance,
                                    BalanceAfter = account.Balance -= Sanaa.BillAmount,
                                    Amount = Sanaa.BillAmount,
                                    TxTypeId = 5,
                                    AccountTypeName = "Debit",
                                    BillingName = "Sanaa",
                                    BillingId = Sanaa.Id,
                                    AtmId = sanaa.AtmId
                                };

                                //Account.Balance -= accountId.Amount;
                                _context.Transactions.Add(transaction);
                                _context.SaveChanges();

                                account = _context.Accounts.FirstOrDefault(s => s.Id == sanaa.AccountId);
                                Customer customer = _context.Customers.FirstOrDefault(s => s.Id == account.CustomerId);
                                Mail.PayService(customer, transaction);
                                return Serialization.SerializePaySanaaResponse(account.Id, account.Balance, "Debit", 200);
                            }
                            else if (account.CcTypeId == 1) // Credit
                            {
                                Sanaa.Status = 1;
                                Sanaa.AccountId = account.Id;
                                Transaction transaction = new Transaction()
                                {
                                    Account = account,
                                    BalanceBefore = account.Balance,
                                    BalanceAfter = account.Balance += Sanaa.BillAmount,
                                    Amount = Sanaa.BillAmount,
                                    TxTypeId = 5,
                                    AccountTypeName = "Credit",
                                    BillingName = "Sanaa",
                                    BillingId = Sanaa.Id,
                                    AtmId = sanaa.AtmId
                                };

                                //Account.Balance -= accountId.Amount;
                                _context.Transactions.Add(transaction);
                                _context.SaveChanges();

                                account = _context.Accounts.FirstOrDefault(s => s.Id == sanaa.AccountId);
                                Customer customer = _context.Customers.FirstOrDefault(s => s.Id == account.CustomerId);
                                Mail.PayService(customer, transaction);
                                return Serialization.SerializePaySanaaResponse(account.Id, account.Balance, "Credit", 200);
                            }
                        }
                    }
                }
                else if (api == "MyTransactions")
                {
                    logText.Transaction = api;
                    MyTransactions myTransactions = Serialization.DeserializeMyTransactions(xmlText);
                    MyTransactions transactions = new MyTransactions
                    {
                        AccountId = myTransactions.AccountId,
                        AtmId = myTransactions.AtmId
                    };
                    var md5OUT = GenerateKey.MD5(Serialization.SerializeMyTransactions(transactions.AccountId, transactions.AtmId));
                    logText.Md5OUT = md5OUT;
                    Log.SaveIn(logText);

                    if (md5OUT != myTransactions.Key)
                    {
                        return Serialization.GeneralResponse(601).ToString();
                    }
                    else
                    {
                        Account account = _context.Accounts.FirstOrDefault(s => s.Id == myTransactions.AccountId);
                        if (account == null)
                        {
                            return Serialization.GeneralResponse(501).ToString();
                        }
                        else
                        {
                            Customer customer = _context.Customers.FirstOrDefault(s => s.Id == account.CustomerId);
                            List<Transaction> transactionsList = _context.Transactions.Where
                                (s => s.AtmId == myTransactions.AtmId && s.AccountId == myTransactions.AccountId)
                                .OrderBy(x => x.CreatedAt).ToList();
                            if (transactionsList != null)
                            {
                                Mail.MyTransactions(customer, transactionsList);
                                return Serialization.SerializeMyTransactionsResponse(account.Id, 201).ToString();
                            }
                            else
                            {
                                return Serialization.GeneralResponse(999).ToString();
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
