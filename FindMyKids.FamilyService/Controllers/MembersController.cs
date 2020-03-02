using FindMyKids.FamilyService.Models;
using FindMyKids.FamilyService.Persistence;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FindMyKids.FamilyService.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class MembersController : Controller
	{
		private readonly IMemberRepository repository;

		public MembersController(IMemberRepository repo)
		{
			repository = repo;
		}

		[HttpGet]
		public virtual IActionResult GetMemberxxx(Guid memberID)
		{
			return this.Ok();
		}

		[HttpGet]
		[Route("[controller]/{memberID}")]
		public virtual IActionResult GetMember(Guid memberID)
		{
			Member member = repository.Get(memberID);

			if (member == null)
			{
				return this.NotFound();
			}
			else
			{
				return this.Ok(member);
			}
		}

		[HttpPut]
		public virtual IActionResult UpdateMember([FromBody]Member updatedMember)
		{
			//Family family = repository.Get(familyID);

			//if (family == null)
			//{
			//	return this.NotFound();
			//}
			//else
			//{
			//	var q = family.Members.Where(m => m.ID == memberId);

			//	if (q.Count() < 1)
			//	{
			//		return this.NotFound();
			//	}
			//	else
			//	{
			//		family.Members.Remove(q.First());
			//		family.Members.Add(updatedMember);
			//		return this.Ok();
			//	}
			//}

			return this.Ok();
		}

		[HttpPost]
		public virtual IActionResult CreateMember([FromBody]Member newMember)
		{
			repository.Add(newMember);
			return this.Ok(newMember);
		}

		//[HttpGet]
		//[Route("/members/{memberId}/family")]
		//public IActionResult GetfamilyForMember(Guid memberId)
		//{
		//	var familyId = GetfamilyIdForMember(memberId);
		//	if (familyId != Guid.Empty)
		//	{
		//		return this.Ok(new
		//		{
		//			familyID = familyId
		//		});
		//	}
		//	else
		//	{
		//		return this.NotFound();
		//	}
		//}

		//private Guid GetfamilyIdForMember(Guid memberId)
		//{
		//	foreach (var family in repository.List())
		//	{
		//		var member = family.Members.FirstOrDefault(m => m.ID == memberId);
		//		if (member != null)
		//		{
		//			return family.ID;
		//		}
		//	}
		//	return Guid.Empty;
		//}
	}
}
