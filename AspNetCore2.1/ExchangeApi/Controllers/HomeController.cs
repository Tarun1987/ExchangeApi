using System;
using System.Collections.Generic;
using ExchangeApiService.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ExchangeApiService.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration Configuration;

        public HomeController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{email}")]
        public ActionResult Get(string email)
        {
            if (!IsValidRequest())
                return BadRequest("Invalid request");

            return Ok("Email");
        }


        private bool IsValidRequest()
        {
            try
            {
                var authenticationKey = Request.Headers["Authorization"];
                if (string.IsNullOrWhiteSpace(authenticationKey))
                    return false;

                var arr = authenticationKey.ToString().Split(new string[] { "Basic ", "basic " }, StringSplitOptions.RemoveEmptyEntries);
                if (arr.Length <= 0) return false;

                var tokenValue = Base64Helper.Decode(arr[0]);
                if (tokenValue == Configuration["TokenKey"])
                    return true;

                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

    }
}
