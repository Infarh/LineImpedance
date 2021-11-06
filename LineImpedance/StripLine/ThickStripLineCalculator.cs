using System;

using LineImpedance.Extensions;

using static System.Math;

namespace LineImpedance.StripLine
{
    public class ThickStripLineCalculator : StripLineCalculator
    {
        public static double Calculate(double H, double W, double T, double Eps)
        {
            const double h_t_threshold = 0.25;
            var th = T / H;
            if (th > h_t_threshold)
                throw new InvalidOperationException($"Отношение t/h = {T}/{H} > {h_t_threshold} не поддерживается")
                {
                    Data =
                    {
                        { nameof(H), H },
                        { nameof(T), T },
                        { nameof(W), W },
                        { nameof(Eps), Eps }
                    }
                };

            const double w_h_t_threshold = 35d / 100;
            if (W / (H - T) < w_h_t_threshold)
            {
                var piw = PI * W;
                var t_piw = T / piw;
                const double log_0_25_with_1 = 2.3862943611198906;
                return
                    60 / Sqrt(Eps) *
                    Log(8 * H / piw / (1 + t_piw * (log_0_25_with_1 - Log(t_piw)) + 0.51 * (T / W).Pow2()));
            }

            var th_inv = 1 / (1 - th);
            var p1 = 2 * th_inv * Log(th_inv + 1);
            var p2 = (th_inv - 1) * Log(th_inv / (1 - th) - 1);
            var eps0885 = 0.0885 * Eps;
            var cfprime = eps0885 / PI * (p1 - p2);
            return 94.15 / (Sqrt(Eps) * (W / H * th_inv + cfprime / eps0885));

        }

        public double Thickness { get; set; }

        public override void Calculate()
        {
            if (Thickness is 0)
                base.Calculate();
            else
                Impedance = Calculate(Height, Width, Thickness, Eps);
        }
    }
}
