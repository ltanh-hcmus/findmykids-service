using FindMyKids.TeamService.Models;
using System;

namespace FindMyKids.FamilyService.Persistence
{
    public interface IMemberRepository
    {
        Member Get(Guid id);
        Member Add(Member member);
        Member Update(Member member);
        Member Delete(Guid id);
    }
}