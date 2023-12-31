﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.api.Controller
{
    [Route("[controller]")]
    public class DinnersController : ApiController
    {
        [HttpGet]
        [Authorize]
        public IActionResult ListDinners()
        {
            return Ok(Array.Empty<string>());
        }
    }
}
