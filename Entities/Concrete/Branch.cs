using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Branch:IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
