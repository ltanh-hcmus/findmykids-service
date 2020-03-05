using System;
using System.ComponentModel.DataAnnotations;

namespace FindMyKids.FamilyService.Models
{
    public class Member
    {
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
        public string RePassWord { get; set; }
        [Required(AllowEmptyStrings = false)]
        [MaxLength(150)]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$")]
        public string Email { get; set; }
        [Required(AllowEmptyStrings = false)]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}