namespace LineImpedance.StripLine
{
    public class DifferentialStripCalculator : Calculator
    {
        public double Width { get; set; }
        public double Height { get; set; }
        public double Thickness { get; set; }
        public double Delta { get; set; }

        public override void Calculate() => Impedance = LineImpedance.Impedance.DifferentialStrip(Height, Width, Thickness, Delta, Eps);
    }
}
