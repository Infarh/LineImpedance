using System.Reflection;
using LineImpedance.Blazor.ViewModels.Base;

namespace LineImpedance.Blazor.ViewModels;

public class CalculatorPropertyViewModel : ViewModel
{
    private readonly PropertyInfo _Property;
    private readonly Calculator _Calculator;
    private readonly string _Description;
    private readonly Func<double> _Getter;
    private readonly Action<double> _Setter;

    public string Name => _Property.Name;

    public double Value { get => _Getter(); set => _Setter(value); }

    public CalculatorPropertyViewModel(PropertyInfo Property, Calculator Calculator, string Description)
    {
        _Property = Property;
        _Calculator = Calculator;
        _Description = Description;

        _Getter = Property.GetGetMethod()!.CreateDelegate<Func<double>>(_Calculator);
        _Setter = Property.GetSetMethod()!.CreateDelegate<Action<double>>(_Calculator);
    }
}
