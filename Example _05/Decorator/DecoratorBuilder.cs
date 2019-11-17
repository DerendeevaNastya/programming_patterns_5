namespace Example__05.Decorator
{
    public class DecoratorBuilder
    {
        private ICalculator _calculator;

        public DecoratorBuilder(ICalculator calculator)
        {
            _calculator = calculator;
        }

        public DecoratorBuilder WithTimer()
        {
            _calculator = new TimerDecorator(_calculator);
            return this;
        }

        public DecoratorBuilder WithLogger()
        {
            _calculator = new LoggerDecorator(_calculator);
            return this;
        }

        public ICalculator Build()
        {
            return _calculator;
        }

    }
}
