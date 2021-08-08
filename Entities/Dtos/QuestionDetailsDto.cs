using Core.Entities.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class QuestionDetailsDto:IDto
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public List<Category> Categories { get; set; }
        public List<Option> Options { get; set; }
        public bool StarQuestion { get; set; }
        public bool BrokenQuestion { get; set; }
        public bool Privacy { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public Branch Branch { get; set; }

    }
}
