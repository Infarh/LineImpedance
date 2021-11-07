using System;
using LineImpedance.StripLine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LineImpedance.Tests.StripLine
{
    [TestClass]
    public class ThickStripLineCalculatorTests
    {
        [TestMethod]
        public void Calculate_W_2_4_H_3_T_0_1_Eps_2_Impedance_50_26()
        {
            const double h = 3;
            const double w = 2.4;
            const double t = 0.1;
            const double eps = 2;

            const double expected_impedance = 50.26;

            var calculator = new ThickStripCalculator
            {
                Height = h,
                Width = w,
                Thickness = t,
                Eps = eps
            };

            calculator.Calculate();

            var actual_impedance = calculator.Impedance;

            const double error = 4.16e-003;
            Assert.That.Value(actual_impedance).IsEqual(expected_impedance, error);
        }

        [TestMethod]
        public void Calculate_W_1_H_3_T_0_1_Eps_2_Impedance_78_86()
        {
            const double h = 3;
            const double w = 1;
            const double t = 0.1;
            const double eps = 2;

            const double expected_impedance = 78.86;

            var calculator = new ThickStripCalculator
            {
                Height = h,
                Width = w,
                Thickness = t,
                Eps = eps
            };

            calculator.Calculate();

            var actual_impedance = calculator.Impedance;

            const double error = 4.16e-003;
            Assert.That.Value(actual_impedance).IsEqual(expected_impedance, error);
        }

        [TestMethod]
        public void T_less_0_25_H_is_Invalid_parameters()
        {
            const double h = 1;
            const double w = 1;
            const double t = 0.5;
            const double eps = 2;

            var calculator = new ThickStripCalculator
            {
                Height = h,
                Width = w,
                Thickness = t,
                Eps = eps
            };

            var exception = Assert.ThrowsException<InvalidOperationException>(calculator.Calculate);

            Assert.That.Value(exception.Data["H"]).IsEqual(h);
            Assert.That.Value(exception.Data["W"]).IsEqual(h);
            Assert.That.Value(exception.Data["T"]).IsEqual(t);
            Assert.That.Value(exception.Data["Eps"]).IsEqual(eps);
        }
    }
}
