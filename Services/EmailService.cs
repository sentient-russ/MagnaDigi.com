using magnadigi.Controllers;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System.Diagnostics;

namespace magnadigi.Services
{
    public class EmailService
    {
        private string? emailPass = Environment.GetEnvironmentVariable("MD_Email_Pass");
        private string? emailAddress = Environment.GetEnvironmentVariable("MD_Email_Address");
        private string? emailServer = Environment.GetEnvironmentVariable("MD_Email_Server");
        public String SendContactMessage(ContactDataModel complexDataIn)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("cs@magnadigi.com"));
            email.To.Add(MailboxAddress.Parse("cs@magnadigi.com"));
            email.Subject = "MagnaDigi Contact Priority Level: " + complexDataIn.priorityLevel;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = "<p><strong>Contact Name: </strong>" + complexDataIn.Name + "</p>" +
              "<p><strong>Business Name: </strong>" + complexDataIn.Business + "</p>" +
              "<p><strong>Contact Number: </strong>" + complexDataIn.Phone + "</p>" +
              "<p><strong>Contact Email: </strong>" + complexDataIn.Email + "</p>" +
              "<p><strong>Message: </strong>" + complexDataIn.Message + "</p>" +
              "<p><strong>Priority Level: </strong>" + complexDataIn.priorityLevel + "</p>" +
              "<p><strong>Project Start Date: </strong>" + complexDataIn.startDate + "</p>"
            };
            using var smtp = new SmtpClient();
            smtp.Connect(emailServer, 465, SecureSocketOptions.Auto);
            smtp.Authenticate(emailAddress, emailPass);
            var response = smtp.Send(email);
            smtp.Disconnect(true);
            Debug.WriteLine($"Email sent to: {email.To}, Subject: {email.Subject}, Body: {email.Body}");
            return response;
        }
    }
}
