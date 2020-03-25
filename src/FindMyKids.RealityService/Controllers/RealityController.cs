using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FindMyKids.RealityService.Location;
using Microsoft.AspNetCore.Cors;

namespace FindMyKids.RealityService.Controllers
{
    [Route("api/reality")]
    [EnableCors]
    public class RealityController : Controller
    {
        private ILocationCache locationCache;
        private ILogger logger;

        public RealityController(ILocationCache locationCache,
            ILogger<RealityController> logger) 
        {
            this.locationCache = locationCache;
            this.logger = logger;
        }

        [EnableCors]
        [HttpGet("/teams/{teamId}/members")]
        public virtual IActionResult GetTeamMembers(Guid teamId)
        {
            return this.Ok(locationCache.GetMemberLocations(teamId));
        }

        [EnableCors]
        [HttpPut("/teams/{teamId}/members/{memberId}")]
        public virtual IActionResult UpdateMemberLocation(Guid teamId, Guid memberId, [FromBody]MemberLocation memberLocation)
        {
            locationCache.Put(teamId, memberLocation);
            return this.Ok(memberLocation);
        }

        [EnableCors]
        [HttpGet("/members/{memberId}")]
        public virtual IActionResult GetMemberLocation(Guid memberId)
        {
            return this.Ok(locationCache.Get(memberId));
        }
    }
}