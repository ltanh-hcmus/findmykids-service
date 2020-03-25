using FindMyKids.FamilyService.Models;
using FindMyKids.TeamService.Models;
using Microsoft.Extensions.Logging;
using Nest;
using System;
using System.Collections.Generic;
using Xunit;

namespace FindMyKids.FamilyService.Persistence
{
    public class TestELSMemberRepository : IMemberRepository
    {
        private readonly ELSOptions eLSOptions;
        //private readonly ILogger logger;

        public TestELSMemberRepository(ELSOptions eLSOptions)
        {
            this.eLSOptions = eLSOptions;
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

        public Member Get(Member member)
        {
            var settings = new ConnectionSettings(new Uri(this.eLSOptions.Uri))
                                          .DefaultIndex(this.eLSOptions.DefaultIndex);
            var client = new ElasticClient(settings);
            
            var searchResponse = client.Search<Member>(s => s
                .From(0)
                .Size(1)
                .Query(q => q
                .Match(m => m
                    .Field(f => f.ID)
                    .Query(member.ID.ToString())
                    )
                )
            );

            if (searchResponse.Documents.Count != 0)
            {
                IEnumerator<Member> enumerator = searchResponse.Documents.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    Member memberCheck = enumerator.Current;
                    Assert.NotNull(memberCheck);

                    Assert.Equal(member.UserName, memberCheck.UserName);
                    Assert.Equal(member.AccessToken, memberCheck.AccessToken);
                    Assert.Equal(member.Email, memberCheck.Email);
                    Assert.Equal(member.Name, memberCheck.Name);
                    Assert.Equal(member.PassWord, memberCheck.PassWord);
                    Assert.Equal(member.RefreshToken, memberCheck.RefreshToken);
                    Assert.Equal(member.UserName, memberCheck.UserName);

                    return enumerator.Current;
                }
            }

            return null;
        }

        public Member Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public MemberInfo Get(AuthenticateModel auth)
        {
            throw new NotImplementedException();
        }

        public Member Update(Member member)
        {
            throw new NotImplementedException();
        }

        public bool Update(MemberInfo member)
        {
            throw new NotImplementedException();
        }

        MemberInfo IMemberRepository.Get(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}