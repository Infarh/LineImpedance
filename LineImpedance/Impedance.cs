using System;
using System.Runtime.InteropServices;

using LineImpedance.Extensions;

using static System.Math;

namespace LineImpedance
{
    /// <summary>Сопротивление линии</summary>
    public static class Impedance
    {
        private static double Sech(double x) => 2 / (Exp(x) + Exp(-x));

        private static double Coth(double x)
        {
            var e = Exp(x);
            var inv_e = 1 / e;
            return (e + inv_e) / (e - inv_e);
        }

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

        /// <summary>Сопротивление ассиметричной экранированной полосковой линии</summary>
        /// <param name="H1">Высота над полоском</param>
        /// <param name="H2">Высота под полоском</param>
        /// <param name="W">Ширина полоска</param>
        /// <param name="T">Толщина полоска</param>
        /// <param name="Eps">Диэлектрическая проницаемость подложки</param>
        public static double AsymmetricThickStrip(double H1, double H2, double W, double T, double Eps)
        {
            if (W / H1 < 0.1 || W / H1 > 2)
                throw new InvalidOperationException("0.1 <= W/H1 <= 2")
                {
                    Data =
                    {
                        { "H1", H1 },
                        { "H2", H2 },
                        { "W", W },
                        { "T", T },
                        { "Eps", Eps }
                    }
                };

            if (T / H1 >= 0.25)
                throw new InvalidOperationException("T / H1 >= 0.25")
                {
                    Data =
                    {
                        { "H1", H1 },
                        { "H2", H2 },
                        { "W", W },
                        { "T", T },
                        { "Eps", Eps }
                    }
                };

            var imp1 = ThickStrip(2 * H1 + T, W, T, Eps);
            var imp2 = ThickStrip(2 * H2 + T, W, T, Eps);

            return 2 * imp1 * imp2 / (imp1 + imp2);
        }

        private const double imp0 = 377;

        public static double DifferentialStrip(double H, double W, double T, double D, double Eps)
        {
            var tw = T / H;

            var aaa = 1 / (1 - tw);

            const double c0885pi = 0.0885 / PI;
            var c0885_pi_eps = c0885pi * Eps;
            var cf_T = c0885_pi_eps * (2 * aaa * Log(aaa + 1) - (aaa - 1) * Log(aaa.Pow2() - 1));
            const double log2 = 0.69314718055994529;
            var cf = c0885_pi_eps * 2 * log2;

            //var m = 6 / (3 + 2 * T / (H - T));
            //var dw1 = 1 / (2 * (H - T) / T + 1);
            //var dw2 = 1 / (4 * Pi) / (W / T + 1.1);
            //var Dw3 = dw1.Pow2() + dw2.Pow(m);
            //var dw = T / Pi * (1 - 0.5 * Log(Dw3));
            //var wp = W + dw;
            //var z01 = 30 / Sqrt(Eps);
            //var z02 = 4 * (H - T) / (Pi * wp);
            //var z03 = z02 * 2;
            //var z04 = Sqrt(z03.Pow2() + 6.27);
            //var Zof = z01 * Log(1 + z02 * (z03 + z04));

            var Zof = ThickStrip(H, W, T, Eps); // Thick isolated stripline
            var Zoo = StripLine(H, W, Eps);     // Thin isolated Stripline

            // calculate zero-thickness odd impedance of edge-coupled stripline
            var ko = Tanh(PI * W / (2 * H)) * Coth(PI * (W + D) / (2 * H));
            var ko_prime = Sqrt(1 - ko.Pow2());
            var Zo = 30 * PI / Sqrt(Eps) * Kint(ko_prime) / Kint(ko); // Im missing a factor of To somewere. Just added it here

            // calculate zero-thickness even impedance of edge-coupled stripline
            //var ke = Tanh(Pi * W / (2 * H)) * Tanh(Pi * (W + D) / (2 * H));
            //var ke_prime = Sqr(1 - ke.Pow2());
            //var Ze = 30 * Pi / Sqr(Eps) * Kint(ko_prime) / Kint(ko);

            if (D / T > 5)
            {
                var a1 = 1 / Zof;
                var a2 = cf_T / cf;
                //var cst_a3_even = 1 / Zoo;
                var a3_odd = 1 / Zo;
                //var a4_even = 1 / Ze;
                var a4_odd = 1 / Zoo;

                return 2 / (a1 + a2 * (a3_odd - a4_odd));
            }

            var p1 = 1 / Zo;
            var p2 = 1 / Zof - 1 / Zoo;
            var p3 = 2d / imp0 * (cf_T / Eps - cf / Eps);
            return 2 / (p1 + p2 - p3 + 2 * T / (imp0 * D));
        }

        public static double SuspendedMicroStrip(double H, double H0, double W, double Eps)
        {
            const double en = 2.71829;

            var f = Log(Eps);

            var f_2 = f * f;
            var f_3 = f_2 * f;
            var d00 = (176.2576 - 43.1240 * f + 13.4094 * f_2 - 1.7010 * f_3) * 1e-2;
            var d01 = (4665.2320 - 1790.4 * f + 291.5858 * f_2 - 8.0888 * f_3) * 1e-4;
            var d02 = (-3025.5070 - 141.9368 * f - 3099.47 * f_2 + 777.6151 * f_3) * 1e-6;
            var d03 = (2491.569 + 143.3860 * f + 10095.55 * f_2 - 2599.132 * f_3) * 1e-8;
            var d10 = (-1410.2050 + 149.9293 * f + 198.2892 * f_2 - 32.1679 * f_3) * 1e-4;
            var d11 = (2548.791 + 1531.9310 * f - 1027.5200 * f_2 + 138.4192 * f_3) * 1e-4;
            var d12 = (999.3135 - 4036.7910 * f + 1762.4120 * f_2 - 298.0241 * f_3) * 1e-6;
            var d13 = (-1983.7890 + 8523.9290 * f - 5235.4600 * f_2 + 1145.7880 * f_3) * 1e-8;
            var d20 = (1954.072 + 333.3873 * f - 700.7473 * f_2 + 121.3212 * f_3) * 1e-5;
            var d21 = (-3931.09 - 1890.719 * f + 1912.266 * f_2 - 319.6794 * f_3) * 1e-5;
            var d22 = (-532.1326 + 7274.7210 * f - 4955.738 * f_2 + 941.4134 * f_3) * 1e-7;
            var d23 = (138.2037 - 1412.427 * f + 1184.27 * f_2 - 270.0047 * f_3) * 1e-8;
            var d30 = (-983.4028 - 255.1229 * f + 455.8729 * f_2 - 83.9468 * f_3) * 1e-6;
            var d31 = (1956.3170 + 779.9975 * f - 995.9494 * f_2 + 183.1957 * f_3) * 1e-6;
            var d32 = (62.855 - 3462.5 * f + 2909.923 * f_2 - 614.7068 * f_3) * 1e-8;
            var d33 = (-35.2531 + 601.0291 * f - 643.0814 * f_2 + 161.2689 * f_3) * 1e-9;
            var f1 = 1 - Sqrt(1 / Eps);
            var h0h = H0 / H;
            var h0h2 = h0h * h0h;
            var h0h3 = h0h2 * h0h;
            var c0 = d00 + d01 * h0h + d02 * h0h2 + d03 * h0h3;
            var c1 = d10 + d11 * h0h + d12 * h0h2 + d13 * h0h3;
            var c2 = d20 + d21 * h0h + d22 * h0h2 + d23 * h0h3;
            var c3 = d30 + d31 * h0h + d32 * h0h2 + d33 * h0h3;
            var wh0 = W / H0;
            var wh02 = wh0 * wh0;
            var wh03 = wh02 * wh0;
            var f2 = 1 / (c0 + c1 * wh0 + c2 * wh02 + c3 * wh03);
            var sqrt_eps = 1 / (1 - f1 * f2);
            var u = wh0 / (1 + H / H0);
            var fu = 6 + (2 * PI - 6) * en.Pow(-(30.666 / u).Pow(0.7528));
            var Z0 = 60 * Log(fu / u + Sqrt(1 + 4 / u.Pow2()));
            //var eps_eff = sqrt_eps.Pow2();
            return Z0 / sqrt_eps;
        }
    }
}
