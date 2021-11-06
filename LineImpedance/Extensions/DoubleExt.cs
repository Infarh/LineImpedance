using System;

namespace LineImpedance.Extensions
{
    internal static class DoubleExt
    {
        public static double Sqr(this double x) => x * x;

        public static double Sqrt(this double x) => x is double.NaN ? double.NaN : Math.Sqrt(x);

        public static double Pow(this double x, int n)
        {
            while (true)
                switch (n)
                {
                    case 0: return 1;

                    case 1: return x;
                    case 2: return x * x;
                    case 3: return x * x * x;
                    case 4: return x * x * x * x;

                    case -1: return 1 / x;
                    case -2: return 1 / (x * x);
                    case -3: return 1 / (x * x * x);
                    case -4: return 1 / (x * x * x * x);

                    case < 0:
                        x = 1 / x;
                        n = -n;
                        break;

                    default:
                        var result = x;
                        for (var i = 2; i <= n; i++)
                            result *= x;
                        return result;
                }
        }

        public static double Pow2(this double x) => x * x;

        public static double Pow(this double x, double k) =>
            x is double.NaN || k is double.NaN
                ? double.NaN
                : k switch
                {
                    0 => 1,
                    1 => x,
                    -1 => 1 / x,
                    { } p when (int)p == k => x.Pow((int)p),
                    _ => Math.Pow(x, k)
                };
    }
}
