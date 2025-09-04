using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Api.Models;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Portfolio.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactController : ControllerBase
{
    private readonly IConfiguration _config;

    public ContactController(IConfiguration config)
    {
        _config = config;
    }

    [HttpPost]
    public async Task<IActionResult> Post(ContactRequest request)
    {
        var apiKey = _config["SendGridApiKey"];
        var client = new SendGridClient(apiKey);

        var from = new EmailAddress("lewisnewtonpaine@hotmail.com", "Lewis Paine Portfolio");
        var to = new EmailAddress("lewisnewtonpaine@hotmail.com"); // Where to send messages
        var subject = $"New Contact Message from {request.Name}";
        var plainTextContent = $"Name: {request.Name}\nEmail: {request.Email}\n\n{request.Message}";
        var htmlContent = $"<strong>Name:</strong> {request.Name}<br/><strong>Email:</strong> {request.Email}<br/><br/>{request.Message}";

        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        var response = await client.SendEmailAsync(msg);

        return response.IsSuccessStatusCode ? Ok() : StatusCode(500, "Email failed to send.");
    }
}
