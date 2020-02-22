using System;
using System.Collections.Generic;

namespace FindMyKids.EventProcessor.Location
{
    public interface ILocationCache
    {
        IList<MemberLocation> GetMemberLocations(Guid familyId);

        void Put(Guid familyId, MemberLocation memberLocation);
    }
}