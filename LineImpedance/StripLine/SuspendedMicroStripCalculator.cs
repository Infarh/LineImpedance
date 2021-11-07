namespace LineImpedance.StripLine
{
    public class SuspendedMicroStripCalculator : Calculator
    {
        public double Height { get; set; }
        public double Height0 { get; set; }
        public double Width { get; set; }

        public override void Calculate() => Impedance = LineImpedance.Impedance.SuspendedMicroStrip(Height, Height0, Width, Eps);
    }
}
