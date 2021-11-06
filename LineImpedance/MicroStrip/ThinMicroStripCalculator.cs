using LineImpedance.Extensions;
using LineImpedance.StripLine;
using static System.Math;

namespace LineImpedance.MicroStrip
{
    public class ThinMicroStripCalculator : ThickStripLineCalculator
    {
        public new static double Calculate(double H, double W, double T, double Eps)
        {
            if (T is 0) T = 4e-5; 
            // Collin, Foundation of Microwave Engineering, pp. 150
            //var c_corr = (Eps - 1) / 4.6 * (T / H) / Sqrt(W / H);

            // Pozar, Microwave Engineering, 2nd Edition. pp. 162
            var f_corr = W < H 
                ? 1 / Sqrt(1 + 12 * H / W) + 0.04 * (1 - W / H).Pow2() 
                : 1 / Sqrt(1 + 12 * H / W);

            var eps_eff = (Eps + 1) / 2 + (Eps - 1) / 2 * f_corr;

            // Collin, Foundation of Microwave Engineering, pp. 150
            var w_eff = W < 1 / (2 * PI) * H 
                ? W + 0.398 * T * (1 + Log(4 * PI * W / T)) 
                : W + 0.398 * T * (1 + Log(2 * H / T));

            // Pozar, Microwave Engineering, 2nd Edition. pp. 162
            var Z0 = W < H
                ? 60 / Sqrt(eps_eff) * Log(8 * H / w_eff + w_eff / (4 * H))
                : 120 * PI / (Sqrt(eps_eff) * (w_eff / H + 1.393 + 0.667 * Log(w_eff / H + 1.444)));

            return Z0;
        }

        public override void Calculate() => Impedance = Calculate(Height, Width, Thickness, Eps);
    }
}
