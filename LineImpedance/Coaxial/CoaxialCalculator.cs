namespace LineImpedance.Coaxial
{
    /// <summary>Коаксиальная линия</summary>
    [Description("Коаксиальная линия")]
    public class CoaxialCalculator : Calculator
    {
        /// <summary>Внутренний диаметр</summary>
        [Description("Внутренний диаметр")]
        public double D1 { get; set; } = 1;

        /// <summary>Внешний диаметр</summary>
        [Description("Внешний диаметр")]
        public double D2 { get; set; } = 3.3;

        public CoaxialCalculator()
        {
            Eps = 2;
            Impedance = 50.62;
        }

        public override void Calculate() => Impedance = LineImpedance.Impedance.Coaxial(D1, D2, Eps);
    }
}
