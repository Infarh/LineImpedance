namespace LineImpedance.StripLine
{
    /// <summary>Симметричная полосковая линия</summary>
    public class StripCalculator : Calculator
    {
        public double Height { get; set; }

        public double Width { get; set; }

        public override void Calculate() => Impedance = LineImpedance.Impedance.StripLine(Height, Width, Eps);
    }
}
