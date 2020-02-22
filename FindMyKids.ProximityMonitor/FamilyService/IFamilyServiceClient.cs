using System;

namespace FindMyKids.ProximityMonitor.FamilyService
{
    public interface IFamilyServiceClient
    {
        Family GetFamily(Guid familyId);
        Member GetMember(Guid familyId, Guid memberId);
    }
}