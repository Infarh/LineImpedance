using System;

using LineImpedance.Extensions;

using static System.Math;

namespace LineImpedance
{
    public static class Impedance
    {
        private static double Sech(double x) => 2 / (Exp(x) + Exp(-x));

        /// <summary>Комплементарный эллиптический интеграл</summary>
        private static double Kint(double k)
        {
            var a = 1d;
            var b = Sqrt(1 - k * k);
            var c = k;
            while (c > 1e-12) // Stop when  c= 0
            {
                var e = a;
                var f = b;
                a = (e + f) / 2;
                b = Sqrt(e * f);
                c = (a - b) / 2;
            }

            return PI / (2 * a);
        }

        /// <summary>Сопротивление коаксиальной линии</summary>
        /// <param name="d1">Диаметр центрального проводника</param>
        /// <param name="d2">Диаметр экрана</param>
        /// <param name="Eps">Диэлектрическая проницаемость подложки</param>
        public static double Coaxial(double d1, double d2, double Eps)
        {
            const double c = Math.PI * 2;
            const double k = 376.734;
            const double k1 = k / c;
            return k1 * Log(d2 / d1) / Sqrt(Eps);
        }

        /// <summary>Сопротивление симметричной полосковой линии с толстым полоском</summary>
        /// <param name="H">Высота подложки</param>
        /// <param name="W">Ширина полоска</param>
        /// <param name="T">Толщина полоска</param>
        /// <param name="Eps">Диэлектрическая проницаемость подложки</param>
        public static double ThinMicroStrip(double H, double W, double T, double Eps)
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

        /// <summary>Сопротивление симметричной полосковой линии с тонким полоском</summary>
        /// <param name="H">Высота подложки</param>
        /// <param name="W">Ширина полоска</param>
        /// <param name="Eps">Диэлектрическая проницаемость подложки</param>
        public static double StripLine(double H, double W, double Eps)
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

        /// <summary>Сопротивление несимметричной полосковой линии</summary>
        /// <param name="H">Высота подложки</param>
        /// <param name="W">Ширина полоска</param>
        /// <param name="T">Толщина полоска</param>
        /// <param name="Eps">Диэлектрическая проницаемость подложки</param>
        public static double ThickStrip(double H, double W, double T, double Eps)
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
    }
}
