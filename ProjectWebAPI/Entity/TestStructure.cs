using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectWebAPI.Entity
{
    public class TestStructure
    {
        [Key]
        public int TestID { get; set; }

        [Required]
        public int SiteID { get; set; }

        [ForeignKey(nameof(SiteID))]
        public Site Site { get; set; }

        [Required]
        [StringLength(255)]
        public string TestName { get; set; }

        [Required]
        public int NoOfQuestions { get; set; }

        [Required]
        public int TotalMarks { get; set; }

        [Required]
        public int Duration { get; set; }
    }
}
