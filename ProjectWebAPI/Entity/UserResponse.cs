using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectWebAPI.Entity
{
    public class UserResponse
    {
        [Key]
        public int ResponseID { get; set; }

        [Required]
        public int TestID { get; set; }

        [ForeignKey(nameof(TestID))]
        public TestStructure AssignedTest { get; set; }

        [Required]
        public int QuestionID { get; set; }

        [ForeignKey(nameof(QuestionID))]
        public QuestionBank Question { get; set; }

        [Required]
        [StringLength(255)]
        public string UserAnswer { get; set; }
    }
}
