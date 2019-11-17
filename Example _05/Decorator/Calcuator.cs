using System;

namespace Example__05.Decorator
{
    public class Calcuator : ICalculator
    {
        private Func<double, double> _func;

        public void SetFunction(Func<double, double> func)
        {
            _func = func;
        }

        public double Calculate(double input)
        {
            return _func(input);
        }
    }
}
