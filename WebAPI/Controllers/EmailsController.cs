using Core.Utilities.Mail;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailsController : ControllerBase
    {
        IEmailService _emailService;
        IConfiguration _configuration;

        public EmailsController(IEmailService emailService, IConfiguration configuration)
        {
            _emailService = emailService;
            _configuration = configuration;
        }

        [HttpPost("send")]
        public IActionResult Send(EmailMessage emailMessage)
        {
            if (emailMessage.ToAddresses.Count == 0)
            {
                emailMessage.ToAddresses.Add(new EmailAddress
                {
                    Name = _configuration.GetSection("EmailConfiguration").GetSection("ClientName").Value,
                    Address = _configuration.GetSection("EmailConfiguration").GetSection("SmtpUsername").Value
                });
            }

            var result = _emailService.Send(emailMessage);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("receive")]
        public IActionResult ReceiveEmails(int maxcount = 10)
        {
            var result = _emailService.ReceiveEmail(maxcount);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
