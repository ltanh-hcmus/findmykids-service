namespace FindMyKids.LocationReporter.Events
{
    public interface IEventEmitter
    {
        void EmitLocationRecordedEvent(MemberLocationRecordedEvent locationRecordedEvent);
    }
}