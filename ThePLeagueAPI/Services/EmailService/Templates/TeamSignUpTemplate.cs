using Microsoft.Extensions.Configuration;
using ThePLeagueDomain.Models;
using ThePLeagueDomain.ViewModels.Team;

namespace Services.EmailService.Templates
{
  public class TeamSignUpTemplate
  {

    #region Fields and Properties
    private static IConfigurationSection _emailAppSettings;

    #endregion

    #region Constructor
    public TeamSignUpTemplate(IConfigurationSection emailAppSettings)
    {
      _emailAppSettings = emailAppSettings;
    }

    #endregion

    #region Methods
    public string SubjectForUser(string name)
    {
      return $"Welcome {name}";
    }

    public string SubjectForAdmin()
    {
      return "New Team Submission";
    }
    public string AdminEmailBody(TeamSignUpFormViewModel email)
    {
      return $@"<html lang='en'>
<head>     
  <meta charset='utf-8'>
  <meta name='viewport' content='initial-scale=1, maximum-scale=1'>
  <meta name='viewport' content='width=device-width, initial-scale=1'>  
  <link href='https://fonts.googleapis.com/css?family=Roboto:300,400,500' rel='stylesheet'>  
  <link href='https://fonts.googleapis.com/css?family=Roboto&display=swap' rel='stylesheet'>
  </style>
</head>
<body style='height: 100vh; font-family: 'Roboto', sans-serif; color:white; text-align: center'>
    <div>            
        <img src='https://res.cloudinary.com/dwsvaiiox/image/upload/v1562171421/movies-place/itmfvw7fogexa8xqyzbg.png' style='max-width:100%; max-height:185px; margin: 0 auto; display: block'>
      </div>
    <div style='text-align: center'>
        <h3>There is a new team that is interested in joining the league</h3>
    </div>
        <br>
        <div style='margin-bottom: 10%'>
          <ul>
            <li>Team Name: {email.Name}</li>
            <li>First Name: {email.Contact.FirstName}</li>
            <li>Last Name: {email.Contact.LastName}</li>
            <li>Phone Number: {email.Contact.PhoneNumber}</li>
            <li>E-mail: {email.Contact.Email}</li>
            <li>Preferred Form of Contact: {(PreferredContact)email.Contact.PreferredContact}</li>
          </ul>
        </div>
        <div style='text-align: center; margin-bottom:10%'>
          
            <h4 style='font-weight: 900'>
                {_emailAppSettings[nameof(EmailServiceOptions.AdminEmail)]}
            </h4>
          <h4 style='font-weight: 900;'>
            {_emailAppSettings[nameof(EmailServiceOptions.AdminPhoneNumber)]}
          </h4>
        </div>
</body>
</html>";
    }

    public string UserEmailBody(TeamSignUpFormViewModel email)
    {
      return $@"<html lang='en'>
                              <head>     
  <meta charset='utf-8'>
  <meta name='viewport' content='initial-scale=1, maximum-scale=1'>
  <meta name='viewport' content='width=device-width, initial-scale=1'>  
  <link href='https://fonts.googleapis.com/css?family=Roboto:300,400,500' rel='stylesheet'>  
  <link href='https://fonts.googleapis.com/css?family=Roboto&display=swap' rel='stylesheet'>
  </style>
</head>
<body style='height: 100vh; font-family: 'Roboto', sans-serif; color:white; text-align: center'>
    <div style='margin-bottom: 35px'>            
        <img src='https://res.cloudinary.com/dwsvaiiox/image/upload/v1562171421/movies-place/itmfvw7fogexa8xqyzbg.png' style='max-width:100%; max-height:185px; margin: 0 auto; display: block'>
      </div>
    <div>
        <h3>Hey {email.Contact.FirstName}, this is Nik from The P League, I am the owner and organizer. Just wanted to say thanks for your interested in this league I am always excited to have new teams come on board. I will get back to you very soon.This email confirms I have received your inquiry and I will be contacting you shortly.</h3>
    </div>
        <br>
        <div style='text-align: center; margin-bottom:10%'>
          
            <h4 style='font-weight: 900'>
                {_emailAppSettings[nameof(EmailServiceOptions.AdminEmail)]}
            </h4>
          <h4 style='font-weight: 900;'>
            {_emailAppSettings[nameof(EmailServiceOptions.AdminPhoneNumber)]}
          </h4>
        </div>
</body>
</html>";
    }

    #endregion
  }
}