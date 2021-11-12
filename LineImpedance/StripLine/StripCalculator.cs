namespace LineImpedance.StripLine
{
    /// <summary>Симметричная полосковая линия</summary>
    [Description("Симметричная полосковая линия")]
    public class StripCalculator : Calculator
    {
        /// <summary>Высота подложки</summary>
        [Description("Высота подложки")]
        public double Height { get; set; }

        /// <summary>Ширина полоска</summary>
        [Description("Ширина полоска")]
        public double Width { get; set; }

        public override void Calculate() => Impedance = LineImpedance.Impedance.StripLine(Height, Width, Eps);
    }
}
