using System;
using System.Collections.Generic;

namespace FindMyKids.RealityService.Location
{
    public interface ILocationCache
    {
        IList<MemberLocation> GetMemberLocations(Guid familyId);
        void Put(Guid familyId, MemberLocation memberLocation);
        MemberLocation Get(Guid familyId, Guid memberId);
    }
}