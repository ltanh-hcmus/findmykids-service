using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using FindMyKids.ProximityMonitor.Queues;
using FindMyKids.ProximityMonitor.Realtime;
using FindMyKids.ProximityMonitor.FamilyService;

namespace FindMyKids.ProximityMonitor.Events
{
    public class ProximityDetectedEventProcessor : IEventProcessor
    {
        private ILogger logger;
        private IRealtimePublisher publisher;
        private IEventSubscriber subscriber;

        private PubnubOptions pubnubOptions;

        public ProximityDetectedEventProcessor(
            ILogger<ProximityDetectedEventProcessor> logger,
            IRealtimePublisher publisher,
            IEventSubscriber subscriber,
            IFamilyServiceClient familyClient,
            IOptions<PubnubOptions> pubnubOptions)
        {
            this.logger = logger;
            this.pubnubOptions = pubnubOptions.Value;
            this.publisher = publisher;
            this.subscriber = subscriber;            

            logger.LogInformation("Created Proximity Event Processor.");        

            subscriber.ProximityDetectedEventReceived += (pde) => {
                Family t = familyClient.GetFamily(pde.FamilyID);
                Member sourceMember = familyClient.GetMember(pde.FamilyID, pde.SourceMemberID);
                Member targetMember = familyClient.GetMember(pde.FamilyID, pde.TargetMemberID);

                ProximityDetectedRealtimeEvent outEvent = new ProximityDetectedRealtimeEvent 
                {
                    TargetMemberID = pde.TargetMemberID,
                    SourceMemberID = pde.SourceMemberID,
                    DetectionTime = pde.DetectionTime,                    
                    SourceMemberLocation = pde.SourceMemberLocation,
                    TargetMemberLocation = pde.TargetMemberLocation,
                    MemberDistance = pde.MemberDistance,
                    FamilyID = pde.FamilyID,
                    FamilyName = t.Name,
                    SourceMemberName = $"{sourceMember.FirstName} {sourceMember.LastName}",
                    TargetMemberName = $"{targetMember.FirstName} {targetMember.LastName}"
                };
                publisher.Publish(this.pubnubOptions.ProximityEventChannel, outEvent.toJson());
            };            
        }    
        
        public void Start()
        {
            subscriber.Subscribe();
        }

        public void Stop()
        {
            subscriber.Unsubscribe();
        }
    }
}