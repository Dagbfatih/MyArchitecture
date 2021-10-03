using Core.Entities.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class QuestionResultDetailsDto:IDto
    {
        public QuestionResult QuestionResult { get; set; }
        public QuestionDetailsDto Question { get; set; }

    }
}
