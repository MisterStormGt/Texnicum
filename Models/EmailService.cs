﻿using MailKit.Net.Smtp;
using MimeKit;
using System.Threading.Tasks;

namespace Texnicum.Models
{
    public class EmailService
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            // нужно указать реально существующую почту mail.ru
            emailMessage.From.Add(new MailboxAddress("Администрация сайта", "pavlik.kolegov@mail.ru"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.mail.ru", 465, true);   // SMTP — 465 (протокол шифрования SSL/TLS)

                // почту дублируем из 14 строки, пароль для внешнего приложения нужно сгенерировать в почте mail.ru
                await client.AuthenticateAsync("pavlik.kolegov@mail.ru", "efgtVndMpqBurqSLnTs9");
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}