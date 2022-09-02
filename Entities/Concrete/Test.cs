using Core.Entities.Abstract;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Test : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int TestTime { get; set; }
        public bool Privacy { get; set; }
        public int LessonId { get; set; }
        public DateTime Date { get; set; }
        public int GradeLevelId { get; set; }
        public int DifficultyLevelId { get; set; }

    }
}
