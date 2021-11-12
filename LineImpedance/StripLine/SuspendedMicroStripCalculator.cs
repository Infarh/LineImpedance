namespace LineImpedance.StripLine
{
    /// <summary>Полосковая линия с воздушной прослойкой</summary>
    [Description("Полосковая линия с воздушной прослойкой")]
    public class SuspendedMicroStripCalculator : Calculator
    {
        /// <summary>Высота подложки</summary>
        [Description("Высота подложки")]
        public double Height { get; set; }

        /// <summary>Высота воздушной прослойки</summary>
        [Description("Высота воздушной прослойки")]
        public double Height0 { get; set; }

        /// <summary>Ширина полоска</summary>
        [Description("Ширина полоска")]
        public double Width { get; set; }

        /// <summary>Полосок между экраном и подложкой</summary>
        [Description("Полосок между экраном и подложкой")]
        public bool Inverted { get; set; }

        public override void Calculate() => Impedance = Inverted
            ? LineImpedance.Impedance.SuspendedMicroStripInverted(Height, Height0, Width, Eps)
            : LineImpedance.Impedance.SuspendedMicroStrip(Height, Height0, Width, Eps);
    }
}
