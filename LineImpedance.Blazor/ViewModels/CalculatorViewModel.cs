using System.Reflection;
using LineImpedance.Blazor.ViewModels.Base;
using LineImpedance.Extensions;

namespace LineImpedance.Blazor.ViewModels;

public class CalculatorViewModel : ViewModel
{
    private readonly Type _CalculatorType;
    private readonly string _Name;
    private readonly Calculator _Calculator;
    private readonly CalculatorPropertyViewModel[] _Properties;

    public string Name => _Name;

    public string TypeName => _CalculatorType.Name.Replace("Calculator", "");

    public IEnumerable<CalculatorPropertyViewModel> Properties => _Properties;

    public CalculatorViewModel(Type CalculatorType, string Name)
    {
        if (!CalculatorType.IsAssignableTo(typeof(Calculator)))
            throw new ArgumentException("Указанный тип не является вычислителем сопротивления", nameof(CalculatorType));

        _CalculatorType = CalculatorType;
        _Name = Name;
        _Calculator = Create();
        _Properties = GetProperties(_Calculator).ToArray();
    }

    public Calculator Create() => (Calculator)Activator.CreateInstance(_CalculatorType)!
        ?? throw new InvalidOperationException("Не удалось создать вычислитель");

    public static IEnumerable<CalculatorPropertyViewModel> GetProperties(Calculator calculator)
    {
        var type = calculator.GetType();
        foreach (var property_info in type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty | BindingFlags.GetProperty))
        {
            if (property_info.GetDescription() is not { Length: > 0 } description)
                continue;

            if(!property_info.PropertyType.IsAssignableTo(typeof(double)))
                continue;

            yield return new CalculatorPropertyViewModel(property_info, calculator, description);
        }
    }

    public void Calculate()
    {
        _Calculator.Calculate();
    }
}
