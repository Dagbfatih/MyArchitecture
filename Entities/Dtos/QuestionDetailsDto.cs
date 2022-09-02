using Core.Entities.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class QuestionDetailsDto : IDto
    {
        public Question Question { get; set; }
        public List<Option> Options { get; set; }
        public GradeLevel GradeLevel { get; set; }
        public Difficulty Difficulty { get; set; }
        public Lesson Lesson { get; set; }
        public Subject Subject { get; set; }
        public string UserName { get; set; }
    }
}
