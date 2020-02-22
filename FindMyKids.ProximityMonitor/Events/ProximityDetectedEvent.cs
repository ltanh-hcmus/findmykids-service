using System;
using Newtonsoft.Json;
using FindMyKids.ProximityMonitor.Location;

namespace FindMyKids.ProximityMonitor.Events
{
    public class ProximityDetectedEvent
    {
        public Guid SourceMemberID { get; set; }
        public Guid TargetMemberID { get; set; }
        public long DetectionTime { get; set; }
        public GpsCoordinate SourceMemberLocation { get; set; }
        public GpsCoordinate TargetMemberLocation { get; set; }
        public double MemberDistance { get; set; }
        public Guid FamilyID { get; set; }

         public string toJson() {
            return JsonConvert.SerializeObject(this);
        }
    }
}