using ProjectWebAPI.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProjectWebAPI.Entity
{
    //POCO class(Plain Old CLR Oject)
    [Table("Users")]
    public class User
    {
        //Scalar Properties
        [Key] //Primary key
        [StringLength(5)] //set size as 5
        [Column(TypeName = "char")]
        public string? UserId { get; set; }
        [Required]
        [StringLength(50)]
        [Column("Username", TypeName = "varchar")]
        public string? Name { get; set; }
        [Required]
        [StringLength(50)]
        [Column("Email", TypeName = "varchar")]
        public string? Email { get; set; }
        [Required]
        [StringLength(10)]
        [Column("Password", TypeName = "varchar")]
        public string? Password { get; set; }
        [Required]
        [StringLength(10)]
        [Column("Role", TypeName = "varchar")]
        public string? Role { get; set; }
        [Required]
        public int SiteID { get; set; }

        [ForeignKey(nameof(SiteID))]
        public Site Site { get; set; }
        //Navigation Properties
    }

} 