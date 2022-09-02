﻿using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradeLevelsController : ControllerRepositoryBase<GradeLevel>
    {
        private readonly IGradeLevelService _gradeLevelService;

        public GradeLevelsController(IGradeLevelService gradeLevelService) : base(gradeLevelService)
        {
            _gradeLevelService = gradeLevelService;
        }
    }
}
