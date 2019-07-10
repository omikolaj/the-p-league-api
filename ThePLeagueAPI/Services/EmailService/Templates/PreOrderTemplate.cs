using System;
using Microsoft.Extensions.Configuration;
using ThePLeagueDomain.Models;
using ThePLeagueDomain.Models.Merchandise;
using ThePLeagueDomain.ViewModels.Merchandise;

namespace Services.EmailService.Templates
{
  public class PreOrderTemplate
  {
    #region Constructor
    public PreOrderTemplate(IConfiguration configuration)
    {
      _emailAppSettings = configuration.GetSection(nameof(EmailServiceOptions));
    }
    #endregion

    #region Fields and Properties
    private IConfigurationSection _emailAppSettings;

    #endregion

    #region Methods

    public string SubjectForUser(string gearItemName)
    {
      return $"Pre-Order Confirmation for {gearItemName}";
    }

    public string SubjectForAdmin(long? orderId)
    {
      return $"New Pre-Order ID#{orderId}";
    }
    public string AdminEmailBody(PreOrderViewModel email, GearItemViewModel gearItem)
    {
      decimal total = Math.Round(email.Quantity.GetValueOrDefault() * gearItem.Price, 2);
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
        <h3>Pre-order has been placed for {email.Quantity} {gearItem.Name}. The total is ${total}. Order Details:</h3>
    </div>
        <div style='margin-bottom: 10%'>
          <ul>
            <li>Item ID: {gearItem.Id}</li>
            <li>Size: {(Size)email.Size}</li>
            <li>Quantity: {email.Quantity}</li>
            <li>Price: ${Math.Round(gearItem.Price, 2)}</li>            
            <li>Name: {email.Contact.FirstName} {email.Contact.LastName}</li>
            <li>Phone Number: {email.Contact.PhoneNumber}</li>
            <li>E-Mail: {email.Contact.Email}</li>
            <li>Preferred Form of Contact: {(PreferredContact)email.Contact.PreferredContact}</li>            
          </ul>
        </div>
        <div>            
            <img src='{gearItem.Images[0].Url}' style='max-width:100%; max-height:250px; margin: 0 auto; display: block'>
          </div>
        <br>
        <div style='text-align: center; margin-bottom:15%'>
          
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
    public string UserEmailBody(PreOrderViewModel email, GearItemViewModel gearItem)
    {
      decimal total = Math.Round(email.Quantity.GetValueOrDefault() * gearItem.Price, 2);
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
        <h3>Your pre-ordered has been successfully processed. You pre-ordered {gearItem.Name}. Total is ${total}. Order Details: </h3>
    </div>
        <div style='margin-bottom: 10%'>
          <ul>
            <li>Size: {(Size)email.Size}</li>
            <li>Quantity: {email.Quantity}</li>
            <li>Price: ${Math.Round(gearItem.Price, 2)}</li>
          </ul>
        </div>
        <div>            
            <img src='{gearItem.Images[0].Url}' style='max-width:100%; max-height:250px; margin: 0 auto; display: block'>
          </div>
        <br>
        <div style='text-align: center'>
          <h3>If something is incorrect about the order please e-mail, call or text us asap. Your pre-order ID is {email.Id}</h3>
        </div>
        <div style='text-align: center; margin-bottom:15%'>
          
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