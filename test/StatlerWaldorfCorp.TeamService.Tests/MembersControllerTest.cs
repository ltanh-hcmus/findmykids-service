using FindMyKids.FamilyService.Models;
using FindMyKids.FamilyService.Persistence;
using FindMyKids.TeamService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using Xunit;

[assembly: CollectionBehavior(MaxParallelThreads = 1)]

namespace FindMyKids.TeamService
{
    public class MembersControllerTest
    {
        [Fact]
        public void Register()
        {
            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.SetBasePath(System.IO.Directory.GetCurrentDirectory());
            configurationBuilder.AddJsonFile("appsettings.json");
            IConfigurationRoot configurationRoot = configurationBuilder.Build();

            ELSOptions eLSOptions = configurationRoot.GetSection("els").Get<ELSOptions>();
            CaptchaToken captchaToken = configurationRoot.GetSection("reCaptchaToken").Get<CaptchaToken>();

            TestELSMemberRepository testELSMemberRepository = new TestELSMemberRepository(eLSOptions);

            var appSettingsSection = configurationRoot.GetSection("AppSettings");
            var appSettings = appSettingsSection.Get<AppSettings>();

            MembersController controller = new MembersController(testELSMemberRepository, Options.Create<AppSettings>(appSettings));
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.Request.Headers["recaptchaToken"] = captchaToken.value;

            Member member = new Member
            {
                AccessToken = Guid.NewGuid().ToString(),
                Email = "test@gmail.com",
                ID = Guid.NewGuid(),
                Name = Guid.NewGuid().ToString(),
                PassWord = Guid.NewGuid().ToString(),
                RefreshToken = Guid.NewGuid().ToString(),
                State = StateMember.Allow,
                UserName = Guid.NewGuid().ToString()
            };

            controller.CreateMember(member);

            testELSMemberRepository.Get(member);
        }

        //[Fact]
        //public void CreateMembertoNonexistantTeamReturnsNotFound() 
        //{
        //    ITeamRepository repository = new TestMemoryTeamRepository();
        //    MembersController controller = new MembersController(repository);

        //    Guid teamId = Guid.NewGuid();

        //    Guid newMemberId = Guid.NewGuid();
        //    Member newMember = new Member(newMemberId);
        //    var result = controller.CreateMember(newMember, teamId);

        //    Assert.True(result is NotFoundResult);
        //}

        //[Fact]
        //public void GetExistingMemberReturnsMember() 
        //{
        //    ITeamRepository repository = new TestMemoryTeamRepository();
        //    MembersController controller = new MembersController(repository);

        //    Guid teamId = Guid.NewGuid();
        //    Team team = new Team("TestTeam", teamId);
        //    var debugTeam = repository.Add(team);        

        //    Guid memberId = Guid.NewGuid();
        //    Member newMember = new Member(memberId);
        //    newMember.FirstName = "Jim";
        //    newMember.LastName = "Smith";
        //    controller.CreateMember(newMember, teamId);

        //    Member member = (Member)(controller.GetMember(teamId, memberId) as ObjectResult).Value;
        //    Assert.Equal(member.ID, newMember.ID);
        //}

        //[Fact]
        //public void GetMembersReturnsMembers() 
        //{
        //    ITeamRepository repository = new TestMemoryTeamRepository();
        //    MembersController controller = new MembersController(repository);

        //    Guid teamId = Guid.NewGuid();
        //    Team team = new Team("TestTeam", teamId);
        //    var debugTeam = repository.Add(team);        

        //    Guid firstMemberId = Guid.NewGuid();
        //    Member newMember = new Member(firstMemberId);
        //    newMember.FirstName = "Jim";
        //    newMember.LastName = "Smith";
        //    controller.CreateMember(newMember, teamId);

        //    Guid secondMemberId = Guid.NewGuid();
        //    newMember = new Member(secondMemberId);
        //    newMember.FirstName = "John";
        //    newMember.LastName = "Doe";
        //    controller.CreateMember(newMember, teamId);            

        //    ICollection<Member> members = (ICollection<Member>)(controller.GetMembers(teamId) as ObjectResult).Value;
        //    Assert.Equal(2, members.Count());
        //    Assert.NotEqual(Guid.Empty, members.Where(m => m.ID == firstMemberId).First().ID);
        //    Assert.NotEqual(Guid.Empty, members.Where(m => m.ID == secondMemberId).First().ID);
        //}

        //[Fact]
        //public void GetMembersForNewTeamIsEmpty() 
        //{
        //    ITeamRepository repository = new TestMemoryTeamRepository();
        //    MembersController controller = new MembersController(repository);

        //    Guid teamId = Guid.NewGuid();
        //    Team team = new Team("TestTeam", teamId);
        //    var debugTeam = repository.Add(team);        

        //    ICollection<Member> members = (ICollection<Member>)(controller.GetMembers(teamId) as ObjectResult).Value;
        //    Assert.Empty(members);
        //}     

        //[Fact]
        //public void GetMembersForNonExistantTeamReturnNotFound() 
        //{
        //    ITeamRepository repository = new TestMemoryTeamRepository();
        //    MembersController controller = new MembersController(repository);

        //    var result = controller.GetMembers(Guid.NewGuid());
        //    Assert.True(result is NotFoundResult);
        //}           

        //[Fact]
        //public void GetNonExistantTeamReturnsNotFound() 
        //{
        //    ITeamRepository repository = new TestMemoryTeamRepository();
        //    MembersController controller = new MembersController(repository);

        //    var result = controller.GetMember(Guid.NewGuid(), Guid.NewGuid());
        //    Assert.True(result is NotFoundResult);
        //}

        //[Fact]
        //public void GetNonExistantMemberReturnsNotFound() 
        //{
        //    ITeamRepository repository = new TestMemoryTeamRepository();
        //    MembersController controller = new MembersController(repository);

        //    Guid teamId = Guid.NewGuid();
        //    Team team = new Team("TestTeam", teamId);
        //    var debugTeam = repository.Add(team);        

        //    var result = controller.GetMember(teamId, Guid.NewGuid());
        //    Assert.True(result is NotFoundResult);
        //}

        //[Fact]
        //public void UpdateMemberOverwrites() 
        //{
        //    ITeamRepository repository = new TestMemoryTeamRepository();
        //    MembersController controller = new MembersController(repository);

        //    Guid teamId = Guid.NewGuid();
        //    Team team = new Team("TestTeam", teamId);
        //    var debugTeam = repository.Add(team);        

        //    Guid memberId = Guid.NewGuid();
        //    Member newMember = new Member(memberId);
        //    newMember.FirstName = "Jim";
        //    newMember.LastName = "Smith";
        //    controller.CreateMember(newMember, teamId);

        // team = repository.Get(teamId);

        //    Member updatedMember = new Member(memberId);
        //    updatedMember.FirstName = "Bob";
        //    updatedMember.LastName = "Jones";            
        //    controller.UpdateMember(updatedMember, teamId, memberId);

        //    team = repository.Get(teamId);
        //    Member testMember = team.Members.Where(m => m.ID == memberId).First();

        //    Assert.Equal("Bob", testMember.FirstName);
        //    Assert.Equal("Jones", testMember.LastName);            
        //}           

        //[Fact]
        //public void UpdateMembertoNonexistantMemberReturnsNoMatch() 
        //{
        //    ITeamRepository repository = new TestMemoryTeamRepository();
        //    MembersController controller = new MembersController(repository);

        //    Guid teamId = Guid.NewGuid();
        //    Team team = new Team("TestController", teamId);
        //    repository.Add(team);        

        //    Guid memberId = Guid.NewGuid();
        //    Member newMember = new Member(memberId);
        //    newMember.FirstName = "Jim";
        //    controller.CreateMember(newMember, teamId);

        //    Guid nonMatchedGuid = Guid.NewGuid();
        //    Member updatedMember = new Member(nonMatchedGuid);
        //    updatedMember.FirstName = "Bob";
        //    var result = controller.UpdateMember(updatedMember, teamId, nonMatchedGuid);            

        //    Assert.True(result is NotFoundResult);
        //}                   
    }
}