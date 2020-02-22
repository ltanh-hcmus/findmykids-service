using FindMyKids.LocationReporter.Events;

namespace FindMyKids.LocationReporter.Models
{
    public interface ICommandEventConverter
    {
        MemberLocationRecordedEvent CommandToEvent(LocationReport locationReport); 
    }
}