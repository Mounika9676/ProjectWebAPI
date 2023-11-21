using ProjectWebAPI.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectWebAPI.Entity
{
    public class Site
    {
        [Key]
        public int SiteID { get; set; }

        [Required]
        public int OrgID { get; set; }

        [ForeignKey(nameof(OrgID))]
        public Organization Organization { get; set; }

        [Required]
        [StringLength(255)]
        public string SiteName { get; set; }
        public ICollection<Subject> Subjects { get; set; }
    }
}