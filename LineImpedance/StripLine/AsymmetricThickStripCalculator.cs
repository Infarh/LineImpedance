namespace LineImpedance.StripLine
{
    public class AsymmetricThickStripCalculator : Calculator
    {
        public double Height1 { get; set; }
        public double Height2 { get; set; }
        public double Width { get; set; }
        public double Thickness { get; set; }

        public override void Calculate() => Impedance = LineImpedance.Impedance.AsymmetricThickStrip(Height1, Height2, Width, Thickness, Eps);
    }
}
