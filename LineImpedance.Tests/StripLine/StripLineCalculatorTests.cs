using LineImpedance.StripLine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LineImpedance.Tests.StripLine
{
    [TestClass]
    public class StripLineCalculatorTests
    {
        [TestMethod]
        public void Calculate_W_1_7_H_2_Eps_2_Impedance_51_63()
        {
            const double h = 2;
            const double w = 1.7;
            const double eps = 2;

            const double expected_impedance = 51.63;

            var calculator = new StripCalculator
            {
                Height = h,
                Width = w,
                Eps = eps
            };

            calculator.Calculate();

            var actual_impedance = calculator.Impedance;

            const double error = 4.16e-003;
            Assert.That.Value(actual_impedance).IsEqual(expected_impedance, error);
        }
    }
}
