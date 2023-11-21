using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProjectWebAPI.DTO;
using ProjectWebAPI.Entity;

namespace ProjectWebAPI.Repo
{
    public class AssignedTestRepoImpl : IRepo<AssignedTestDTO>
    {
        private readonly oExamDbContext context;

        public AssignedTestRepoImpl(oExamDbContext ctx)
        {
            context = ctx;
        }

        public bool Add(AssignedTestDTO item)
        {
            AssignedTest assignedTest = new AssignedTest
            {
                TestID = item.TestID,
                UserId = item.UserId,
                ScheduledDateTime = item.ScheduledDateTime
            };

            context.AssignedTests.Add(assignedTest);
            context.SaveChanges();
            return true;
        }

        public List<AssignedTestDTO> GetAll()
        {
            var result = context.AssignedTests
                .Include(at => at.Test)
                .Include(at => at.User)
                .Select(at => new AssignedTestDTO
                {
                    AssignmentID = at.AssignmentID,
                    TestID = at.TestID,
                    UserId = at.UserId,
                    ScheduledDateTime = at.ScheduledDateTime,
                })
                .ToList();

            return result;
        }

        public AssignedTestDTO GetById(int assignmentId)
        {
            var assignedTest = context.AssignedTests
                .Include(at => at.Test)
                .Include(at => at.User)
                .FirstOrDefault(at => at.AssignmentID == assignmentId);

            if (assignedTest == null)
            {
                return null;
            }

            return new AssignedTestDTO
            {
                AssignmentID = assignedTest.AssignmentID,
                TestID = assignedTest.TestID,
                UserId = assignedTest.UserId,
                ScheduledDateTime = assignedTest.ScheduledDateTime,
            };
        }

        public bool Update(int assignmentId, AssignedTestDTO updatedItem)
        {
            var assignedTest = context.AssignedTests.Find(assignmentId);

            if (assignedTest == null)
            {
                return false; // AssignedTest not found
            }

            assignedTest.TestID = updatedItem.TestID;
            assignedTest.UserId = updatedItem.UserId;
            assignedTest.ScheduledDateTime = updatedItem.ScheduledDateTime;

            context.SaveChanges();
            return true;
        }

        public bool Delete(int assignmentId)
        {
            var assignedTest = context.AssignedTests.Find(assignmentId);

            if (assignedTest == null)
            {
                return false; // AssignedTest not found
            }

            context.AssignedTests.Remove(assignedTest);
            context.SaveChanges();
            return true;
        }
    }
}