using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
namespace ProjectWebAPI.DTO
{
    public class AssignedTestDTO
    {
        public int AssignmentID { get; set; }
        public int TestID { get; set; }
        public string UserId { get; set; }
        public DateTime ScheduledDateTime { get; set; }
      
    }
}
