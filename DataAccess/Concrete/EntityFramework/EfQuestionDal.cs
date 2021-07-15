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
                                 QuestionId = q.QuestionId,

                                 Categories = (from c in context.Categories
                                               join qc in context.QuestionCategories
                                               on q.QuestionId equals qc.QuestionId
                                               where qc.CategoryId == c.CategoryId
                                               select new Category
                                               {
                                                   CategoryId = c.CategoryId,
                                                   CategoryName = c.CategoryName
                                               }).ToList(),

                                 QuestionText = q.QuestionText,

                                 StarQuestion = q.StarQuestion,

                                 BrokenQuestion = q.BrokenQuestion,

                                 Privacy = q.Privacy,

                                 UserId = q.UserId,
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
                                            }).ToList()
                             };


                return result.ToList();
            }
        }

        public QuestionDetailsDto GetQuestionDetailsById(int questionId)
        {
            using (SqlContext context = new SqlContext())
            {
                var result = new QuestionDetailsDto
                {
                    QuestionId = (from q in context.Questions
                                  where q.QuestionId == questionId
                                  select q.QuestionId).FirstOrDefault(),

                    Categories = (from c in context.Categories
                                  join qc in context.QuestionCategories
                                  on questionId equals qc.QuestionId
                                  where qc.CategoryId == c.CategoryId
                                  select new Category
                                  {
                                      CategoryId = c.CategoryId,
                                      CategoryName = c.CategoryName
                                  }).ToList(),

                    QuestionText = (from q in context.Questions
                                    where q.QuestionId == questionId
                                    select q.QuestionText).FirstOrDefault(),

                    StarQuestion = (from q in context.Questions
                                    where q.QuestionId == questionId
                                    select q.StarQuestion).FirstOrDefault(),

                    BrokenQuestion = (from q in context.Questions
                                      where q.QuestionId == questionId
                                      select q.BrokenQuestion).FirstOrDefault(),

                    Privacy = (from q in context.Questions
                               where q.QuestionId == questionId
                               select q.Privacy).FirstOrDefault(),

                    UserId = (from q in context.Questions
                              where q.QuestionId == questionId
                              select q.UserId).FirstOrDefault(),

                    Options = (from o in context.Options
                               where questionId == o.QuestionId
                               select new Option
                               {
                                   Id = o.Id,
                                   QuestionId = o.QuestionId,
                                   OptionText = o.OptionText,
                                   Accuracy = o.Accuracy
                               }).ToList()
                };
                return result;

            }
        }

        public List<Question> GetQuestionsByCategoryId(int categoryId) // intro
        {
            using (SqlContext context = new SqlContext())
            {
                var result = from qc in context.QuestionCategories
                             where qc.CategoryId == categoryId
                             join q in context.Questions
                             on qc.QuestionId equals q.QuestionId
                             select new Question
                             {
                                 QuestionId = q.QuestionId,
                                 QuestionText = q.QuestionText,
                                 BrokenQuestion = q.BrokenQuestion,
                                 StarQuestion = q.StarQuestion,
                                 Privacy = q.StarQuestion,
                                 UserId = q.UserId
                             };
                return result.ToList();
            }
        }

        public QuestionCategoriesDto GetQuestionCategories(int questionId)
        {
            using (SqlContext context = new SqlContext())
            {
                var result =
                             new QuestionCategoriesDto
                             {
                                 QuestionId = questionId,
                                 Categories = (from qc in context.QuestionCategories
                                               join c in context.Categories
                                               on 1 equals 1
                                               where qc.QuestionId == questionId
                                               where qc.CategoryId == c.CategoryId
                                               select new Category
                                               {
                                                   CategoryId = c.CategoryId,
                                                   CategoryName = c.CategoryName
                                               }).ToList()
                             };
                return result;
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
                                 QuestionId = q.QuestionId,

                                 Categories = (from c in context.Categories
                                               join qc in context.QuestionCategories
                                               on q.QuestionId equals qc.QuestionId
                                               where qc.CategoryId == c.CategoryId
                                               select new Category
                                               {
                                                   CategoryId = c.CategoryId,
                                                   CategoryName = c.CategoryName
                                               }).ToList(),

                                 QuestionText = q.QuestionText,

                                 StarQuestion = q.StarQuestion,

                                 BrokenQuestion = q.BrokenQuestion,

                                 Privacy = q.Privacy,

                                 UserId = q.UserId,
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
                                            }).ToList()
                             };


                return result.ToList();
            }
        }

        public List<QuestionDetailsDto> GetDetailsByCategory(int categoryId)
        {
            using (SqlContext context = new SqlContext())
            {
                var result = from q in context.Questions
                             join questionc in context.QuestionCategories
                             on q.QuestionId equals questionc.QuestionId
                             where categoryId == questionc.CategoryId
                             select new QuestionDetailsDto
                             {
                                 QuestionId = q.QuestionId,

                                 Categories = (from c in context.Categories
                                               join qc in context.QuestionCategories
                                               on q.QuestionId equals qc.QuestionId
                                               where qc.CategoryId == c.CategoryId
                                               select new Category
                                               {
                                                   CategoryId = c.CategoryId,
                                                   CategoryName = c.CategoryName
                                               }).ToList(),

                                 QuestionText = q.QuestionText,

                                 StarQuestion = q.StarQuestion,

                                 BrokenQuestion = q.BrokenQuestion,

                                 Privacy = q.Privacy,

                                 UserId = q.UserId,
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
                                            }).ToList()
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
                                 QuestionId = q.QuestionId,

                                 Categories = (from c in context.Categories
                                               join qc in context.QuestionCategories
                                               on q.QuestionId equals qc.QuestionId
                                               where qc.CategoryId == c.CategoryId
                                               select new Category
                                               {
                                                   CategoryId = c.CategoryId,
                                                   CategoryName = c.CategoryName
                                               }).ToList(),

                                 QuestionText = q.QuestionText,

                                 StarQuestion = q.StarQuestion,

                                 BrokenQuestion = q.BrokenQuestion,

                                 Privacy = q.Privacy,

                                 UserId = q.UserId,
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
                                            }).ToList()
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
                                 QuestionId = q.QuestionId,

                                 Categories = (from c in context.Categories
                                               join qc in context.QuestionCategories
                                               on q.QuestionId equals qc.QuestionId
                                               where qc.CategoryId == c.CategoryId
                                               select new Category
                                               {
                                                   CategoryId = c.CategoryId,
                                                   CategoryName = c.CategoryName
                                               }).ToList(),

                                 QuestionText = q.QuestionText,

                                 StarQuestion = q.StarQuestion,

                                 BrokenQuestion = q.BrokenQuestion,

                                 Privacy = q.Privacy,

                                 UserId = q.UserId,
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
                                            }).ToList()
                             };


                return result.ToList();
            }
        }
    }
}
