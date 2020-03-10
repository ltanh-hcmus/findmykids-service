using System;

namespace FindMyKids.LocationReporter.Services
{
    public interface ITeamServiceClient
    {
        Guid GetTeamForMember(Guid memberId);
    }
}