namespace LineImpedance.StripLine
{
    public class SuspendedMicroStripCalculator : Calculator
    {
        public double Height { get; set; }
        public double Height0 { get; set; }
        public double Width { get; set; }

        public bool Inverted { get; set; }

        public override void Calculate() => Impedance = Inverted
            ? LineImpedance.Impedance.SuspendedMicroStripInverted(Height, Height0, Width, Eps)
            : LineImpedance.Impedance.SuspendedMicroStrip(Height, Height0, Width, Eps);
    }
}
