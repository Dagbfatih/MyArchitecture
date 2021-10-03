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
        public string UserName { get; set; }
    }
}
