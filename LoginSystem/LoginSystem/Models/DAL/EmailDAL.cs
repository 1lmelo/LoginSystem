using LoginSystem.Models.Interfaces;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;
using LoginSystem.Utils;
using System;
using LoginSystem.Enum;

namespace LoginSystem.Models.DAL
{
    public class EmailDAL : IEmailDAL
    {
        private readonly IUtil _util;

        public EmailDAL(IUtil util)
        {
            _util = util;
        }

        public async Task EmailCreateUser(string email, string name)
        {
            try
            {
                var subject = "Created User";
                var message = $"Hi {name}, your user has been successfully created!";
                await _util.SendEmailAsync(email, subject, message);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}


