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
        private IFamilyServiceClient familyServiceClient;
        

        public LocationReportsController(ICommandEventConverter converter, 
            IEventEmitter eventEmitter, 
            IFamilyServiceClient familyServiceClient) {
            this.converter = converter;
            this.eventEmitter = eventEmitter;
            this.familyServiceClient = familyServiceClient;
        }

        [HttpPost]
        public ActionResult PostLocationReport(Guid memberId, [FromBody]LocationReport locationReport)
        {
            MemberLocationRecordedEvent locationRecordedEvent = converter.CommandToEvent(locationReport);
            locationRecordedEvent.FamilyID = familyServiceClient.GetFamilyForMember(locationReport.MemberID);
            eventEmitter.EmitLocationRecordedEvent(locationRecordedEvent);

            return this.Created($"/api/members/{memberId}/locationreports/{locationReport.ReportID}", locationReport);
        }
    }
}