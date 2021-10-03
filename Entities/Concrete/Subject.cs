using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Subject : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LessonId { get; set; }
        public string Description { get; set; }

    }
}
