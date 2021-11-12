namespace LineImpedance.StripLine
{
    /// <summary>Щелевая полосковая линия</summary>
    [Description("Щелевая полосковая линия")]
    public class CoplanarCalculator : Calculator
    {
        /// <summary>Высота подложки</summary>
        [Description("Высота подложки")]
        public double Height { get; set; }

        /// <summary>Ширина полоска</summary>
        [Description("Ширина полоска")]
        public double Width { get; set; }

        /// <summary>Толщина полоска</summary>
        [Description("Толщина полоска")]
        public double Thickness { get; set; }

        /// <summary>Зазор между полосками</summary>
        [Description("Зазор между полосками")]
        public double Gap { get; set; }

        /// <summary>Заземление</summary>
        [Description("Заземление")]
        public bool Ground { get; set; }

        public override void Calculate() => Impedance = Ground
            ? LineImpedance.Impedance.CoplanarWithGround(Height, Width, Gap, Eps)
            : LineImpedance.Impedance.ThickCoplanar(Height, Width, Thickness, Gap, Eps);
    }
}
