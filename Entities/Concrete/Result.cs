using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Result:IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TestId { get; set; }

    }
}
