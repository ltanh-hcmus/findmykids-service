using FindMyKids.FamilyService.Models;
using System;

namespace FindMyKids.FamilyService.Persistence
{
    public interface IMemberRepository
    {
        Member Get(Guid id);
        Member Add(Member family);
        Member Update(Member family);
        Member Delete(Guid id);
    }
}
