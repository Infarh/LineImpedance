namespace LineImpedance.StripLine
{
    public class ThickCoplanarCalculator : Calculator
    {
        public double Height { get; set; }

        public double Width { get; set; }

        public double Thickness { get; set; }

        public double Gap { get; set; }

        public override void Calculate() => Impedance = LineImpedance.Impedance.ThickCoplanar(Height, Width, Thickness, Gap, Eps);
    }
}
