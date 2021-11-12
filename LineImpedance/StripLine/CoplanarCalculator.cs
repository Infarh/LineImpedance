namespace LineImpedance.StripLine
{
    /// <summary>Щелевая полосковая линия</summary>
    public class CoplanarCalculator : Calculator
    {
        /// <summary>Высота подложки</summary>
        public double Height { get; set; }

        /// <summary>Ширина полоска</summary>
        public double Width { get; set; }

        /// <summary>Толщина полоска</summary>
        public double Thickness { get; set; }

        /// <summary>Зазор между полосками</summary>
        public double Gap { get; set; }

        /// <summary>Заземление</summary>
        public bool Ground { get; set; }

        public override void Calculate() => Impedance = Ground
            ? LineImpedance.Impedance.CoplanarWithGround(Height, Width, Gap, Eps)
            : LineImpedance.Impedance.ThickCoplanar(Height, Width, Thickness, Gap, Eps);
    }
}
