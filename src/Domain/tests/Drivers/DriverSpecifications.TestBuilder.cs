using BuildingLink.DriverManagement.Domain.Drivers;
using BuildingLink.DriverManagement.Domain.Types;

namespace BuildingLink.DriverManagement.Domain.Tests.Drivers;

public partial class DriverSpecifications
{
    private static class TestBuilder
    {
        private const int TestId = 1;
        private const string TestName = "test";
        private const string TestEmail = "test@domain.com";
        private const string TestPhoneNumber = "+1-404-724-1937";

        public static Driver CreateDriver()
        {
            return new Driver
            {
                Id = new Id(TestId),
                Email = new Email(TestEmail),
                FirstName = new Name(TestName),
                LastName = new Name(TestName),
                PhoneNumber = new PhoneNumber(TestPhoneNumber)
            };
        }
    }
}