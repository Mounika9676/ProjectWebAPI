using System.ComponentModel.DataAnnotations;

namespace ProjectWebAPI.DTO
{
    public class UserResponseDTO
    {
        [Required]
        public int ResponseID { get; set; }

        [Required]
        public int TestID { get; set; }

        [Required]
        public int QuestionID { get; set; }

        [Required]
        [StringLength(255)]
        public string UserAnswer { get; set; }
    }
}
