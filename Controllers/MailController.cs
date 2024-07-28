using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using System;
using GearVentures.Models;

namespace GearVentures.Controllers
{
    public class MailController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<MailController> _logger;

        public MailController(IConfiguration configuration, ILogger<MailController> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult SendEmail(ContactFormModel model)
        {
            try
            {
                var name = model.Name;
                var email = model.Email;
                var message = model.Message;
                using (var smtpClient = new MailKit.Net.Smtp.SmtpClient())
                {
                    smtpClient.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    smtpClient.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                    smtpClient.Authenticate("gearventuresnepal@gmail.com", "funo kfkr bfie vngi");

                    if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(name))
                    {
                        var mailMessage = new MimeMessage
                        {
                            From = { new MailboxAddress(name, email) },
                            Subject = "Contact Message",
                            Body = new TextPart("plain")
                            {
                                Text = $"Name: {name}\nEmail: {email}\nMessage: {message}"
                            }
                        };

                        mailMessage.To.Add(new MailboxAddress("GearVenturesNepal", "gearventuresnepal@gmail.com"));
                        smtpClient.Send(mailMessage);

                        return Json(new { success = true });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Error: Please fill in the required fields." });
                    }

                    
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error sending email: {ex.Message}");
                return Json(new { success = false, message = "Failed to send message. Please try again later." });
            }
        }
    }
}
