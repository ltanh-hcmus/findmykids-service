namespace FindMyKids.ProximityMonitor.Realtime
{
    public interface IRealtimePublisher
    {
        void Publish(string channelName, string message);
        void Validate();
    }
}