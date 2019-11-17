using System;
using System.Diagnostics;

namespace Example__05.Decorator
{
    public class CalculatorDecoratorBase : ICalculator
    {
        protected readonly ICalculator Decoratee;

        protected CalculatorDecoratorBase(ICalculator calculator)
        {
            Decoratee = calculator;
        }

        public virtual void SetFunction(Func<double, double> func)
        {
            Decoratee.SetFunction(func);
        }

        public double Calculate(double input)
        {
            input = OnBeforeCalculate(input);
            var result = Decoratee.Calculate(input);
            return OnAfterCalculate(result);
        }

        protected virtual double OnBeforeCalculate(double input)
        {
            return input;
        }

        protected virtual double OnAfterCalculate(double result)
        {
            return result;
        }
    }

    public class TimerDecorator : CalculatorDecoratorBase
    {
        private readonly Stopwatch _watch;

        public TimerDecorator(ICalculator calculator) : base(calculator)
        {
            _watch = new Stopwatch();
        }

        protected override double OnBeforeCalculate(double input)
        {
            _watch.Start();
            return base.OnBeforeCalculate(input);
        }

        protected override double OnAfterCalculate(double result)
        {
            _watch.Stop();
            Console.WriteLine($"Compulation complete: {_watch.ElapsedMilliseconds}ms");
            return base.OnAfterCalculate(result);
        }
    }

    public class LoggerDecorator : CalculatorDecoratorBase
    {
        public LoggerDecorator(ICalculator calculator) : base(calculator)
        {
        }

        public override void SetFunction(Func<double, double> func)
        {
            Console.WriteLine($"Function has been set");
            Decoratee.SetFunction(func);
        }

        protected override double OnBeforeCalculate(double input)
        {
            Console.WriteLine($"Computed started with argument: {input}");
            return base.OnBeforeCalculate(input);
        }

        protected override double OnAfterCalculate(double result)
        {
            Console.WriteLine($"Computed finished with argument: {result}");
            return base.OnAfterCalculate(result);
        }
    }
}