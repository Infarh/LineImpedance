namespace LineImpedance.StripLine
{
    /// <summary>Симметричная полосковая линия с толстым полоском</summary>
    [Description("Симметричная полосковая линия с толстым полоском")]
    public class ThickStripCalculator : StripCalculator
    {
        /// <summary>Толщина полоска</summary>
        [Description("Толщина полоска")]
        public double Thickness { get; set; }

        public override void Calculate()
        {
            if (Thickness is 0)
                base.Calculate();
            else
                Impedance = LineImpedance.Impedance.ThickStrip(Height, Width, Thickness, Eps);
        }
    }
}
