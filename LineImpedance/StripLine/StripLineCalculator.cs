using static System.Math;

namespace LineImpedance.StripLine
{
    /// <summary>Симметричная полосковая линия</summary>
    public class StripLineCalculator : Calculator
    {
        public static double Calculate(double H, double W, double Eps)
        {
            // Formula: Pozar, Microwave Engineering, 2nd Edition. pp. 156

            //const double threshold = 35d / 100;
            //var correction = W / H < threshold ? (threshold - W / H).Pow2() : 0;
            //var we = H * (W / H - correction);
            //return 30 * PI / Sqrt(Eps) * (H / we + 0.441 * H);

            var we = PI * W / (2 * H);
            var k = Sech(we);
            var k_prime = Tanh(we);
            return 30 * PI / Sqrt(Eps) * Kint(k) / Kint(k_prime);
        }

        public double Height { get; set; }

        public double Width { get; set; }

        public override void Calculate() => Impedance = Calculate(Height, Width, Eps);
    }
}
