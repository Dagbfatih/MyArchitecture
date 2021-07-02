using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class TestQuestion:IEntity
    {
        public int Id { get; set; }
        public int TestId { get; set; }
        public int QuestionId { get; set; }

    }
}
