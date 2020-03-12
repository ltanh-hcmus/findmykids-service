using FindMyKids.FamilyService.Persistence;
using FindMyKids.TeamService.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

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

		[HttpPost]
		[EnableCors("_myAllowSpecificOrigins")]

		public virtual IActionResult CreateMember([FromBody]Member newMember)
		{
			//if (Request.Headers.ContainsKey("recaptchaToken"))
			//{
			//	string EncodeResponse = Request.Headers["recaptchaToken"];
			//	if (EncodeResponse == null)
			//	{
			//		return this.NotFound();
			//	}
			//	bool IsCaptchaValid = (Recaptcha.Validate(EncodeResponse) == "True" ? true : false);

			//	if (!IsCaptchaValid)
			//	{
			//		return this.NotFound();
			//	}
			//}
			//else
			//{
			//	return this.NotFound();
			//}

			newMember.PassWord = BCrypt.Net.BCrypt.HashPassword(newMember.PassWord);
			if (repository.Add(newMember) != null)
			{
				return this.Created($"[controller]", newMember);
			}

			return this.NotFound();
		}

		//[HttpGet]
		//public virtual IActionResult GetMembers(Guid teamID) 
		//{
		//	//Team team = repository.Get(teamID);
			
		//	//if(team == null) {
		//	//	return this.NotFound();
		//	//} else {
		//	//	return this.Ok(team.Members);
		//	//}
		//	return this.NotFound();
		//}
		

		//[HttpGet]
		//[Route("/teams/{teamId}/[controller]/{memberId}")]		
		//public virtual IActionResult GetMember(Guid teamID, Guid memberId) 
		//{
		//	//Team team = repository.Get(teamID);

		//	//if(team == null) {
		//	//	return this.NotFound();
		//	//} else {
		//	//	var q = team.Members.Where(m => m.ID == memberId);

		//	//	if(q.Count() < 1) {
		//	//		return this.NotFound();
		//	//	} else {
		//	//		return this.Ok(q.First());
		//	//	}				
		//	//}			

		//	return this.NotFound();
		//}

		//[HttpPut]
		//[Route("/teams/{teamId}/[controller]/{memberId}")]		
		//public virtual IActionResult UpdateMember([FromBody]Member updatedMember, Guid teamID, Guid memberId) 
		//{
		//	//Team team = repository.Get(teamID);
			
		//	//if(team == null) {
		//	//	return this.NotFound();
		//	//} else {
		//	//	var q = team.Members.Where(m => m.ID == memberId);

		//	//	if(q.Count() < 1) {
		//	//		return this.NotFound();
		//	//	} else {
		//	//		team.Members.Remove(q.First());
		//	//		team.Members.Add(updatedMember);
		//	//		return this.Ok();
		//	//	}
		//	//}
		//	return this.Ok();
		//}

		//[HttpGet]
		//[Route("/members/{memberId}/team")]
		//public IActionResult GetTeamForMember(Guid memberId)
		//{
		//	//var teamId = GetTeamIdForMember(memberId);
		//	//if (teamId != Guid.Empty) {
		//	//	return this.Ok(new {
		//	//		TeamID = teamId
		//	//	});
		//	//} else {
		//	//	return this.NotFound();
		//	//}

		//	return this.NotFound();
		//}

		//private Guid GetTeamIdForMember(Guid memberId)
		//{
		//	//foreach (var team in repository.List()) {
		//	//	var member = team.Members.FirstOrDefault( m => m.ID == memberId);
		//	//	if (member != null) {
		//	//		return team.ID;
		//	//	}
		//	//}
		//	return Guid.Empty;
		//}    
    }
}