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
    public class FooterMailController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<FooterMailController> _logger;

        public FooterMailController(IConfiguration configuration, ILogger<FooterMailController> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult FooterSendEmail(FooterContactFormModel model)
        {
            try
            {
                var email = model.Email;
                var message = model.Message;
                using (var smtpClient = new MailKit.Net.Smtp.SmtpClient())
                {
                    smtpClient.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    smtpClient.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                    smtpClient.Authenticate("gearventuresnepal@gmail.com", "funo kfkr bfie vngi");

                    if (!string.IsNullOrEmpty(email) )
                    {
                        var mailMessage = new MimeMessage
                        {
                            From = { new MailboxAddress("Sender Name", email) },
                            Subject = "Contact Message",
                            Body = new TextPart("plain")
                            {
                                Text = $"Email: {email}\nMessage: {message}"
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
