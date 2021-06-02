using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Preparation
{
    public partial class Mail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                try
                {
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.To.Add("novalexx@yahoo.com");
                    mailMessage.From = new MailAddress("nosytoby@outlook.com");
                    mailMessage.Subject = "ASP.NET e-mail test";
                    mailMessage.Body = "Hello world,\n\nThis is an ASP.NET test e-mail!";
                    SmtpClient smtpClient = new SmtpClient("smtp.comcast.net");
                    smtpClient.Send(mailMessage);
                    Response.Write("E-mail sent!");
                }
                catch (Exception ex)
                {
                    Response.Write("Could not send the e-mail - error: " + ex.Message);
                }

            //mailMessage.Attachments.Add(new Attachment(Server.MapPath("~/image.jpg")));

            //mailMessage.To.Add("your.own@mail-address.com");
            //mailMessage.To.Add("another@mail-address.com");

            //mailMessage.From = new MailAddress("me@mail-address.com", "My Name");

            //mailMessage.IsBodyHtml = true;
            //mailMessage.Body = "Hello <b>world!</b>";

            //mailMessage.CC.Add("me@mail-address.com");
            //mailMessage.Bcc.Add("me2@mail-address.com");

            //mailMessage.Priority = MailPriority.High;
        }
    }
}