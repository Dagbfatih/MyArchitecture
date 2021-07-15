﻿using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Translate:IEntity
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public int LanguageId { get; set; }
        public string Value { get; set; }

    }
}
