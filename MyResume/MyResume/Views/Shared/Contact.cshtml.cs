using Microsoft.AspNetCore.Mvc.RazorPages;
using MimeKit;
using MailKit.Net.Smtp;
using System;

namespace MyResume.Models
{
    public class ContactModel : PageModel
    {
        
        public string ContactMessage = "Schedule Appointment";
        public bool IsSubmited { get; set; }

        public void OnGet()
        {
            
        }
        //---------
        public void OnPost()
        {
            SendingEmailOnClick(out string returnMessage);
        }
        public bool SendingEmailOnClick(out string ReturnMessage)
        {
            var client = new SmtpClient();
            var EmailMessage = new MimeMessage();
            ReturnMessage = string.Empty;
            IsSubmited = false;
            //----------------
            string emailSubject;
            string CustomerName;
            string CustomerPhone;
            string CustomerEmailAddress;
            string Customermessage;
            //----------------
            emailSubject = "Salon Customer Email";
            CustomerName = Request.Form["name"].ToString();
            CustomerPhone = Request.Form["phone"].ToString();
            CustomerEmailAddress = Request.Form["emailAddress"].ToString();
            Customermessage = Request.Form["messages"].ToString();
            Customermessage = Customermessage + "\n" + $"Phone:{CustomerPhone}";
            //----------------
            
            try
            {
                EmailMessage.From.Add(new MailboxAddress(CustomerName, CustomerEmailAddress));
                EmailMessage.To.Add(new MailboxAddress("richard", "richqualitydevelopment@gmail.com"));
                EmailMessage.Subject = emailSubject;
                EmailMessage.Body = new TextPart("")
                {
                    Text = Customermessage
                };
                client.Connect("smtp.mail.yahoo.com", 587, false);
                client.Authenticate("rahayn@yahoo.com", "b00mb00m1");
                client.Send(EmailMessage);

                ReturnMessage = "Success!";
                return IsSubmited = true;
            }

            catch(Exception ex)
            {
                ReturnMessage = $"Something Went Wrong:{ex.ToString()}";
          
                return IsSubmited;
            }
            finally
            {
                client.Dispose();
            }
            
            
        }
    }
}
