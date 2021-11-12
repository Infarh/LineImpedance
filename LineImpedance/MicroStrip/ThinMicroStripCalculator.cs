using LineImpedance.StripLine;

namespace LineImpedance.MicroStrip
{
    /// <summary>Несимметричная полосковая линия</summary>
    [Description("Несимметричная полосковая линия")]
    public class ThinMicroStripCalculator : ThickStripCalculator
    {
        public override void Calculate() => Impedance = LineImpedance.Impedance.ThinMicroStrip(Height, Width, Thickness, Eps);
    }
}
