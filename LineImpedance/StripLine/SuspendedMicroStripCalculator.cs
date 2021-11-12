namespace LineImpedance.StripLine
{
    /// <summary>Полосковая линия с воздушной прослойкой</summary>
    public class SuspendedMicroStripCalculator : Calculator
    {
        /// <summary>Высота подложки</summary>
        public double Height { get; set; }

        /// <summary>Высота воздушной прослойки</summary>
        public double Height0 { get; set; }

        /// <summary>Ширина полоска</summary>
        public double Width { get; set; }

        /// <summary>Полосок между экраном и подложкой</summary>
        public bool Inverted { get; set; }

        public override void Calculate() => Impedance = Inverted
            ? LineImpedance.Impedance.SuspendedMicroStripInverted(Height, Height0, Width, Eps)
            : LineImpedance.Impedance.SuspendedMicroStrip(Height, Height0, Width, Eps);
    }
}
