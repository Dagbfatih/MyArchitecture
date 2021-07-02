﻿using Core.DataAccess;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IQuestionDal:IEntityRepository<Question>
    {
        List<QuestionDetailsDto> GetQuestionDetails();
        QuestionDetailsDto GetQuestionDetailsById(int questionId);
        List<Question> GetQuestionsByCategoryId(int categoryId);
        QuestionCategoriesDto GetQuestionCategories(int questionId);
        List<QuestionDetailsDto> GetDetailsByQuestionText(string text);
        List<QuestionDetailsDto> GetDetailsByCategory(int categoryId);

    }
}
