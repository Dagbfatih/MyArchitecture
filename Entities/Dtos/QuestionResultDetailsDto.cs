using Core.Entities.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class QuestionResultDetailsDto:IDto
    {
        public int QuestionResultId { get; set; }
        public int TestResultId { get; set; }
        public int QuestionId { get; set; }
        public int SelectedOptionId { get; set; }
        public int CorrectOptionId { get; set; }
        public bool Accuracy { get; set; }
        public QuestionDetailsDto Question { get; set; }

    }
}
