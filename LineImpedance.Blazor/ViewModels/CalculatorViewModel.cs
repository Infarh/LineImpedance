namespace LineImpedance.Blazor.ViewModels;

public class CalculatorViewModel
{
    private readonly Calculator _Calculator;

    public string CalculatorName => _Calculator.GetType().Name.Replace("Calculator", "");

    public CalculatorViewModel(Type CalculatorType)
    {
        if (!CalculatorType.IsAssignableTo(typeof(Calculator)))
            throw new ArgumentException("Указанный тип не является вычислителем сопротивления", nameof(CalculatorType));

        _Calculator = (Calculator)Activator.CreateInstance(CalculatorType)! ?? throw new InvalidOperationException("Не удалось создать вычислитель");
    }
}
