using LineImpedance.StripLine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LineImpedance.Tests.StripLine
{
    [TestClass]
    public class SuspendedMicroStripCalculatorTests
    {
        [TestMethod]
        public void Calculate_NonInverted()
        {
            const double h = 2;
            const double h0 = 0.5;
            const double w = 4;
            const double eps = 2;
            const bool inverted = false;

            const double expected_impedance = 81.45;

            var calculator = new SuspendedMicroStripCalculator
            {
                Height = h,
                Height0 = h0,
                Width = w,
                Eps = eps,
                Inverted = inverted,
            };

            calculator.Calculate();

            var actual_impedance = calculator.Impedance;

            const double error = 1e-2;
            Assert.That.Value(actual_impedance).IsEqual(expected_impedance, error);
        }

        [TestMethod]
        public void Calculate_Inverted()
        {
            const double h = 2;
            const double h0 = 0.5;
            const double w = 4;
            const double eps = 2;
            const bool inverted = true;

            const double expected_impedance = 32.92;

            var calculator = new SuspendedMicroStripCalculator
            {
                Height = h,
                Height0 = h0,
                Width = w,
                Eps = eps,
                Inverted = inverted,
            };

            calculator.Calculate();

            var actual_impedance = calculator.Impedance;

            const double error = 1e-2;
            Assert.That.Value(actual_impedance).IsEqual(expected_impedance, error);
        }
    }
}
