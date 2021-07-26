﻿using Core.Entities.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class CustomerForRegisterDto:IDto
    {
        public UserForRegisterDto User { get; set; }
        public Customer Customer{ get; set; }


    }
}
