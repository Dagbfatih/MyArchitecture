using Business.Abstract;
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
    public class DifficultiesController : ControllerRepositoryBase<Difficulty>
    {
        private readonly IDifficultyService _difficultyService;

        public DifficultiesController(IDifficultyService difficultyService) : base(difficultyService)
        {
            _difficultyService = difficultyService;
        }
    }
}
