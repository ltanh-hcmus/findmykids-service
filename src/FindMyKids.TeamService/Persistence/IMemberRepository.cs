using FindMyKids.TeamService.Models;
using System;

namespace FindMyKids.FamilyService.Persistence
{
    public interface IMemberRepository
    {
        MemberInfo Get(Guid id);
        MemberInfo Get(AuthenticateModel auth);
        Member Add(Member member);
        bool Update(MemberInfo member);
        //bool UpdateLogout(MemberInfo member);
        Member Delete(Guid id);
    }
}