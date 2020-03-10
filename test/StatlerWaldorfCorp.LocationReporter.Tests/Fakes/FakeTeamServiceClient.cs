using System;
using FindMyKids.LocationReporter.Services;

namespace FindMyKids.LocationReporter.Tests.Fakes
{
    public class FakeTeamServiceClient : ITeamServiceClient
    {
        private Guid teamGuid;

        public FakeTeamServiceClient() {
            teamGuid = Guid.NewGuid();
        }
    
        public Guid GetTeamForMember(Guid memberId)
        {
            return teamGuid;
        }

        public Guid FixedID 
        {
            get {
                return teamGuid;
            }
        }
    }
}