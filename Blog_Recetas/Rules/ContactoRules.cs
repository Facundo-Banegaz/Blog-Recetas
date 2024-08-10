using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;


namespace Blog_Recetas.Models
{
    public class ContactoRules
    {
        private readonly IConfiguration _configuration;

        public ContactoRules(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public void SendEmail(string emailTo, string mensaje, string asunto, string? deQuienNombre, string? deQuienEmail)
        {
            var from = deQuienEmail ?? _configuration["Email:fromEmail"];
            var fromName = deQuienNombre ?? _configuration["Email:fromName"];

            // create email message
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(fromName, from));
            email.To.Add(MailboxAddress.Parse(emailTo));
            email.Subject = asunto;
            email.Body = new TextPart(TextFormat.Html) { Text = mensaje };

            // send email
            using var smtp = new SmtpClient();
            smtp.Connect(_configuration["Email:smtpServer"], int.Parse(_configuration["Email:smtpPort"]));
            smtp.Authenticate(_configuration["Email:fromEmail"], _configuration["Email:password"]);
            smtp.Send(email);
            smtp.Disconnect(true);
        }




    }
}
