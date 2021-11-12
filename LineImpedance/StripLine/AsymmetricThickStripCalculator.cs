namespace LineImpedance.StripLine
{
    /// <summary>Асимметричная экранированная полосковая линия</summary>
    [Description("Асимметричная экранированная полосковая линия")]
    public class AsymmetricThickStripCalculator : Calculator
    {
        /// <summary>Высота над полоском</summary>
        [Description("Высота над полоском")]
        public double Height1 { get; set; }

        /// <summary>Высота под полоском</summary>
        [Description("Высота под полоском")]
        public double Height2 { get; set; }

        /// <summary>Ширина полоска</summary>
        [Description("Ширина полоска")]
        public double Width { get; set; }

        /// <summary>Толщина полоска</summary>
        [Description("Толщина полоска")]
        public double Thickness { get; set; }

        public override void Calculate() => Impedance = LineImpedance.Impedance.AsymmetricThickStrip(Height1, Height2, Width, Thickness, Eps);
    }
}
