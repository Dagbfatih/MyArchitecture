using Core.Entities.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class TestResultDetailsDto:IDto
    {
        public TestResult ResultDetails { get; set; }
        public List<QuestionResultDetailsDto> QuestionResults{ get; set; }
        public Test TestDetails { get; set; }

    }
}
