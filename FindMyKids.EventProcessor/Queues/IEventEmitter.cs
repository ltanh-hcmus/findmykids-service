using FindMyKids.EventProcessor.Events;

namespace FindMyKids.EventProcessor.Queues
{
    public interface IEventEmitter
    {
        void EmitProximityDetectedEvent(ProximityDetectedEvent proximityDetectedEvent);
    }
}