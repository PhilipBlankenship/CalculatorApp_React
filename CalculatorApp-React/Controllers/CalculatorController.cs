using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CalculatorApp.Services;
using System.Net;

namespace CalculatorApp.Controller
{
    [Route("api/calculator")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {

        private readonly IService CalculatorService;
        private static double? cachedNumber = null;

        public CalculatorController(ServiceImpl service)
        {
            this.CalculatorService = service;
        }

        [HttpPost("AddNumbers")]
        public ActionResult<double> AddNumbers([FromQuery] double num1, [FromQuery] double? num2 = null)
        {
            try
            {
                double? results = null;

                if (num2.HasValue)
                    results = CalculatorService.AddNumbers(num1, num2.Value);
                else if (cachedNumber.HasValue)
                    results = CalculatorService.AddNumbers(cachedNumber.Value, num1);
                else
                    throw new Exception("Only one number supplied but no cached number exists");

                cachedNumber = results;
                return StatusCode((int)HttpStatusCode.OK, results);

            }
            catch( Exception e)
            {
                Console.Out.WriteLine(e.Message);
                return StatusCode((int) HttpStatusCode.BadRequest, 0.0);
            }
        }

        [HttpPost("SubtractNumbers")]
        public ActionResult<double> SubtractNumbers([FromQuery] double num1, [FromQuery] double? num2 = null)
        {
            try
            {
                double? results = null;

                if (num2.HasValue)
                    results = CalculatorService.AddNumbers(num1, -1.0 * num2.Value);
                else if (cachedNumber.HasValue)
                    results = CalculatorService.AddNumbers(cachedNumber.Value, -1.0 * num1);
                else
                    throw new Exception("Only one number supplied but no cached number exists");

                cachedNumber = results;
                return StatusCode((int)HttpStatusCode.OK, results);

            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e.Message);
                return StatusCode((int)HttpStatusCode.BadRequest, 0.0);
            }
        }

        [HttpPost("MultiplyNumbers")]
        public ActionResult<double> MultiplyNumbers([FromQuery] double num1, [FromQuery] double? num2 = null)
        {
            try
            {
                double? results = null;

                if (num2.HasValue)
                    results = CalculatorService.MultiplyNumbers(num1, num2.Value);
                else if (cachedNumber.HasValue)
                    results = CalculatorService.MultiplyNumbers(cachedNumber.Value, num1);
                else
                    throw new Exception("Only one number supplied but no cached number exists");

                cachedNumber = results;
                return StatusCode((int)HttpStatusCode.OK, results);

            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e.Message);
                return StatusCode((int)HttpStatusCode.BadRequest, 0.0);
            }
        }

        [HttpPost("DivideNumbers")]
        public ActionResult<double> DivideNumbers([FromQuery] double num1, [FromQuery] double? num2 = null)
        {
            try
            {
                double? results = null;

                if (num2.HasValue)
                    results = CalculatorService.DivideNumbers(num1, num2.Value);
                else if (cachedNumber.HasValue)
                    results = CalculatorService.DivideNumbers(cachedNumber.Value, num1);
                else
                    throw new Exception("Only one number supplied but no cached number exists");

                cachedNumber = results;
                return StatusCode((int)HttpStatusCode.OK, results);

            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e.Message);
                return StatusCode((int)HttpStatusCode.BadRequest, 0.0);
            }
        }

        [HttpPost("StoreNumber")]
        public ActionResult<bool> StoreNumber([FromQuery] double? num)
        {
            try 
            {
                cachedNumber = num;
                return StatusCode((int)HttpStatusCode.OK, true);
            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e.Message);
                return StatusCode((int)HttpStatusCode.BadRequest, 0.0);
            }
        }

        [HttpGet("RetrieveNumber")]
        public ActionResult<double?> RetrieveNumber()
        {
            try
            {
                return StatusCode((int)HttpStatusCode.OK, cachedNumber);
            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e.Message);
                return StatusCode((int)HttpStatusCode.BadRequest, null);
            }
        }


    }
}
