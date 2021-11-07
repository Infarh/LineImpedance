using System;

namespace LineImpedance.Coaxial
{
    public class CoaxialCalculator : Calculator
    {
        public double D1 { get; set; }
        public double D2 { get; set; }

        public override void Calculate() => Impedance = LineImpedance.Impedance.Coaxial(D1, D2, Eps);
    }
}
