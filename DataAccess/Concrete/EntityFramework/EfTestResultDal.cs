using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfTestResultDal : EfEntityRepositoryBase<TestResult, SqlContext>, ITestResultDal
    {
        IQuestionResultDal _questionResultDal;
        public EfTestResultDal(IQuestionResultDal questionResultDal)
        {
            _questionResultDal = questionResultDal;
        }

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
                                 QuestionResults = _questionResultDal.GetAllDetailsByTestResultId(tr.Id)
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
                             join qr in context.QuestionResults
                             on tr.Id equals qr.TestResultId
                             join q in context.Questions
                             on qr.QuestionId equals q.QuestionId
                             where tr.UserId == userId
                             select new TestResultDetailsDto
                             {
                                 ResultDetails = tr,
                                 TestDetails = t,
                                 QuestionResults = (_questionResultDal.GetAllDetailsByTestResultId(tr.Id))
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
                                                        QuestionResult = qr,
                                                        Question = (from q in context.Questions
                                                                    where qr.QuestionId == q.QuestionId
                                                                    select new QuestionDetailsDto
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
                                                                    }).FirstOrDefault()
                                                    }).ToList()
                             };
                return result.FirstOrDefault();
            }
        }
    }
}
