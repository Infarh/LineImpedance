using System;

using LineImpedance.Extensions;

using static System.Math;

namespace LineImpedance.StripLine
{
    public class ThickStripCalculator : StripCalculator
    {
        public double Thickness { get; set; }

        public override void Calculate()
        {
            if (Thickness is 0)
                base.Calculate();
            else
                Impedance = LineImpedance.Impedance.ThickStrip(Height, Width, Thickness, Eps);
        }
    }
}
