using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace ExchangeApiService.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{email}")]
        public ActionResult<string> Get(string email)
        {
            return email;
        }

    }
}
