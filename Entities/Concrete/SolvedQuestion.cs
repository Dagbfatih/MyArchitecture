using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class SolvedQuestion:IEntity
    {
        public int Id { get; set; }
        public int ResultId { get; set; }
        public int QuestionId { get; set; }
        public int OptionId { get; set; }
        public bool Accuracy { get; set; }

    }
}
