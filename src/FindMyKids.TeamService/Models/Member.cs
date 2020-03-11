using System;
using System.ComponentModel.DataAnnotations;

namespace FindMyKids.TeamService.Models
{
    public class Member
    {
        public string SiteKey { get; set; }
        public Guid ID { get; set; }
        [Required(AllowEmptyStrings = false)]
        [MinLength(5)]
        [MaxLength(50)]
        public string UserName { get; set; }
        [Required(AllowEmptyStrings = false)]
        [MinLength(8)]
        [MaxLength(16)]
        public string PassWord { get; set; }
        [Required(AllowEmptyStrings = false)]
        [MinLength(8)]
        [MaxLength(16)]
        public string Email { get; set; }
        [Required(AllowEmptyStrings = false)]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}