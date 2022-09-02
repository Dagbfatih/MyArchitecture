using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Concrete
{
    public class PathInfo:IEntity
    {
        public string FileName { get; set; }
        public string FullPath { get; set; }
    }
}
