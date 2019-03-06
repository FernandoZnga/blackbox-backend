using Blackbox.Server.DataConn;
using Blackbox.Server.Domain;
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

            // Run Server
            SocketConn.AsynchronousSocketListener.StartListening();
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

            using (var context = new DataContext())
            {
                context.TxTypes.AddRange(txTypeWithdraw, txTypeDeposit, txTypeTransfIn, txTypeTransfOut);
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
            var ccTypeCredit = new CcType()
            {
                TypeName = "Credit"
            };
            var ccTypeDebit = new CcType()
            {
                TypeName = "Debit"
            };
            
            /// Credit Cards
            var creditCard1 = new CreditCard()
            {
                CcNumber = "1111-1111-1111-1111",
                PinNumber = "1111"
            };
            var creditCard2 = new CreditCard()
            {
                CcNumber = "2222-2222-2222-2222",
                PinNumber = "2222"
            };
            var creditCard3 = new CreditCard()
            {
                CcNumber = "3333-3333-3333-3333",
                PinNumber = "3333"
            };
            var creditCard4 = new CreditCard()
            {
                CcNumber = "4444-4444-4444-4444",
                PinNumber = "4444"
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
                context.CcTypes.AddRange(ccTypeCredit, ccTypeDebit);
                context.Accounts.AddRange(accountDebit1, accountCredit2, accountDebit3, accountCredit4);
                context.SaveChanges();
            }
        }
    }
}
