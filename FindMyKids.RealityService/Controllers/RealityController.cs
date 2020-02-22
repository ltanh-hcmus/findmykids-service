using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FindMyKids.RealityService.Location;

namespace FindMyKids.RealityService.Controllers
{
    [Route("api/reality")]
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

        [HttpGet("/families/{familyId}/members")]
        public virtual IActionResult GetFamilyMembers(Guid familyId)
        {
            return this.Ok(locationCache.GetMemberLocations(familyId));
        }

        [HttpPut("/families/{familyId}/members/{memberId}")]
        public virtual IActionResult UpdateMemberLocation(Guid familyId, Guid memberId, [FromBody]MemberLocation memberLocation)
        {
            locationCache.Put(familyId, memberLocation);
            return this.Ok(memberLocation);
        }

        [HttpGet("/families/{familyId}/members/{memberId}")]
        public virtual IActionResult GetMemberLocation(Guid familyId, Guid memberId)
        {
            return this.Ok(locationCache.Get(familyId, memberId));
        }
    }
}