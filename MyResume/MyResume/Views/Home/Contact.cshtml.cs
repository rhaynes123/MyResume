using System;
using MimeKit;
using MailKit.Net.Smtp;
using System.ComponentModel.DataAnnotations;
//using System.Net.Mail;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyResume.Views.Home
{
    public class Contact : PageModel
    {
        public bool IsSubmited { get; set; }
        [Required]
        [EmailAddress]
        [RegularExpression(@"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
            ErrorMessage = "Invalid Email Format")]
        [Display(Name = "Email")]
        public string EmailAddress { get; set; }
        //----------------
        string Subject { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Phone { get; set; }
        //string CustomerEmailAddress;
        [Required]
        public string Message;
        
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
           
            Subject = "Salon Customer Email";
            Name = Request.Form["name"].ToString();
            Phone = Request.Form["phone"].ToString();
            EmailAddress = Request.Form["emailAddress"].ToString();
            Message = Request.Form["messages"].ToString();
            Message = Message + "\n" + $"Phone:{Phone}";
            //----------------

            try
            {
                EmailMessage.From.Add(new MailboxAddress(Name, EmailAddress));
                EmailMessage.To.Add(new MailboxAddress("richard", ""));
                EmailMessage.Subject = Subject;
                EmailMessage.Body = new TextPart("")
                {
                    Text = Message
                };
                client.Connect("smtp.mail.yahoo.com", 587, false);
                client.Authenticate("", "");
                client.Send(EmailMessage);

                ReturnMessage = "Success!";
                return IsSubmited = true;
            }

            catch (Exception ex)
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
