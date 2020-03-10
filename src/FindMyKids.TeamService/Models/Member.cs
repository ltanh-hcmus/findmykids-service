using System;

namespace FindMyKids.TeamService.Models
{
    public class Member {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string Email { get; set; }
    }
}