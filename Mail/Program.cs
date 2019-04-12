using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Mail
{
    class Program
    {
        static void Main(string[] args)
        {
            string Msg = "Hi ! This is DailyMailSchedulerService mail.";//whatever msg u want to send write here.  
                                                                        // Here you can write the   
            SendEmail("anamika.sawhney22@gmail.com", "anamika_sawhney@rediffmail.com", "a@live.com", "Daily Report of DailyMailSchedulerService on " + DateTime.Now.ToString("dd-MMM-yyyy"), Msg);
        }

        public static void SendEmail(String ToEmail, string cc, string bcc, String Subj, string Message)
        {
            //Reading sender Email credential from web.config file  
            
            string HostAdd = ConfigurationManager.AppSettings["Host"].ToString();
            string FromEmailid = ConfigurationManager.AppSettings["FromMail"].ToString();
            string Pass = ConfigurationManager.AppSettings["Password"].ToString();
            //creating the object of MailMessage  

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(FromEmailid); //From Email Id  
            mailMessage.Subject = Subj; //Subject of Email  
            mailMessage.Body = Message; //body or message of Email  
            //mailMessage.IsBodyHtml = true;

            string[] ToMuliId = ToEmail.Split(',');
            foreach (string ToEMailId in ToMuliId)
            {
                mailMessage.To.Add(new MailAddress(ToEMailId)); //adding multiple TO Email Id  
            }


            string[] CCId = cc.Split(',');

            foreach (string CCEmail in CCId)
            {
                mailMessage.CC.Add(new MailAddress(CCEmail)); //Adding Multiple CC email Id  
            }

            string[] bccid = bcc.Split(',');

            foreach (string bccEmailId in bccid)
            {
                mailMessage.Bcc.Add(new MailAddress(bccEmailId)); //Adding Multiple BCC email Id  
            }
            SmtpClient smtp = new SmtpClient();  // creating object of smptpclient  
            smtp.Host = HostAdd;              //host of emailaddress for example smtp.gmail.com etc  

            //network and security related credentials  

            smtp.EnableSsl = true ;
            NetworkCredential NetworkCred = new NetworkCredential();
            NetworkCred.UserName = mailMessage.From.Address;
//            Console.WriteLine(NetworkCred.UserName);
            NetworkCred.Password = Pass;
  //          smtp.UseDefaultCredentials = true;
            smtp.Credentials = NetworkCred;
            smtp.Port = 587;
            smtp.Send(mailMessage); //sending Email  
        }

    }
}
