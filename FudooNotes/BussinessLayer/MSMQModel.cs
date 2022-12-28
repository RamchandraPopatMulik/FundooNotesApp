using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Experimental.System.Messaging;

namespace FundooModel
{
    public class MSMQModel
    {
        MessageQueue messageQueue = new MessageQueue();
        private string reciverEmailAddress;
        private string reciverName;

        public void SendMessage(string token,string emailId,string name)
        {
            reciverEmailAddress = emailId;
            reciverName = name;
            messageQueue.Path = @".\Private$\Fundoo";
            try
            {
                if(!MessageQueue.Exists(messageQueue.Path))
                {
                    MessageQueue.Create(messageQueue.Path);
                }
                messageQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
                messageQueue.ReceiveCompleted += MessageQueue_ReceiveCompleted;
                messageQueue.Send(token);
                messageQueue.BeginReceive();
                messageQueue.Close();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        private void MessageQueue_ReceiveCompleted(object sender,ReceiveCompletedEventArgs e)
        {
            try
            {
                var msg=messageQueue.EndReceive(e.AsyncResult);
                string token = msg.Body.ToString();
                MailMessage mailMessage = new MailMessage();
                SmtpClient smtp = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    EnableSsl = true,
                    Credentials = new NetworkCredential("mulikram2019@gmail.com", "pqmuwajlydwjsilr")
                };
                mailMessage.From = new MailAddress("mulikram2019@gmail.com");
                mailMessage.To.Add(new MailAddress(reciverEmailAddress));
                string mailBody = $"<!DOCTYPE html>" +
                                    $"<html>" +
                                    $"<style>" +
                                    $".blink" +
                                    $"</style>" +
                                           $"<body style = \"background-color:#FFFFFF;text-align:center;padding:5px;\">" +
                                    $"<h1 style = \"color:#6A8D02; border-bottom: 3px solid #84AF08; margin-top: 5px;\"> Dear <b>{reciverName}</b> </h1>\n" +
                                    $"<h3 style = \"color:#B03A2E ;\"> For Resetting Password The Below Link Is Issued</h3>" +
                                    $"<h3 style = \"#FF0000 ;\"> Please Click The Link Below To Reset Your Password</h3>" +
                                    $"<a style = \"color:#000000; text-decoration: none; font-size:20px;\" href='http://localhost:4200/resetpassword/{token}'>Click me</a>\n" +
                                    $"<h3 style = \"color:#DE3163;margin-bottom:5px;\"><blink>This Token will be Valid For Next 6 Hours<blink></h3>" +
                                    $"</body>" +
                                    $"</html>";

                 mailMessage.Body = mailBody;
                 mailMessage.IsBodyHtml = true;
                 mailMessage.Subject = "PassWord Reset Link";
                 smtp.Send(mailMessage);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
