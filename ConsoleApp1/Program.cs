using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Vastaad");
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("siddhant.gosavi23994@gmail.com");
                mail.To.Add("siddhant.gosavi@rediffmail.com");
                mail.Subject = "Test Mail";
                mail.Body = "This is for testing SMTP mail from GMAIL";

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("siddhant.gosavi23994@gmail.com", "doffjsmy23994");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                //MessageBox.Show("mail Send");
                Console.WriteLine("Message Sent");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
