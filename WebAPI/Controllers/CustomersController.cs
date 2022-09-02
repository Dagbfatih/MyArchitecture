using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
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
    public class CustomersController : ControllerRepositoryBase<Customer>
    {
        ICustomerService _customerService;
        public CustomersController(ICustomerService customerService) : base(customerService)
        {
            _customerService = customerService;
        }

        
        [HttpGet("getallbyuserids")]
        [AllowAnonymous]
        public IActionResult GetAllByIds([FromQuery(Name = "userIds")] int[] userIds)
        {
            var result = _customerService.GetAllByUserIds(userIds);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("get")]
        public IActionResult Get(int id)
        {
            var result = _customerService.Get(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbyuser")]
        public IActionResult GetByUser(int userId)
        {
            var result = _customerService.GetByUser(userId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getdetailsbyuser")]
        public IActionResult GetDetailsByUser(int userId)
        {
            var result = _customerService.GetCustomerDetailsByUser(userId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("confirmaccount")]
        public IActionResult ConfirmAccount(Customer customer)
        {
            var result = _customerService.ConfirmAccount(customer);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
