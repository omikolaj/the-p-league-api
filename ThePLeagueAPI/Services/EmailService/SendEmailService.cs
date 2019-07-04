using System;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Services.EmailService.Templates;
using ThePLeagueDomain.ViewModels.Merchandise;
using ThePLeagueDomain.ViewModels.Team;

namespace Services.EmailService
{
  public class SendEmailService : ISendEmailService
  {
    #region Fields And Properties
    private const string Admin = "The P League Squad";
    private const string User = "User";
    private IConfigurationSection _emailAppSettings;
    private IConfiguration _configuration;
    #endregion

    #region Constructor
    public SendEmailService(IConfiguration configuration)
    {
      this._configuration = configuration;
      this._emailAppSettings = configuration.GetSection(nameof(EmailServiceOptions));
    }
    #endregion

    #region Methods
    public bool SendEmail(PreOrderViewModel email, GearItemViewModel gearItemPreOrder)
    {
      bool success = false;
      try
      {
        // Create the template class
        PreOrderTemplate preOrderTemplate = new PreOrderTemplate(this._configuration);

        // Message to User
        MimeMessage messageToUser = new MimeMessage();
        // Add From
        messageToUser.From.Add(new MailboxAddress(this._emailAppSettings[nameof(EmailServiceOptions.Admin)], this._emailAppSettings[nameof(EmailServiceOptions.AdminEmail)]));
        // Add TO
        messageToUser.To.Add(new MailboxAddress(User, email.Contact.Email));
        // Add Subject
        messageToUser.Subject = preOrderTemplate.SubjectForUser(gearItemPreOrder.Name);

        BodyBuilder bodyBuilderForUser = new BodyBuilder();

        bodyBuilderForUser.HtmlBody = preOrderTemplate.UserEmailBody(email, gearItemPreOrder);

        messageToUser.Body = bodyBuilderForUser.ToMessageBody();

        // Message To Admin
        MimeMessage messageToAdmin = new MimeMessage();
        // Add From
        messageToAdmin.From.Add(new MailboxAddress(this._emailAppSettings[nameof(EmailServiceOptions.Admin)], this._emailAppSettings[nameof(EmailServiceOptions.SystemAdminEmail)]));
        // Add TO
        messageToAdmin.To.Add(new MailboxAddress(this._emailAppSettings[nameof(EmailServiceOptions.Admin)], this._emailAppSettings[nameof(EmailServiceOptions.AdminEmail)]));
        // Add Subject
        messageToAdmin.Subject = preOrderTemplate.SubjectForAdmin(email.Id);

        BodyBuilder bodyBuilderForAdmin = new BodyBuilder();

        bodyBuilderForAdmin.HtmlBody = preOrderTemplate.AdminEmailBody(email, gearItemPreOrder);

        messageToAdmin.Body = bodyBuilderForAdmin.ToMessageBody();

        SmtpClient client = new SmtpClient();
        client.Connect(this._emailAppSettings[nameof(EmailServiceOptions.SmtpServer)], int.Parse(this._emailAppSettings[nameof(EmailServiceOptions.Port)]), true);
        client.Authenticate(this._emailAppSettings[nameof(EmailServiceOptions.SystemAdminEmail)], this._configuration["ThePLeague:SystemAdminPassword"]);

        client.Send(messageToUser);
        client.Send(messageToAdmin);

        client.Disconnect(true);
        client.Dispose();

        success = true;

      }
      catch (Exception ex)
      {
        success = false;
      }

      return success;
    }
    public bool SendEmail(TeamSignUpFormViewModel email)
    {
      bool success = false;
      try
      {
        // Create the template class
        TeamSignUpTemplate teamSignUpTemplate = new TeamSignUpTemplate(this._emailAppSettings);
        // Message to User
        MimeMessage messageToUser = new MimeMessage();
        // Add From
        messageToUser.From.Add(new MailboxAddress(this._emailAppSettings[nameof(EmailServiceOptions.Admin)], this._emailAppSettings[nameof(EmailServiceOptions.AdminEmail)]));
        // Add TO
        messageToUser.To.Add(new MailboxAddress(User, email.Contact.Email));
        // Add Subject
        messageToUser.Subject = teamSignUpTemplate.SubjectForUser(email.Name);

        BodyBuilder bodyBuilderForUser = new BodyBuilder();

        bodyBuilderForUser.HtmlBody = teamSignUpTemplate.UserEmailBody(email);

        messageToUser.Body = bodyBuilderForUser.ToMessageBody();

        // Message To Admin
        MimeMessage messageToAdmin = new MimeMessage();
        // Add From
        messageToAdmin.From.Add(new MailboxAddress(this._emailAppSettings[nameof(EmailServiceOptions.Admin)], this._emailAppSettings[nameof(EmailServiceOptions.SystemAdminEmail)]));
        // Add TO
        messageToAdmin.To.Add(new MailboxAddress(this._emailAppSettings[nameof(EmailServiceOptions.Admin)], this._emailAppSettings[nameof(EmailServiceOptions.AdminEmail)]));
        // Add Subject
        messageToAdmin.Subject = teamSignUpTemplate.SubjectForAdmin();

        BodyBuilder bodyBuilderForAdmin = new BodyBuilder();

        bodyBuilderForAdmin.HtmlBody = teamSignUpTemplate.AdminEmailBody(email);

        messageToAdmin.Body = bodyBuilderForAdmin.ToMessageBody();

        SmtpClient client = new SmtpClient();
        client.Connect(this._emailAppSettings[nameof(EmailServiceOptions.SmtpServer)], int.Parse(this._emailAppSettings[nameof(EmailServiceOptions.Port)]), true);
        client.Authenticate(this._emailAppSettings[nameof(EmailServiceOptions.SystemAdminEmail)], this._configuration["ThePLeague:SystemAdminPassword"]);

        client.Send(messageToUser);
        client.Send(messageToAdmin);

        client.Disconnect(true);
        client.Dispose();

        success = true;

      }
      catch (Exception ex)
      {
        success = false;
      }
      return success;
    }
    #endregion
  }
}