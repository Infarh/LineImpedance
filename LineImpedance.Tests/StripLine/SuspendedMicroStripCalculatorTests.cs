using LineImpedance.StripLine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LineImpedance.Tests.StripLine
{
    [TestClass]
    public class SuspendedMicroStripCalculatorTests
    {
        [TestMethod]
        public void Calculate()
        {
            const double h = 2;
            const double h0 = 0.5;
            const double w = 4;
            const double eps = 2;

            const double expected_impedance = 81.45;

            var calculator = new SuspendedMicroStripCalculator
            {
                Height = h,
                Height0 = h0,
                Width = w,
                Eps = eps,
            };

            calculator.Calculate();

            var actual_impedance = calculator.Impedance;

            const double error = 1e-2;
            Assert.That.Value(actual_impedance).IsEqual(expected_impedance, error);
        }
    }
}
