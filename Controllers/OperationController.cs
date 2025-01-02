﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationController : ControllerBase
    {
        [HttpGet]
        public decimal Get(decimal a, decimal b) 
        {
            return a + b;
        }

        [HttpPost]
        public int Add(Numbers numbers)
        {
            return numbers.A - numbers.B;
        }

        [HttpPut]
        public decimal Edit(decimal a, decimal b)
        {
            return a * b;
        }

        [HttpDelete]
        public decimal Delete(decimal a, decimal b)
        {
            return a / b;
        }

    }

    public class Numbers
    {
        public int A { get; set; }
        public int B { get; set; }
    }
}
