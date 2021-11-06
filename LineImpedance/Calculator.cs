using static System.Math;

namespace LineImpedance
{
    /// <summary>Вычислитель</summary>
    public abstract class Calculator
    {
        protected static double Sech(double x) => 2 / (Exp(x) + Exp(-x));

        /// <summary>Диэлектрическая проницаемость диэлектрика</summary>
        public double Eps { get; set; }

        /// <summary>Сопротивление линии</summary>
        public double Impedance { get; set; }

        /// <summary>Рассчитать</summary>
        public abstract void Calculate();
    }
}
