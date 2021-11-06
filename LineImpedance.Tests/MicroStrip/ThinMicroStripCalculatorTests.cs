using LineImpedance.MicroStrip;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LineImpedance.Tests.MicroStrip
{
    [TestClass]
    public class ThinMicroStripCalculatorTests
    {
        [TestMethod]
        public void W_less_2_pi_H_and_less_H()
        {
            const double w = 4;
            const double h = 5;
            const double t = 0;
            const double eps = 2;

            const double expected_impedance = 109.28;

            var calculator = new ThinMicroStripCalculator
            {
                Width = w,
                Height = h,
                Thickness = t,
                Eps = eps,
            };

            calculator.Calculate();

            var actual_impedance = calculator.Impedance;

            const double error = 1e-3;
            Assert.That.Value(actual_impedance).IsEqual(expected_impedance, error);
        }

        [TestMethod]
        public void W_between_H_and_2_Pi_H()
        {
            const double w = 10;
            const double h = 5;
            const double t = 0;
            const double eps = 2;

            const double expected_impedance = 68.77;

            var calculator = new ThinMicroStripCalculator
            {
                Width = w,
                Height = h,
                Thickness = t,
                Eps = eps,
            };

            calculator.Calculate();

            var actual_impedance = calculator.Impedance;

            const double error = 3.93e-3;
            Assert.That.Value(actual_impedance).IsEqual(expected_impedance, error);
        }

        [TestMethod]
        public void W_greater_H()
        {
            const double w = 20;
            const double h = 5;
            const double t = 0;
            const double eps = 2;

            const double expected_impedance = 43.69;

            var calculator = new ThinMicroStripCalculator
            {
                Width = w,
                Height = h,
                Thickness = t,
                Eps = eps,
            };

            calculator.Calculate();

            var actual_impedance = calculator.Impedance;

            const double error = 3.93e-3;
            Assert.That.Value(actual_impedance).IsEqual(expected_impedance, error);
        }

    }
}
