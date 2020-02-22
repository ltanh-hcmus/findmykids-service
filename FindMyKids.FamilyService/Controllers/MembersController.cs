using FindMyKids.FamilyService.Models;
using FindMyKids.FamilyService.Persistence;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace FindMyKids.FamilyService.Controllers
{
	[Route("/families/{familyId}/[controller]")]
	public class MembersController : Controller
	{
		IFamilyRepository repository;

		public MembersController(IFamilyRepository repo)
		{
			repository = repo;
		}

		[HttpGet]
		public virtual IActionResult GetMembers(Guid familyID)
		{
			Family family = repository.Get(familyID);

			if (family == null)
			{
				return this.NotFound();
			}
			else
			{
				return this.Ok(family.Members);
			}
		}


		[HttpGet]
		[Route("/families/{familyId}/[controller]/{memberId}")]
		public virtual IActionResult GetMember(Guid familyID, Guid memberId)
		{
			Family family = repository.Get(familyID);

			if (family == null)
			{
				return this.NotFound();
			}
			else
			{
				var q = family.Members.Where(m => m.ID == memberId);

				if (q.Count() < 1)
				{
					return this.NotFound();
				}
				else
				{
					return this.Ok(q.First());
				}
			}
		}

		[HttpPut]
		[Route("/families/{familyId}/[controller]/{memberId}")]
		public virtual IActionResult UpdateMember([FromBody]Member updatedMember, Guid familyID, Guid memberId)
		{
			Family family = repository.Get(familyID);

			if (family == null)
			{
				return this.NotFound();
			}
			else
			{
				var q = family.Members.Where(m => m.ID == memberId);

				if (q.Count() < 1)
				{
					return this.NotFound();
				}
				else
				{
					family.Members.Remove(q.First());
					family.Members.Add(updatedMember);
					return this.Ok();
				}
			}
		}

		[HttpPost]
		public virtual IActionResult CreateMember([FromBody]Member newMember, Guid familyID)
		{
			Family family = repository.Get(familyID);

			if (family == null)
			{
				return this.NotFound();
			}
			else
			{
				family.Members.Add(newMember);
				var familyMember = new { familyID = family.ID, MemberID = newMember.ID };
				return this.Created($"/families/{familyMember.familyID}/[controller]/{familyMember.MemberID}", familyMember);
			}
		}

		[HttpGet]
		[Route("/members/{memberId}/family")]
		public IActionResult GetfamilyForMember(Guid memberId)
		{
			var familyId = GetfamilyIdForMember(memberId);
			if (familyId != Guid.Empty)
			{
				return this.Ok(new
				{
					familyID = familyId
				});
			}
			else
			{
				return this.NotFound();
			}
		}

		private Guid GetfamilyIdForMember(Guid memberId)
		{
			foreach (var family in repository.List())
			{
				var member = family.Members.FirstOrDefault(m => m.ID == memberId);
				if (member != null)
				{
					return family.ID;
				}
			}
			return Guid.Empty;
		}
	}
}
