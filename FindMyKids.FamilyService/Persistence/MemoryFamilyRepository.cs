using FindMyKids.FamilyService.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FindMyKids.FamilyService.Persistence
{
    class MemoryFamilyRepository : IFamilyRepository
	{
		protected static ICollection<Family> families;

		public MemoryFamilyRepository()
		{
			//if (families == null)
			//{
			//	families = new List<Family>();
			//}

			//families = new List<Family>();

			//ICollection<Member> members = new List<Member>();

			//members.Add(new Member { ID = Guid.Parse("875ea426-6317-4945-8dbf-b775fb3e9287"), FirstName = "Lê Tuấn", LastName = "Anh" });
			//members.Add(new Member { ID = Guid.Parse("2d9a6966-17f5-4339-9f14-0acfd05005fd"), FirstName = "Huỳnh Văn", LastName = "Hậu" });

			//Family family = new Family { ID = Guid.Parse("681c9af4-7780-4647-b180-ab7e01cb8617"), Name = "Gia Đình 1", Members = members };

			//families.Add(family);
		}

		public MemoryFamilyRepository(ICollection<Family> families)
		{
			MemoryFamilyRepository.families = families;
		}

		public IEnumerable<Family> List()
		{
			return families;
		}

		public Family Get(Guid id)
		{
			return families.FirstOrDefault(t => t.ID == id);
		}

		public Family Update(Family t)
		{
			Family family = this.Delete(t.ID);

			if (family != null)
			{
				family = this.Add(t);
			}

			return family;
		}

		public Family Add(Family family)
		{
			families.Add(family);
			return family;
		}

		public Family Delete(Guid id)
		{
			var q = families.Where(t => t.ID == id);
			Family family = null;

			if (q.Count() > 0)
			{
				family = q.First();
				families.Remove(family);
			}

			return family;
		}
	}
}