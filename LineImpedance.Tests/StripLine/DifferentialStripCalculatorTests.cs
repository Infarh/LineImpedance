using LineImpedance.StripLine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LineImpedance.Tests.StripLine
{
    [TestClass]
    public class DifferentialStripCalculatorTests
    {
        [TestMethod]
        public void Calculate_DtoT_greater_5()
        {
            const double h = 2;
            const double t = 0.1;
            const double w = 4;
            const double d = 1;
            const double eps = 2;

            const double expected_impedance = 49.10;

            var calculator = new DifferentialStripCalculator
            {
                Height = h,
                Thickness = t,
                Width = w,
                Delta = d,
                Eps = eps,
            };

            calculator.Calculate();

            var actual_impedance = calculator.Impedance;

            const double error = 1e-2;
            Assert.That.Value(actual_impedance).IsEqual(expected_impedance, error);
        }

        [TestMethod]
        public void Calculate_DtoT_less_5()
        {
            const double h = 2;
            const double t = 0.5;
            const double w = 4;
            const double d = 2.4;
            const double eps = 2;

            const double expected_impedance = 38.22;

            var calculator = new DifferentialStripCalculator
            {
                Height = h,
                Thickness = t,
                Width = w,
                Delta = d,
                Eps = eps,
            };

            calculator.Calculate();

            var actual_impedance = calculator.Impedance;

            const double error = 1e-2;
            Assert.That.Value(actual_impedance).IsEqual(expected_impedance, error);
        }
    }
}
