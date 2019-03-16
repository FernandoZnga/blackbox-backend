using Blackbox.Server.DataConn;
using Blackbox.Server.Domain;
using Blackbox.Server.src;
using System;

namespace Blackbox.Server
{
    internal static class Program
    {
        private static DataContext _context = new DataContext();
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Main());


            // Dummy data and Initial Data
            //InsertDummyData();
            //InsertTrTypeData();
            //InsertEnne();
            //InsertSanaa();
            //InsertHondutel();

            // Run Server
            SocketConn.AsynchronousSocketListener.StartListening();
        }

        private static void InsertEnne()
        {
            var bill1 = new Enee()
            {
                BillAmount = 100
            };
            var bill2 = new Enee()
            {
                BillAmount = 110
            };
            var bill3 = new Enee()
            {
                BillAmount = 120
            };
            var bill4 = new Enee()
            {
                BillAmount = 130
            };
            var bill5 = new Enee()
            {
                BillAmount = 140
            };
            var bill6 = new Enee()
            {
                BillAmount = 150
            };
            var bill7 = new Enee()
            {
                BillAmount = 50
            };
            var bill8 = new Enee()
            {
                BillAmount = 40
            };
            var bill9 = new Enee()
            {
                BillAmount = 30
            };
            var bill10 = new Enee()
            {
                BillAmount = 20
            };
            using (var context = new DataContext())
            {
                context.Enee.AddRange(bill1, bill2, bill3, bill4, bill5, bill6, bill7, bill8, bill9, bill10);
                context.SaveChanges();
            }
        }
        private static void InsertSanaa()
        {
            var bill1 = new Sanaa()
            {
                BillAmount = 100
            };
            var bill2 = new Sanaa()
            {
                BillAmount = 110
            };
            var bill3 = new Sanaa()
            {
                BillAmount = 120
            };
            var bill4 = new Sanaa()
            {
                BillAmount = 130
            };
            var bill5 = new Sanaa()
            {
                BillAmount = 140
            };
            var bill6 = new Sanaa()
            {
                BillAmount = 150
            };
            var bill7 = new Sanaa()
            {
                BillAmount = 50
            };
            var bill8 = new Sanaa()
            {
                BillAmount = 40
            };
            var bill9 = new Sanaa()
            {
                BillAmount = 30
            };
            var bill10 = new Sanaa()
            {
                BillAmount = 20
            };
            using (var context = new DataContext())
            {
                context.Sanaa.AddRange(bill1, bill2, bill3, bill4, bill5, bill6, bill7, bill8, bill9, bill10);
                context.SaveChanges();
            }
        }
        private static void InsertHondutel()
        {
            var bill1 = new Hondutel()
            {
                BillAmount = 100
            };
            var bill2 = new Hondutel()
            {
                BillAmount = 110
            };
            var bill3 = new Hondutel()
            {
                BillAmount = 120
            };
            var bill4 = new Hondutel()
            {
                BillAmount = 130
            };
            var bill5 = new Hondutel()
            {
                BillAmount = 140
            };
            var bill6 = new Hondutel()
            {
                BillAmount = 150
            };
            var bill7 = new Hondutel()
            {
                BillAmount = 50
            };
            var bill8 = new Hondutel()
            {
                BillAmount = 40
            };
            var bill9 = new Hondutel()
            {
                BillAmount = 30
            };
            var bill10 = new Hondutel()
            {
                BillAmount = 20
            };
            using (var context = new DataContext())
            {
                context.Hondutel.AddRange(bill1, bill2, bill3, bill4, bill5, bill6, bill7, bill8, bill9, bill10);
                context.SaveChanges();
            }
        }

        private static void InsertTrTypeData()
        {
            var txTypeWithdraw = new TxType()
            {
                TypeName = "Withdraw"
            };
            var txTypeDeposit = new TxType()
            {
                TypeName = "Deposit"
            };
            var txTypeTransfIn = new TxType()
            {
                TypeName = "Transf-IN"
            };
            var txTypeTransfOut = new TxType()
            {
                TypeName = "Transf-OUT"
            };
            var txTypePayService = new TxType()
            {
                TypeName = "Service Payment"
            };

            using (var context = new DataContext())
            {
                context.TxTypes.AddRange(txTypeWithdraw, txTypeDeposit, txTypeTransfIn, txTypeTransfOut, txTypePayService);
                context.SaveChanges();
            }
        }

        static void InsertDummyData()
        {
            /// A couple of customers
            var customer1 = new Customer()
            {
                FirstName = "Oscar",
                MiddleName = "Fernando",
                LastName = "Zuniga"
            };
            var customer2 = new Customer()
            {
                FirstName = "Ana",
                MiddleName = "Maria",
                LastName = "Morales"
            };

            /// Credit Card Types
            var ccTypeCredit = new AccountType()
            {
                TypeName = "Credit"
            };
            var ccTypeDebit = new AccountType()
            {
                TypeName = "Debit"
            };
            
            /// Credit Cards
            var creditCard1 = new CreditCard()
            {
                CcNumber = GenerateKey.MD5("1111-1111-1111-1111"),
                PinNumber = GenerateKey.MD5("1111")
            };
            var creditCard2 = new CreditCard()
            {
                CcNumber = GenerateKey.MD5("2222-2222-2222-2222"),
                PinNumber = GenerateKey.MD5("2222")
            };
            var creditCard3 = new CreditCard()
            {
                CcNumber = GenerateKey.MD5("3333-3333-3333-3333"),
                PinNumber = GenerateKey.MD5("3333")
            };
            var creditCard4 = new CreditCard()
            {
                CcNumber = GenerateKey.MD5("4444-4444-4444-4444"),
                PinNumber = GenerateKey.MD5("4444")
            };

            /// A couple of accounts
            var accountDebit1 = new Account()
            {
                Customer = customer1,
                CreditCard = creditCard1,
                CcType = ccTypeDebit
            };
            var accountCredit2 = new Account()
            {
                Customer = customer1,
                CreditCard = creditCard2,
                CcType = ccTypeCredit
            };
            var accountDebit3 = new Account()
            {
                Customer = customer2,
                CreditCard = creditCard3,
                CcType = ccTypeDebit
            };
            var accountCredit4 = new Account()
            {
                Customer = customer2,
                CreditCard = creditCard4,
                CcType = ccTypeCredit
            };

            /// Add data to db
            using (var context = new DataContext())
            {
                context.Customers.AddRange(customer1, customer2);
                context.CreditCards.AddRange(creditCard1, creditCard2, creditCard3, creditCard4);
                context.AccountTypes.AddRange(ccTypeCredit, ccTypeDebit);
                context.Accounts.AddRange(accountDebit1, accountCredit2, accountDebit3, accountCredit4);
                context.SaveChanges();
            }
        }
    }
}
