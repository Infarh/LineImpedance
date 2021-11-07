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
    public class AsymmetricThickStripCalculatorTests
    {
        [TestMethod]
        public void Calculate()
        {
            const double h1 = 1;
            const double h2 = 4;
            const double w = 2;
            const double t = 0.1;
            const double eps = 2;

            const double expected_impedance = 60.01;

            var calculator = new AsymmetricThickStripCalculator
            {
                Height1 = h1,
                Height2 = h2,
                Width = w,
                Thickness = t,
                Eps = eps,
            };

            calculator.Calculate();

            var actual_impedance = calculator.Impedance;

            const double error = 1e-2;
            Assert.That.Value(actual_impedance).IsEqual(expected_impedance, error);
        }

        [TestMethod] 
        public void Claculate_WtoH1_less_0_1()
        {
            const double h1 = 22;
            const double h2 = 4;
            const double w = 2;
            const double t = 0.1;
            const double eps = 2;

            var calculator = new AsymmetricThickStripCalculator
            {
                Height1 = h1,
                Height2 = h2,
                Width = w,
                Thickness = t,
                Eps = eps,
            };

            var exception = Assert.ThrowsException<InvalidOperationException>(calculator.Calculate);

            Assert.That.Value(exception.Message).IsEqual("0.1 <= W/H1 <= 2");
        }

        [TestMethod] 
        public void Claculate_WtoH1_greater_2()
        {
            const double h1 = 2;
            const double h2 = 4;
            const double w = 5;
            const double t = 0.1;
            const double eps = 2;

            var calculator = new AsymmetricThickStripCalculator
            {
                Height1 = h1,
                Height2 = h2,
                Width = w,
                Thickness = t,
                Eps = eps,
            };

            var exception = Assert.ThrowsException<InvalidOperationException>(calculator.Calculate);

            Assert.That.Value(exception.Message).IsEqual("0.1 <= W/H1 <= 2");
        }

        [TestMethod]
        public void Claculate_TtoH1_greater_2_5()
        {
            const double h1 = 1;
            const double h2 = 4;
            const double w = 2;
            const double t = 3;
            const double eps = 2;

            var calculator = new AsymmetricThickStripCalculator
            {
                Height1 = h1,
                Height2 = h2,
                Width = w,
                Thickness = t,
                Eps = eps,
            };

            var exception = Assert.ThrowsException<InvalidOperationException>(calculator.Calculate);

            Assert.That.Value(exception.Message).IsEqual("T / H1 >= 0.25");
        }
    }
}
