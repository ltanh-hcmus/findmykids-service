namespace FindMyKids.EventProcessor.Queues
{
    public class QueueOptions
    {
        public string ProximityDetectedEventQueueName { get; set; }

        public string MemberLocationRecordedEventQueueName { get; set; }
    }
}