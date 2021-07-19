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
    public class EfTestResultDal : EfEntityRepositoryBase<TestResult, SqlContext>, ITestResultDal
    {
        public List<TestResultDetailsDto> GetAllDetails()
        {
            using (SqlContext context = new SqlContext())
            {
                var result = from tr in context.TestResults
                             join t in context.Tests
                             on tr.TestId equals t.Id
                             select new TestResultDetailsDto
                             {
                                 ResultDetails = tr,
                                 TestDetails = t,
                                 QuestionResults = (from qr in context.QuestionResults
                                                    where qr.TestResultId == tr.Id
                                                    select new QuestionResultDetailsDto
                                                    {
                                                        QuestionId = qr.QuestionId,
                                                        TestResultId = qr.TestResultId,
                                                        Accuracy = qr.Accuracy,
                                                        CorrectOptionId = qr.CorrectOptionId,
                                                        QuestionResultId = qr.Id,
                                                        SelectedOptionId = qr.SelectedOptionId,
                                                        Question = (new QuestionDetailsDto
                                                        {
                                                            QuestionId = (from q in context.Questions
                                                                          where q.QuestionId == qr.QuestionId
                                                                          select q.QuestionId).FirstOrDefault(),

                                                            Categories = (from c in context.Categories
                                                                          join qc in context.QuestionCategories
                                                                          on qr.QuestionId equals qc.QuestionId
                                                                          where qc.CategoryId == c.CategoryId
                                                                          select new Category
                                                                          {
                                                                              CategoryId = c.CategoryId,
                                                                              CategoryName = c.CategoryName
                                                                          }).ToList(),

                                                            QuestionText = (from q in context.Questions
                                                                            where q.QuestionId == qr.QuestionId
                                                                            select q.QuestionText).FirstOrDefault(),

                                                            StarQuestion = (from q in context.Questions
                                                                            where q.QuestionId == qr.QuestionId
                                                                            select q.StarQuestion).FirstOrDefault(),

                                                            BrokenQuestion = (from q in context.Questions
                                                                              where q.QuestionId == qr.QuestionId
                                                                              select q.BrokenQuestion).FirstOrDefault(),

                                                            Privacy = (from q in context.Questions
                                                                       where q.QuestionId == qr.QuestionId
                                                                       select q.Privacy).FirstOrDefault(),

                                                            UserId = (from q in context.Questions
                                                                      where q.QuestionId == qr.QuestionId
                                                                      select q.UserId).FirstOrDefault(),

                                                            Options = (from o in context.Options
                                                                       where qr.QuestionId == o.QuestionId
                                                                       select new Option
                                                                       {
                                                                           Id = o.Id,
                                                                           QuestionId = o.QuestionId,
                                                                           OptionText = o.OptionText,
                                                                           Accuracy = o.Accuracy
                                                                       }).ToList()
                                                        })
                                                    }).ToList()
                             };
                return result.ToList();
            }
        }

        public List<TestResultDetailsDto> GetAllDetailsByUser(int userId)
        {
            using (SqlContext context = new SqlContext())
            {
                var result = from tr in context.TestResults
                             join t in context.Tests
                             on tr.TestId equals t.Id
                             where tr.UserId == userId
                             select new TestResultDetailsDto
                             {
                                 ResultDetails = tr,
                                 TestDetails = t,
                                 QuestionResults = (from qr in context.QuestionResults
                                                    where qr.TestResultId == tr.Id
                                                    select new QuestionResultDetailsDto
                                                    {
                                                        QuestionId = qr.QuestionId,
                                                        TestResultId = qr.TestResultId,
                                                        Accuracy = qr.Accuracy,
                                                        CorrectOptionId = qr.CorrectOptionId,
                                                        QuestionResultId = qr.Id,
                                                        SelectedOptionId = qr.SelectedOptionId,
                                                        Question = (new QuestionDetailsDto
                                                        {
                                                            QuestionId = (from q in context.Questions
                                                                          where q.QuestionId == qr.QuestionId
                                                                          select q.QuestionId).FirstOrDefault(),

                                                            Categories = (from c in context.Categories
                                                                          join qc in context.QuestionCategories
                                                                          on qr.QuestionId equals qc.QuestionId
                                                                          where qc.CategoryId == c.CategoryId
                                                                          select new Category
                                                                          {
                                                                              CategoryId = c.CategoryId,
                                                                              CategoryName = c.CategoryName
                                                                          }).ToList(),

                                                            QuestionText = (from q in context.Questions
                                                                            where q.QuestionId == qr.QuestionId
                                                                            select q.QuestionText).FirstOrDefault(),

                                                            StarQuestion = (from q in context.Questions
                                                                            where q.QuestionId == qr.QuestionId
                                                                            select q.StarQuestion).FirstOrDefault(),

                                                            BrokenQuestion = (from q in context.Questions
                                                                              where q.QuestionId == qr.QuestionId
                                                                              select q.BrokenQuestion).FirstOrDefault(),

                                                            Privacy = (from q in context.Questions
                                                                       where q.QuestionId == qr.QuestionId
                                                                       select q.Privacy).FirstOrDefault(),

                                                            UserId = (from q in context.Questions
                                                                      where q.QuestionId == qr.QuestionId
                                                                      select q.UserId).FirstOrDefault(),

                                                            Options = (from o in context.Options
                                                                       where qr.QuestionId == o.QuestionId
                                                                       select new Option
                                                                       {
                                                                           Id = o.Id,
                                                                           QuestionId = o.QuestionId,
                                                                           OptionText = o.OptionText,
                                                                           Accuracy = o.Accuracy
                                                                       }).ToList()
                                                        })
                                                    }).ToList()
                             };
                return result.ToList();
            }
        }

        public TestResultDetailsDto GetDetailsById(int id)
        {
            using (SqlContext context = new SqlContext())
            {
                var result = from tr in context.TestResults
                             join t in context.Tests
                             on tr.TestId equals t.Id
                             where tr.Id == id
                             select new TestResultDetailsDto
                             {
                                 ResultDetails = tr,
                                 TestDetails = t,
                                 QuestionResults = (from qr in context.QuestionResults
                                                    where qr.TestResultId == tr.Id
                                                    select new QuestionResultDetailsDto
                                                    {
                                                        QuestionId = qr.QuestionId,
                                                        TestResultId = qr.TestResultId,
                                                        Accuracy = qr.Accuracy,
                                                        CorrectOptionId = qr.CorrectOptionId,
                                                        QuestionResultId = qr.Id,
                                                        SelectedOptionId = qr.SelectedOptionId,
                                                        Question = (new QuestionDetailsDto
                                                        {
                                                            QuestionId = (from q in context.Questions
                                                                          where q.QuestionId == qr.QuestionId
                                                                          select q.QuestionId).FirstOrDefault(),

                                                            Categories = (from c in context.Categories
                                                                          join qc in context.QuestionCategories
                                                                          on qr.QuestionId equals qc.QuestionId
                                                                          where qc.CategoryId == c.CategoryId
                                                                          select new Category
                                                                          {
                                                                              CategoryId = c.CategoryId,
                                                                              CategoryName = c.CategoryName
                                                                          }).ToList(),

                                                            QuestionText = (from q in context.Questions
                                                                            where q.QuestionId == qr.QuestionId
                                                                            select q.QuestionText).FirstOrDefault(),

                                                            StarQuestion = (from q in context.Questions
                                                                            where q.QuestionId == qr.QuestionId
                                                                            select q.StarQuestion).FirstOrDefault(),

                                                            BrokenQuestion = (from q in context.Questions
                                                                              where q.QuestionId == qr.QuestionId
                                                                              select q.BrokenQuestion).FirstOrDefault(),

                                                            Privacy = (from q in context.Questions
                                                                       where q.QuestionId == qr.QuestionId
                                                                       select q.Privacy).FirstOrDefault(),

                                                            UserId = (from q in context.Questions
                                                                      where q.QuestionId == qr.QuestionId
                                                                      select q.UserId).FirstOrDefault(),

                                                            Options = (from o in context.Options
                                                                       where qr.QuestionId == o.QuestionId
                                                                       select new Option
                                                                       {
                                                                           Id = o.Id,
                                                                           QuestionId = o.QuestionId,
                                                                           OptionText = o.OptionText,
                                                                           Accuracy = o.Accuracy
                                                                       }).ToList()
                                                        })
                                                    }).ToList()
                             };
                return result.FirstOrDefault();
            }
        }
    }
}
