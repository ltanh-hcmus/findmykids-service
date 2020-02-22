namespace FindMyKids.EventProcessor.Events
{
    public interface IEventProcessor
    {
        void Start();
        void Stop();   
    }
}