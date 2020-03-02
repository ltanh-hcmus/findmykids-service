using FindMyKids.FamilyService.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Nest;
using System;

namespace FindMyKids.FamilyService.Persistence
{
    public class ELSMemberRepository : IMemberRepository
    {
        private readonly ELSOptions eLSOptions;

        private readonly ILogger logger;

        public ELSMemberRepository(ILogger<ILogger> logger, IOptions<ELSOptions> eLSOptions)
        {
            this.logger = logger;
            this.eLSOptions = eLSOptions.Value;
        }

        public Member Add(Member member)
        {
            var settings = new ConnectionSettings(new Uri(this.eLSOptions.Uri))
                                                      .DefaultIndex(this.eLSOptions.DefaultIndex);
            var client = new ElasticClient(settings);
            member.ID = Guid.NewGuid();
            client.IndexDocument(member);
            return member;
        }

        public Member Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Member Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Member Update(Member member)
        {
            return this.Add(member);
        }
    }
}
