using Blackbox.Server.DataConn;
using Blackbox.Server.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Windows.Forms;

namespace Blackbox.Server.src
{
    public class Mail
    {
        private static DataContext _context = new DataContext();

        public static void Send(string to, string subject, string body, bool isBodyHtml = false)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("atm.proyecto.seguridad@gmail.com");
                mail.To.Add(to);
                mail.Subject = subject;

                mail.IsBodyHtml = isBodyHtml;
                mail.Body = body;

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("atm.proyecto.seguridad", "Pr0y3ct01");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                //MessageBox.Show("Mail-Sent");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        internal static void CcPinNumber(Customer customer)
        {
            string str = "Hola " + customer.LastName + "," + customer.FirstName;
            str += "\n Hemos notado que ha iniciado sesion recientemente, si no lo hizo usted, por favor ponerse en contacto con Soporte tecnico a la brevedad.";
            str += "\n Gracias por su preferencia.";
            Send(customer.Email, "Inicio de Sesion Reciente", str);
        }

        internal static void Withdraw(Customer customer, Transaction transaction)
        {
            string str = "Hola " + customer.LastName + "," + customer.FirstName;
            str += "\n Retiro realizado exitosamente por la cantidad de $" + transaction.Amount + ".";
            str += "\n Su nuevo saldo es de $" + transaction.BalanceAfter;
            str += "\n Gracias por su preferencia.";
            Send(customer.Email, "Retiro de Dinero", str);
        }

        internal static void Deposit(Customer customer, Transaction transaction)
        {
            string str = "Hola " + customer.LastName + "," + customer.FirstName;
            str += "\n Deposito recibo por la cantidad de $" + transaction.Amount + ".";
            str += "\n Su nuevo saldo es de $" + transaction.BalanceAfter;
            str += "\n Gracias por su preferencia.";
            Send(customer.Email, "Deposito de Dinero", str);
        }

        internal static void TransferOut(Customer customer, Transaction transaction)
        {
            string str = "Hola " + customer.LastName + "," + customer.FirstName;
            str += "\n Transferencia realizada por la cantidad de $" + transaction.Amount + " fue exitosa.";
            str += "\n Su nuevo saldo es de $" + transaction.BalanceAfter;
            str += "\n Gracias por su preferencia.";
            Send(customer.Email, "Transferencia de Dinero", str);
        }

        internal static void TransferIn(Customer customer, Transaction transaction)
        {
            string str = "Hola " + customer.LastName + "," + customer.FirstName;
            str += "\n Ha recibido la cantidad de $" + transaction.Amount + " mediante una transferencia electronica.";
            str += "\n Su nuevo saldo es de $" + transaction.BalanceAfter;
            str += "\n Gracias por su preferencia.";
            Send(customer.Email, "Transferencia de Dinero", str);
        }

        internal static void ChangePin(Customer customer)
        {
            string str = "Hola " + customer.LastName + "," + customer.FirstName;
            str += "\n Hemos notado que ha cambiado su pin.";
            str += "\n Si usted realizo este cambio, haga caso omiso a este correo, sino llamenos inmediatamente.";
            str += "\n Gracias por su preferencia.";
            Send(customer.Email, "Cambio de Pin", str);
        }

        internal static void PayService(Customer customer, Transaction transaction)
        {
            string str = "Hola " + customer.LastName + "," + customer.FirstName;
            str += "\n Hemos recibod su pago por $" + transaction.Amount;
            str += "\n Para el servicio de " + transaction.BillingName;
            str += "\n Gracias por su preferencia.";
            Send(customer.Email, "Pago Servicio Publico", str);
        }

        internal static void MyTransactions(Customer customer, List<Transaction> transactionsList)
        {
            string str = "<!DOCTYPE html>" +
                "<html>" +
                "<head>" +
                "<style>table{border-collapse: collapse; width:100%;}" +
                "td, th {border: 1px solid #dddddd; text-align: left; padding: 8px;}" +
                "</style></head>" +
                "<body>" +
                "<p>Hola " + customer.LastName + "," + customer.FirstName + "</p>";
            str += "<p> A continuacion se listan las transacciones realizadas en este cajero:</p>";

            str += "\n <table>" +
                "<tr>" +
                "<th>Trx Type</th>" +
                "<th>Account Type</th>" +
                "<th>Balance Before</th>" +
                "<th>balance After</th>" +
                "<th>Amount</th>" +
                "<th>Date and Time</th>" +
                "<th>Service Name</th>" +
                "<th>ATM Machine</th>" +
                "</tr>";
            foreach (var tx in transactionsList)
            {
                str += "<tr>" +
                    "<td>" + _context.TxTypes.FirstOrDefault(s => s.Id == tx.TxTypeId).TypeName + "</td>" +
                    "<td>" + tx.AccountTypeName + "</td>" +
                    "<td>" + tx.BalanceBefore + "</td>" +
                    "<td>" + tx.BalanceAfter + "</td>" +
                    "<td>" + tx.Amount + "</td>" +
                    "<td>" + tx.CreatedAt + "</td>" +
                    "<td>" + tx.BillingName + "</td>" +
                    "<td>" + tx.AtmId + "</td>" +
                    "</tr>";
            }
                str += "</table></body></html>";

            str += "<p>Gracias por su preferencia.</p>";
            Send(customer.Email, "Listado de Tranacciones", str, true);
        }
    }
}
