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
    public class EfQuestionResultDal : EfEntityRepositoryBase<QuestionResult, SqlContext>, IQuestionResultDal
    {
        public List<QuestionResultDetailsDto> GetAllDetails()
        {
            using (SqlContext context = new SqlContext())
            {
                var result = from qr in context.QuestionResults
                             join q in context.Questions
                             on qr.QuestionId equals q.QuestionId
                             select new QuestionResultDetailsDto
                             {
                                 QuestionResult = qr,
                                 Question = new QuestionDetailsDto
                                 {
                                     Question = q,
                                     UserName = (from u in context.Users
                                                 where q.UserId == u.Id
                                                 select u.FirstName + " " + u.LastName).FirstOrDefault(),
                                     Options = (from o in context.Options
                                                where qr.QuestionId == o.QuestionId
                                                select new Option
                                                {
                                                    Id = o.Id,
                                                    QuestionId = o.QuestionId,
                                                    OptionText = o.OptionText,
                                                    Accuracy = o.Accuracy
                                                }).ToList()
                                 }
                             };
                return result.ToList();
            }
        }

        public List<QuestionResultDetailsDto> GetAllDetailsByTestResultId(int testResultId)
        {
            using (SqlContext context = new SqlContext())
            {
                var result = from qr in context.QuestionResults
                             join q in context.Questions
                             on qr.QuestionId equals q.QuestionId
                             where qr.TestResultId == testResultId
                             select new QuestionResultDetailsDto
                             {
                                 QuestionResult = qr,
                                 Question = new QuestionDetailsDto
                                 {
                                     Question = q,
                                     UserName = (from u in context.Users
                                                 where q.UserId == u.Id
                                                 select u.FirstName + " " + u.LastName).FirstOrDefault(),
                                     Options = (from o in context.Options
                                                where qr.QuestionId == o.QuestionId
                                                select new Option
                                                {
                                                    Id = o.Id,
                                                    QuestionId = o.QuestionId,
                                                    OptionText = o.OptionText,
                                                    Accuracy = o.Accuracy
                                                }).ToList()
                                 }
                             };
                return result.ToList();
            }
        }
    }
}
