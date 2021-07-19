using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class QuestionResult:IEntity
    {
        public int Id { get; set; }
        public int TestResultId { get; set; }
        public int QuestionId { get; set; }
        public int SelectedOptionId { get; set; }
        public int CorrectOptionId { get; set; }
        public bool Accuracy { get; set; }

    }
}
