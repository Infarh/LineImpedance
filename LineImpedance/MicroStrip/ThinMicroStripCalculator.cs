using LineImpedance.StripLine;

namespace LineImpedance.MicroStrip
{
    public class ThinMicroStripCalculator : ThickStripCalculator
    {
        public override void Calculate() => Impedance = LineImpedance.Impedance.ThinMicroStrip(Height, Width, Thickness, Eps);
    }
}
