using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProjectWebAPI.DTO;
using ProjectWebAPI.Entity;

namespace ProjectWebAPI.Repo
{
    public class UserResponseRepoImpl : IRepo<UserResponseDTO>
    {
        private readonly oExamDbContext context;

        public UserResponseRepoImpl(oExamDbContext ctx)
        {
            context = ctx;
        }

        public bool Add(UserResponseDTO item)
        {
            UserResponse userResponse = new UserResponse
            {
                TestID = item.TestID,
                QuestionID = item.QuestionID,
                UserAnswer = item.UserAnswer
            };

            context.UserResponses.Add(userResponse);
            context.SaveChanges();
            return true;
        }

        public List<UserResponseDTO> GetAll()
        {
            var result = context.UserResponses
                .Select(ur => new UserResponseDTO
                {
                    ResponseID = ur.ResponseID,
                    TestID = ur.TestID,
                    QuestionID = ur.QuestionID,
                    UserAnswer = ur.UserAnswer
                })
                .ToList();

            return result;
        }

        public UserResponseDTO GetById(int responseId)
        {
            var userResponse = context.UserResponses
                .Include(ur => ur.AssignedTest)
                .Include(ur => ur.Question)
                .FirstOrDefault(ur => ur.ResponseID == responseId);

            if (userResponse == null)
            {
                return null;
            }

            return new UserResponseDTO
            {
                ResponseID = userResponse.ResponseID,
                TestID = userResponse.TestID,
                QuestionID = userResponse.QuestionID,
                UserAnswer = userResponse.UserAnswer
            };
        }

        public bool Update(int responseId, UserResponseDTO updatedItem)
        {
            var userResponse = context.UserResponses.Find(responseId);

            if (userResponse == null)
            {
                return false; // UserResponse not found
            }

            userResponse.TestID = updatedItem.TestID;
            userResponse.QuestionID = updatedItem.QuestionID;
            userResponse.UserAnswer = updatedItem.UserAnswer;

            context.SaveChanges();
            return true;
        }

        public bool Delete(int responseId)
        {
            var userResponse = context.UserResponses.Find(responseId);

            if (userResponse == null)
            {
                return false; // UserResponse not found
            }

            context.UserResponses.Remove(userResponse);
            context.SaveChanges();
            return true;
        }

        public List<UserResponseDTO> GetByTestId(int testId)
        {
            var result = context.UserResponses
                .Where(ur => ur.TestID == testId)
                .Select(ur => new UserResponseDTO
                {
                    ResponseID = ur.ResponseID,
                    TestID = ur.TestID,
                    QuestionID = ur.QuestionID,
                    UserAnswer = ur.UserAnswer
                })
                .ToList();

            return result;
        }

        public List<UserResponseDTO> GetByQuestionId(int questionId)
        {
            var result = context.UserResponses
                .Where(ur => ur.QuestionID == questionId)
                .Select(ur => new UserResponseDTO
                {
                    ResponseID = ur.ResponseID,
                    TestID = ur.TestID,
                    QuestionID = ur.QuestionID,
                    UserAnswer = ur.UserAnswer
                })
                .ToList();

            return result;
        }
    }
}
