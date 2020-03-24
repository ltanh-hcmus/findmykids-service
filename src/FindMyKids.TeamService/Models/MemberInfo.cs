using System;

namespace FindMyKids.TeamService.Models
{
    public class MemberInfo
    {
        public Guid ID { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}