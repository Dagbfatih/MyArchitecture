using Core.Utilities.IoC;
using Core.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Utilities.Services.Translate
{

    public class CoreMessagesService
    {
        protected readonly CoreMessages _coreMessages;

        public CoreMessagesService()
        {
            _coreMessages = ServiceTool.ServiceProvider.GetService<CoreMessages>();
        }
    }
}
