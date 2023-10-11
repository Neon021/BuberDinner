using BuberDinner.Domain.Hosts.ValueObjects;

namespace BuberDinner.Application.UnitTests.TestUtils.Constants;

public static partial class Constants
{
    public static class Host
    {
        public static readonly HostId hostId = HostId.Create(Guid.NewGuid());
    }
}
