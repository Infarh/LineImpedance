using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LineImpedance.StripLine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LineImpedance.Tests.StripLine
{
    [TestClass]
    public class ThickCoplanarCalculatorTests
    {
        [TestMethod]
        public void Calculate()
        {
            const double h = 2;
            const double w = 34;
            const double t = 0.1;
            const double g = 1.1;
            const double eps = 2;

            const double expected_impedance = 50.08;

            var calculator = new ThickCoplanarCalculator
            {
                Height = h,
                Width = w,
                Thickness = t,
                Gap = g,
                Eps = eps
            };

            calculator.Calculate();

            var actual_impedance = calculator.Impedance;

            const double error = 1e-2;
            Assert.That.Value(actual_impedance).IsEqual(expected_impedance, error);
        }
    }
}
