using Core.DataAccess.EntityFramework;
using Core.Utilities.Results.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Core.Entities.Concrete;

namespace DataAccess.Concrete
{
    public class EfQuestionDal : EfEntityRepositoryBase<Question, SqlContext>, IQuestionDal
    {
        public List<QuestionDetailsDto> GetQuestionDetails()
        {
            using (SqlContext context = new SqlContext())
            {
                var result = from q in context.Questions
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
                                               select d).FirstOrDefault(),
                                 Lesson = (from l in context.Lessons
                                           where q.LessonId == l.Id
                                           select l).FirstOrDefault(),
                                 Subject = (from s in context.Subjects
                                            where q.SubjectId == s.Id
                                            select s).FirstOrDefault()

                             };


                return result.ToList();
            }
        }

        public QuestionDetailsDto GetQuestionDetailsById(int questionId)
        {
            using (SqlContext context = new SqlContext())
            {
                var result = from q in context.Questions
                             where q.QuestionId == questionId
                             select new QuestionDetailsDto
                             {

                                 Question = q,

                                 Options = (from o in context.Options
                                            where questionId == o.QuestionId
                                            select new Option
                                            {
                                                Id = o.Id,
                                                QuestionId = o.QuestionId,
                                                OptionText = o.OptionText,
                                                Accuracy = o.Accuracy
                                            }).ToList(),
                                 UserName = (from u in context.Users
                                             where q.UserId == u.Id
                                             select new string((u.FirstName + " " + u.LastName)
                                             .ToCharArray())).FirstOrDefault(),
                                 GradeLevel = (from g in context.GradeLevels
                                               where q.GradeLevelId == g.Id
                                               select g).FirstOrDefault(),
                                 Difficulty = (from d in context.Difficulties
                                               where q.DifficultyLevelId == d.Id
                                               select d).FirstOrDefault(),
                                 Lesson = (from l in context.Lessons
                                           where q.LessonId == l.Id
                                           select l).FirstOrDefault(),
                                 Subject = (from s in context.Subjects
                                            where q.SubjectId == s.Id
                                            select s).FirstOrDefault()
                             };

                return result.FirstOrDefault();
            }
        }



        public List<QuestionDetailsDto> GetDetailsByQuestionText(string text)
        {
            using (SqlContext context = new SqlContext())
            {
                var result = from q in context.Questions
                             where q.QuestionText.Contains(text)
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
                                               select d).FirstOrDefault(),
                                 Lesson = (from l in context.Lessons
                                           where q.LessonId == l.Id
                                           select l).FirstOrDefault(),
                                 Subject = (from s in context.Subjects
                                            where q.SubjectId == s.Id
                                            select s).FirstOrDefault()

                             };


                return result.ToList();
            }
        }

        public List<QuestionDetailsDto> GetAllDetailsByPublic()
        {
            using (SqlContext context = new SqlContext())
            {
                var result = from q in context.Questions
                             where q.Privacy == true // public
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
                                               select d).FirstOrDefault(),
                                 Lesson = (from l in context.Lessons
                                           where q.LessonId == l.Id
                                           select l).FirstOrDefault(),
                                 Subject = (from s in context.Subjects
                                            where q.SubjectId == s.Id
                                            select s).FirstOrDefault()
                             };


                return result.ToList();
            }
        }

        public List<QuestionDetailsDto> GetQuestionDetailsByUser(int userId)
        {
            using (SqlContext context = new SqlContext())
            {
                var result = from q in context.Questions
                             where q.UserId == userId
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
                                               select d).FirstOrDefault(),
                                 Lesson = (from l in context.Lessons
                                           where q.LessonId == l.Id
                                           select l).FirstOrDefault(),
                                 Subject = (from s in context.Subjects
                                            where q.SubjectId == s.Id
                                            select s).FirstOrDefault()
                             };


                return result.ToList();
            }
        }

        public List<QuestionDetailsDto> GetAllDetailsBySubjects(params int[] subjects)
        {
            using (SqlContext context = new SqlContext())
            {
                var result = from q in context.Questions
                             where subjects.Contains(q.SubjectId)
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
                                               select d).FirstOrDefault(),
                                 Lesson = (from l in context.Lessons
                                           where q.LessonId == l.Id
                                           select l).FirstOrDefault(),
                                 Subject = (from s in context.Subjects
                                            where q.SubjectId == s.Id
                                            select s).FirstOrDefault()
                             };


                return result.ToList();
            }
        }
    }
}
