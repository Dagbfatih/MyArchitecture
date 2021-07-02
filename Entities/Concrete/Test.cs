using Core.Entities.Abstract;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Test:IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string TestName { get; set; }
        public string TestNotes { get; set; }
        public int TestTime { get; set; }
        public bool MixedCategory { get; set; }
        public bool Privacy { get; set; }

    }
}
