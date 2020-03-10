using FindMyKids.ProximityMonitor.Events;

namespace FindMyKids.ProximityMonitor.Realtime
{
    public class ProximityDetectedRealtimeEvent : ProximityDetectedEvent
    {
       public string TeamName { get; set; }
       public string SourceMemberName { get; set; }
       public string TargetMemberName { get; set; } 
    }
}