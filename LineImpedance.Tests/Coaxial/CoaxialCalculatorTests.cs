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
            var calculator = new CoaxialCalculator
            {
                D1 = 1,
                D2 = 3.3,
                Eps = 2
            };

            const double expected_impedance = 50.62;

            calculator.Calculate();

            var actual_impedance = calculator.Impedance;

            const double eps = 1e-2;
            Assert.That.Value(actual_impedance).IsEqual(expected_impedance, eps);
        }
    }
}
