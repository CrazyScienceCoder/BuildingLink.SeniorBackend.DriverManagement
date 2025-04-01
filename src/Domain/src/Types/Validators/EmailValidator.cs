using System.Net;
using System.Net.Mail;

namespace BuildingLink.DriverManagement.Domain.Types.Validators;

public static class EmailValidator
{
    public static bool IsValidEmail(this string email)
    {
        try
        {
            var emailAddress = new MailAddress(email);
            var entry = Dns.GetHostEntry(emailAddress.Host);
            return entry.AddressList.Length > 0;
        }
        catch
        {
            return false;
        }
    }
}