using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculatorApp.Services
{
    public interface IService
    {
        public double AddNumbers(double num1, double num2);
        public double MultiplyNumbers(double num1, double num2);
        public double DivideNumbers(double num1, double num2);
    }
}
