using PhoneNumbers;

namespace BuildingLink.DriverManagement.Domain.Types.Validators;

public static class PhoneNumberValidator
{
    public static bool IsValidPhoneNumber(this string phoneNumber, string? region = null)
    {
        try
        {
            var phoneUtil = PhoneNumberUtil.GetInstance();
            var number = phoneUtil.Parse(phoneNumber, region);
            return phoneUtil.IsValidNumber(number);
        }
        catch
        {
            return false;
        }
    }
}