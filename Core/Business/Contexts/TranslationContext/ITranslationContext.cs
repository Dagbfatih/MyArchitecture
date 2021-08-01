using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Business.Contexts.TranslationContext
{
    public interface ITranslationContext
    {
        Dictionary<string, string> Translates { get; set; }
    }
}
