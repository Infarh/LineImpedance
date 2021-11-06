using LineImpedance.Coaxial;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LineImpedance.Tests.Coaxial
{
    [TestClass]
    public class CoaxialCalculatorTests
    {
        [TestMethod]
        public void Calculate_d1_1_d2_3_3_Impedance_50_62()
        {
            const double d1 = 1;
            const double d2 = 3.3;
            const double eps = 2;

            var calculator = new CoaxialCalculator
            {
                D1 = d1,
                D2 = d2,
                Eps = eps
            };

            const double expected_impedance = 50.62;

            calculator.Calculate();

            var actual_impedance = calculator.Impedance;

            const double error = 1e-2;
            Assert.That.Value(actual_impedance).IsEqual(expected_impedance, error);
        }
    }
}
