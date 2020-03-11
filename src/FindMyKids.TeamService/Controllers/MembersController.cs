using FindMyKids.FamilyService.Persistence;
using FindMyKids.TeamService.Models;
using FindMyKids.TeamService.Persistence;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace FindMyKids.TeamService
{
	[Route("[controller]")]
	public class MembersController : Controller
	{
		IMemberRepository repository;

		public MembersController(IMemberRepository repo) 
		{
			repository = repo;
		}

		[HttpGet]
		public virtual IActionResult GetMembers(Guid teamID) 
		{
			//Team team = repository.Get(teamID);
			
			//if(team == null) {
			//	return this.NotFound();
			//} else {
			//	return this.Ok(team.Members);
			//}
			return this.NotFound();
		}
		

		[HttpGet]
		[Route("/teams/{teamId}/[controller]/{memberId}")]		
		public virtual IActionResult GetMember(Guid teamID, Guid memberId) 
		{
			//Team team = repository.Get(teamID);

			//if(team == null) {
			//	return this.NotFound();
			//} else {
			//	var q = team.Members.Where(m => m.ID == memberId);

			//	if(q.Count() < 1) {
			//		return this.NotFound();
			//	} else {
			//		return this.Ok(q.First());
			//	}				
			//}			

			return this.NotFound();
		}

		[HttpPut]
		[Route("/teams/{teamId}/[controller]/{memberId}")]		
		public virtual IActionResult UpdateMember([FromBody]Member updatedMember, Guid teamID, Guid memberId) 
		{
			//Team team = repository.Get(teamID);
			
			//if(team == null) {
			//	return this.NotFound();
			//} else {
			//	var q = team.Members.Where(m => m.ID == memberId);

			//	if(q.Count() < 1) {
			//		return this.NotFound();
			//	} else {
			//		team.Members.Remove(q.First());
			//		team.Members.Add(updatedMember);
			//		return this.Ok();
			//	}
			//}
			return this.Ok();
		}

		[EnableCors("_myAllowSpecificOrigins")]
		[HttpPost]


		public virtual IActionResult CreateMember([FromBody]Member newMember) 
		{
			// vao day nhu ben nodejs vay
			//string EncodeResponse = Request.Form["g-Recaptcha-Response"];
			//bool IsCaptchaValid = (Recaptcha.Validate(EncodeResponse) == "True" ? true : false);
			int countMember= repository.FindUserName(newMember.UserName);

			if (countMember == 0)
			{
				newMember.PassWord = BCrypt.Net.BCrypt.HashPassword(newMember.PassWord);
				repository.Add(newMember);
				return this.Created($"[controller]", newMember);
			}
			return null;
		}

		[HttpGet]
		[Route("/members/{memberId}/team")]
		public IActionResult GetTeamForMember(Guid memberId)
		{
			//var teamId = GetTeamIdForMember(memberId);
			//if (teamId != Guid.Empty) {
			//	return this.Ok(new {
			//		TeamID = teamId
			//	});
			//} else {
			//	return this.NotFound();
			//}

			return this.NotFound();
		}

		private Guid GetTeamIdForMember(Guid memberId)
		{
			//foreach (var team in repository.List()) {
			//	var member = team.Members.FirstOrDefault( m => m.ID == memberId);
			//	if (member != null) {
			//		return team.ID;
			//	}
			//}
			return Guid.Empty;
		}    
    }
}