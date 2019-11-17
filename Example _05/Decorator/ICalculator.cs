using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example__05.Decorator
{
    public interface ICalculator
    {
        void SetFunction(Func<double, double> func);
        double Calculate(double input);
    }
}
