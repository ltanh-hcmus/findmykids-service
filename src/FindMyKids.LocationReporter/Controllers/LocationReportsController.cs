using System;
using Microsoft.AspNetCore.Mvc;
using FindMyKids.LocationReporter.Events;
using FindMyKids.LocationReporter.Models;
using FindMyKids.LocationReporter.Services;

namespace FindMyKids.LocationReporter.Controllers
{
    [Route("/api/members/{memberId}/locationreports")]
    public class LocationReportsController : Controller
    {
        private ICommandEventConverter converter;
        private IEventEmitter eventEmitter;
        private ITeamServiceClient teamServiceClient;
        

        public LocationReportsController(ICommandEventConverter converter, 
            IEventEmitter eventEmitter, 
            ITeamServiceClient teamServiceClient) {
            this.converter = converter;
            this.eventEmitter = eventEmitter;
            this.teamServiceClient = teamServiceClient;
        }

        [HttpPost]
        public ActionResult PostLocationReport(Guid memberId, [FromBody]LocationReport locationReport)
        {
            MemberLocationRecordedEvent locationRecordedEvent = converter.CommandToEvent(locationReport);
            //locationRecordedEvent.TeamID = teamServiceClient.GetTeamForMember(locationReport.MemberID);
            locationRecordedEvent.TeamID = Guid.Parse("681c9af4-7780-4647-b180-ab7e01cb8617");
            eventEmitter.EmitLocationRecordedEvent(locationRecordedEvent);

            return this.Created($"/api/members/{memberId}/locationreports/{locationReport.ReportID}", locationReport);
        }
    }
}