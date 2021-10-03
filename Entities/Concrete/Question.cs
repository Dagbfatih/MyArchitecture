﻿using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Question : IEntity
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public bool StarQuestion { get; set; }
        public bool BrokenQuestion { get; set; }
        public bool Privacy { get; set; }
        public int UserId { get; set; }
        public int LessonId { get; set; }
        public int SubjectId { get; set; }
        public int DifficultyLevel { get; set; }

    }
}
