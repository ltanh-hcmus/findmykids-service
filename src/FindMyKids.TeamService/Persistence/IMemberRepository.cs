using FindMyKids.TeamService.Models;
using System;

namespace FindMyKids.FamilyService.Persistence
{
    public interface IMemberRepository
    {
        Member Get(Guid id);
        MemberInfo Get(AuthenticateModel auth);
        Member Add(Member member);
        bool Update(MemberInfo member);
        Member Delete(Guid id);
    }
}