using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Option:IEntity
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string OptionText { get; set; }
        public bool Accuracy { get; set; }
    }
}
