using System;

namespace FindMyKids.LocationReporter.Services
{
    public interface IFamilyServiceClient
    {
        Guid GetFamilyForMember(Guid memberId);
    }
}