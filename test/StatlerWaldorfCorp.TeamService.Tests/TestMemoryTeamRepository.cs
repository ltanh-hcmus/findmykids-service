using System.Collections.Generic;
using FindMyKids.TeamService.Persistence;

namespace FindMyKids.TeamService.Models
{
	public class TestMemoryTeamRepository : MemoryTeamRepository {
		public TestMemoryTeamRepository() : base(CreateInitialFake()) {
			 
		}

		private static ICollection<Team> CreateInitialFake()
		{
			var teams = new List<Team>();
			teams.Add(new Team("one"));
			teams.Add(new Team("two"));

			return teams;
		}
	}	
}

