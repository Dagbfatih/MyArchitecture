using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfTestDal : EfEntityRepositoryBase<Test, SqlContext>, ITestDal
    {
        public List<TestDetailsDto> GetTestDetails()
        {
            using (SqlContext context = new SqlContext())
            {
                var result = from t in context.Tests
                             select new TestDetailsDto
                             {
                                 Test = t,
                                 UserName = (from u in context.Users
                                             where t.UserId == u.Id
                                             select u.FirstName + " " + u.LastName).FirstOrDefault(),

                                 Questions = (from tq in context.TestQuestions
                                              where t.Id == tq.TestId
                                              join q in context.Questions
                                              on tq.QuestionId equals q.QuestionId
                                              select new QuestionDetailsDto
                                              {
                                                  Question = q,
                                                  UserName = (from u in context.Users
                                                              where q.UserId == u.Id
                                                              select u.FirstName + " " + u.LastName).FirstOrDefault(),

                                                  Options = (from o in context.Options
                                                             where q.QuestionId == o.QuestionId
                                                             select new Option
                                                             {
                                                                 Id = o.Id,
                                                                 QuestionId = o.QuestionId,
                                                                 OptionText = o.OptionText,
                                                                 Accuracy = o.Accuracy
                                                             }).ToList(),
                                                  GradeLevel = (from g in context.GradeLevels
                                                                where q.GradeLevelId == g.Id
                                                                select g).FirstOrDefault(),
                                                  Difficulty = (from d in context.Difficulties
                                                                where q.DifficultyLevelId == d.Id
                                                                select d).FirstOrDefault()
                                              }).ToList(),
                                 GradeLevel = (from g in context.GradeLevels
                                               where t.GradeLevelId == g.Id
                                               select g).FirstOrDefault(),
                                 Difficulty = (from d in context.Difficulties
                                               where t.DifficultyLevelId == d.Id
                                               select d).FirstOrDefault()
                             };

                return result.ToList();
            }
        }

        public TestDetailsDto GetTestDetailsById(int id)
        {
            using (SqlContext context = new SqlContext())
            {
                var result = from t in context.Tests
                             where t.Id == id
                             select new TestDetailsDto
                             {
                                 Test = t,
                                 UserName = (from u in context.Users
                                             where t.UserId == u.Id
                                             select u.FirstName + " " + u.LastName).FirstOrDefault(),

                                 Questions = (from tq in context.TestQuestions
                                              where t.Id == tq.TestId
                                              join q in context.Questions
                                              on tq.QuestionId equals q.QuestionId
                                              select new QuestionDetailsDto
                                              {
                                                  Question = q,
                                                  UserName = (from u in context.Users
                                                              where q.UserId == u.Id
                                                              select u.FirstName + " " + u.LastName).FirstOrDefault(),

                                                  Options = (from o in context.Options
                                                             where q.QuestionId == o.QuestionId
                                                             select new Option
                                                             {
                                                                 Id = o.Id,
                                                                 QuestionId = o.QuestionId,
                                                                 OptionText = o.OptionText,
                                                                 Accuracy = o.Accuracy
                                                             }).ToList(),
                                                  GradeLevel = (from g in context.GradeLevels
                                                                where q.GradeLevelId == g.Id
                                                                select g).FirstOrDefault(),
                                                  Difficulty = (from d in context.Difficulties
                                                                where q.DifficultyLevelId == d.Id
                                                                select d).FirstOrDefault()
                                              }).ToList(),
                                 GradeLevel = (from g in context.GradeLevels
                                               where t.GradeLevelId == g.Id
                                               select g).FirstOrDefault(),
                                 Difficulty = (from d in context.Difficulties
                                               where t.DifficultyLevelId == d.Id
                                               select d).FirstOrDefault()
                             };

                return result.FirstOrDefault();
            }
        }

        public List<TestDetailsDto> GetTestDetailsByUser(int userId)
        {
            using (SqlContext context = new SqlContext())
            {
                var result = from t in context.Tests
                             where t.UserId == userId
                             select new TestDetailsDto
                             {
                                 Test = t,
                                 UserName = (from u in context.Users
                                             where t.UserId == u.Id
                                             select u.FirstName + " " + u.LastName).FirstOrDefault(),

                                 Questions = (from tq in context.TestQuestions
                                              where t.Id == tq.TestId
                                              join q in context.Questions
                                              on tq.QuestionId equals q.QuestionId
                                              select new QuestionDetailsDto
                                              {
                                                  Question = q,
                                                  UserName = (from u in context.Users
                                                              where q.UserId == u.Id
                                                              select u.FirstName + " " + u.LastName).FirstOrDefault(),

                                                  Options = (from o in context.Options
                                                             where q.QuestionId == o.QuestionId
                                                             select new Option
                                                             {
                                                                 Id = o.Id,
                                                                 QuestionId = o.QuestionId,
                                                                 OptionText = o.OptionText,
                                                                 Accuracy = o.Accuracy
                                                             }).ToList(),
                                                  GradeLevel = (from g in context.GradeLevels
                                                                where q.GradeLevelId == g.Id
                                                                select g).FirstOrDefault(),
                                                  Difficulty = (from d in context.Difficulties
                                                                where q.DifficultyLevelId == d.Id
                                                                select d).FirstOrDefault()
                                              }).ToList(),
                                 GradeLevel = (from g in context.GradeLevels
                                               where t.GradeLevelId == g.Id
                                               select g).FirstOrDefault(),
                                 Difficulty = (from d in context.Difficulties
                                               where t.DifficultyLevelId == d.Id
                                               select d).FirstOrDefault()
                             };

                return result.ToList();
            }
        }
    }
}
