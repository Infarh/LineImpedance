using static System.Math;

namespace LineImpedance
{
    /// <summary>Вычислитель</summary>
    public abstract class Calculator
    {
        protected static double Sech(double x) => 2 / (Exp(x) + Exp(-x));

        /// <summary>Комплементарный эллиптический интеграл</summary>
        protected static double Kint(double k)
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

        /// <summary>Диэлектрическая проницаемость диэлектрика</summary>
        public double Eps { get; set; }

        /// <summary>Сопротивление линии</summary>
        public double Impedance { get; set; }

        /// <summary>Рассчитать</summary>
        public abstract void Calculate();
    }
}
