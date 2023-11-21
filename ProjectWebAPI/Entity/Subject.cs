using ProjectWebAPI.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectWebAPI.Entity
{
    public class Subject
    {
        [Key]
        public int SubjectID { get; set; }

        [Required]
        public int SiteID { get; set; }

        [ForeignKey(nameof(SiteID))]
        public Site Site { get; set; }

        [Required]
        [StringLength(255)]
        public string SubjectName { get; set; }
    }


}