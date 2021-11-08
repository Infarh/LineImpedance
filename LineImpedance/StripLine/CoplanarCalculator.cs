namespace LineImpedance.StripLine
{
    public class CoplanarCalculator : Calculator
    {
        public double Height { get; set; }

        public double Width { get; set; }

        public double Thickness { get; set; }

        public double Gap { get; set; }

        public bool Ground { get; set; }

        public override void Calculate() => Impedance = Ground
            ? LineImpedance.Impedance.CoplanarWithGround(Height, Width, Gap, Eps)
            : LineImpedance.Impedance.ThickCoplanar(Height, Width, Thickness, Gap, Eps);
    }
}
