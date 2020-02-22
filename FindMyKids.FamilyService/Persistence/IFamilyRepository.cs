using FindMyKids.FamilyService.Models;
using System;
using System.Collections.Generic;

namespace FindMyKids.FamilyService.Persistence
{
	public interface IFamilyRepository
	{
		IEnumerable<Family> List();
		Family Get(Guid id);
		Family Add(Family family);
		Family Update(Family family);
		Family Delete(Guid id);
	}
}
