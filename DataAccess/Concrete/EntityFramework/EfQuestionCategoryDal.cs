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
    public class EfQuestionCategoryDal : EfEntityRepositoryBase<QuestionCategory, SqlContext>, IQuestionCategoryDal
    {
        public QuestionCategoriesDto GetQuestionCategories(int questionId)
        {
            using (SqlContext context = new SqlContext())
            {
                //var result = from qc in context.QuestionCategories
                //             join c in context.Categories
                //             on qc.QuestionId equals questionId
                //             where 
                //return result.ToList();
                QuestionCategoriesDto dto = new QuestionCategoriesDto(); 
                var questionCategories = context.QuestionCategories.Where(qc => qc.QuestionId == questionId);

                foreach (var questionCategory in questionCategories)
                {
                    dto.Categories.Add(new Category
                    {
                        CategoryId=questionCategory.CategoryId,
                        CategoryName=context.Categories.FirstOrDefault(c=>c.CategoryId==questionCategory.CategoryId).CategoryName
                    });
                }

                return dto;

            }
        }
    }
}
