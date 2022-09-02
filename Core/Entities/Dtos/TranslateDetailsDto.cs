using Core.Entities.Abstract;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Dtos
{
    public class TranslateDetailsDto : IDto
    {
        public Translate Translate { get; set; }
        public Language Language { get; set; }
    }
}
