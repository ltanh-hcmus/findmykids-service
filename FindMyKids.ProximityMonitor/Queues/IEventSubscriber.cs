using FindMyKids.ProximityMonitor.Events;

namespace FindMyKids.ProximityMonitor.Queues
{
    public delegate void ProximityDetectedEventReceivedDelegate(ProximityDetectedEvent evt);

    public interface IEventSubscriber
    {
        void Subscribe();
        void Unsubscribe();
        
        event ProximityDetectedEventReceivedDelegate ProximityDetectedEventReceived;
    }
}