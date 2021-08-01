using Business.Constants;
using Core.Utilities.IoC;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Services
{
    public class BusinessMessagesService
    {
        protected readonly Messages _messages;
        public BusinessMessagesService()
        {
            _messages = ServiceTool.ServiceProvider.GetService<Messages>();
        }
    }
}
