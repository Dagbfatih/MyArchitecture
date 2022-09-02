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
    public class WebsitesController : ControllerRepositoryBase<Website>
    {
        private IWebsiteService _websiteService;

        public WebsitesController(IWebsiteService websiteService) : base(websiteService)
        {
            _websiteService = websiteService;
        }
    }
}
