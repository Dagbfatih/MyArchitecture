using Core.Entities.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class TestDetailsDto:IDto
    {
        public int TestId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string TestName { get; set; }
        public string TestNotes { get; set; }
        public int TestTime { get; set; }
        public bool MixedCategory { get; set; }
        public bool Privacy { get; set; }
        public Branch Branch { get; set; }
        public List<QuestionDetailsDto> Questions { get; set; }

    }
}
