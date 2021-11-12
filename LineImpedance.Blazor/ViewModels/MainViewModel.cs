using LineImpedance.Blazor.ViewModels.Base;
using LineImpedance.Extensions;

namespace LineImpedance.Blazor.ViewModels;

public class MainViewModel : ViewModel
{
    private readonly Dictionary<string, CalculatorViewModel> _Calculators;

    public IEnumerable<CalculatorViewModel> Calculators => _Calculators.Values;

    public MainViewModel()
    {
        var calculator_type = typeof(Calculator);
        _Calculators = calculator_type.Assembly.ExportedTypes
           .Where(t => !t.IsAbstract && t.IsAssignableTo(calculator_type))
           .Select(t => (Type: t, Description: t.GetDescription()))
           .Where(t => t.Description is { })
           .Select(t => new CalculatorViewModel(t.Type, t.Description!))
           .ToDictionary(c => c.TypeName);
    }

    public CalculatorViewModel? GetCalculatorByTypeName(string CalculatorTypeName) => 
        _Calculators.GetValueOrDefault(CalculatorTypeName);
}
