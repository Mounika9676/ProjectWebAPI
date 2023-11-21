using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProjectWebAPI.Entity
{
    public class AssignedTest
    {
        [Key]
        public int AssignmentID { get; set; }

        [Required]
        public int TestID { get; set; }

        [ForeignKey(nameof(TestID))]
        public TestStructure Test { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        [Required]
        public User User { get; set; }

        [Required]
        public DateTime ScheduledDateTime { get; set; }

        public void Reschedule(DateTime newTime)
        {
            if (newTime > DateTime.Now) // Check if the new time is in the future
            {

                ScheduledDateTime = newTime;

                Console.WriteLine($"Test {TestID} rescheduled for {ScheduledDateTime}.");
            }
            else
            {
                throw new InvalidOperationException("New scheduled time must be in the future.");
            }
        }
    }
}


