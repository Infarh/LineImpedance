using System;

namespace LineImpedance.Coaxial
{
    public class CoaxialCalculator : Calculator
    {
        public static double Calculate(double d1, double d2, double Eps)
        {
            const double c = Math.PI * 2;
            const double k = 376.734;
            const double k1 = k / c;
            return k1 * Math.Log(d2 / d1) / Math.Sqrt(Eps);
        }

        public double D1 { get; set; }
        public double D2 { get; set; }

        public override void Calculate() => Impedance = Calculate(D1, D2, Eps);
    }
}
