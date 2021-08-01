using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Business.Contexts.TranslationContext
{
    public class TranslationContext : ITranslationContext
    {
        public Dictionary<string, string> Translates { get; set; } = new Dictionary<string, string>();
    }
}
