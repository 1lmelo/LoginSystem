using LoginSystem.Enum;
using LoginSystem.Models;
using LoginSystem.Models.Interfaces;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using System.ComponentModel;

namespace LoginSystem.Utils
{
    public class Util : IUtil
    {
        public EmailSettings _emailSettings { get; }

        public Util(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public string EncryptPassword(string password)
        {
            if (password != null)
            {
                MD5 md5 = System.Security.Cryptography.MD5.Create();
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(password);
                byte[] hash = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    sb.Append(hash[i].ToString("X2"));
                }
                return sb.ToString();
            }
            return string.Empty;
        }

        public string ShowAlert(Alerts obj, string message)
        {
            string alertDiv = string.Empty;
            switch (obj)
            {
                case Alerts.Success:
                    alertDiv = "<div class='alert alert-success alert-dismissable' id='alert'><button type='button' class='close' data-dismiss='alert'>×</button><strong> Success!</ strong > " + message + "</a>.</div>";
                    break;
                case Alerts.Danger:
                    alertDiv = "<div class='alert alert-danger alert-dismissible' id='alert'><button type='button' class='close' data-dismiss='alert'>×</button><strong> Error!</ strong > " + message + "</a>.</div>";
                    break;
                case Alerts.Info:
                    alertDiv = "<div class='alert alert-info alert-dismissable' id='alert'><button type='button' class='close' data-dismiss='alert'>×</button><strong> Info!</ strong > " + message + "</a>.</div>";
                    break;
                case Alerts.Warning:
                    alertDiv = "<div class='alert alert-warning alert-dismissable' id='alert'><button type='button' class='close' data-dismiss='alert'>×</button><strong> Warning!</strong> " + message + "</a>.</div>";
                    break;
            }
            return alertDiv;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                Execute(email, subject, message).Wait();
                return Task.FromResult(0);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Execute(string email, string subject, string message)
        {
            try
            {
                string toEmail = string.IsNullOrEmpty(email) ? _emailSettings.ToEmail : email;

                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(_emailSettings.UsernameEmail, "System Login")
                };

                mail.To.Add(new MailAddress(toEmail));
                mail.CC.Add(new MailAddress(_emailSettings.CcEmail));

                mail.Subject = "System Login - " + subject;
                mail.Body = message;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;


                using (SmtpClient smtp = new SmtpClient(_emailSettings.PrimaryDomain, _emailSettings.PrimaryPort))
                {
                    smtp.Credentials = new NetworkCredential(_emailSettings.UsernameEmail, _emailSettings.UsernamePassword);
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(mail);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
