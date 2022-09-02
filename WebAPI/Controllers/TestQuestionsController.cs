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
    public class TestQuestionsController : ControllerRepositoryBase<TestQuestion>
    {
        ITestQuestionService _testQuestionService;
        public TestQuestionsController(ITestQuestionService testQuestionService) : base(testQuestionService)
        {
            _testQuestionService = testQuestionService;
        }
    }
}
