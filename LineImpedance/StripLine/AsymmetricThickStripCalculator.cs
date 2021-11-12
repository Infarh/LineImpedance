namespace LineImpedance.StripLine
{
    /// <summary>Асимметричная экранированная полосковая линия</summary>
    public class AsymmetricThickStripCalculator : Calculator
    {
        /// <summary>Высота над полоском</summary>
        public double Height1 { get; set; }

        /// <summary>Высота под полоском</summary>
        public double Height2 { get; set; }

        /// <summary>Ширина полоска</summary>
        public double Width { get; set; }

        /// <summary>Толщина полоска</summary>
        public double Thickness { get; set; }

        public override void Calculate() => Impedance = LineImpedance.Impedance.AsymmetricThickStrip(Height1, Height2, Width, Thickness, Eps);
    }
}
