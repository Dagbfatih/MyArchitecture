using Core.Entities.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class QuestionCategoriesDto:IDto
    {
        public int QuestionId { get; set; }
        public List<Category> Categories { get; set; }

    }
}
