using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Difficulty : IEntity
    {
        public int Id { get; set; }
        public int DifficultyRank { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
