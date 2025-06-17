using System.ComponentModel.DataAnnotations;

namespace BusinessObjects
{
    public class AccountMember
    {
        [Key]
        [StringLength(20)]
        public required string MemberId { get; set; }

        [Required]
        [StringLength(80)]
        public required string MemberPassword { get; set; }

        [Required]
        [StringLength(80)]
        public required string FullName { get; set; }

        [Required]
        [StringLength(100)]
        public required string EmailAddress { get; set; }

        [Required]
        public int MemberRole { get; set; }
    }
}