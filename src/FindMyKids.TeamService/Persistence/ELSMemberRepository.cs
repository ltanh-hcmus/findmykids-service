using FindMyKids.FamilyService.Models;
using FindMyKids.TeamService.Models;
using FindMyKids.TeamService.Persistence;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Nest;
using System;
using System.Collections.Generic;

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

            var searchResponse = client.Search<Member>(s => s
                .From(0)
                .Size(1)
                .Query(q => q
                .Match(m => m
                    .Field(f => f.UserName)
                    .Query(member.UserName)
                    )
                )
            );

            if (searchResponse.Documents.Count == 0)
            {
                member.ID = Guid.NewGuid();
                client.IndexDocument(member);
                return member;
            }

            return null;
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
            throw new NotImplementedException();
        }
    }
}