using Core.Entities.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class TestDetailsDto : IDto
    {
        public Test Test { get; set; }
        public Difficulty Difficulty { get; set; }
        public GradeLevel GradeLevel { get; set; }
        public string UserName { get; set; }
        public List<QuestionDetailsDto> Questions { get; set; }

    }
}
