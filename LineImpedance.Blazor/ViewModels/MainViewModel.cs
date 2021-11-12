using LineImpedance.Blazor.ViewModels.Base;

namespace LineImpedance.Blazor.ViewModels;

public class MainViewModel : ViewModel
{
    private readonly CalculatorViewModel[] _Calculators;

    private readonly Task _UpdateTimeTask;
    private readonly CancellationTokenSource _UpdateTaskCancellation;

    public IEnumerable<string> CalculatorNames => _Calculators.Select(c => c.CalculatorName);

    public DateTime Time
    {
        get => DateTime.Now;
        set
        {

        }
    }

    public MainViewModel()
    {
        var calculator_type = typeof(Calculator);
        _Calculators = calculator_type.Assembly.ExportedTypes
           .Where(t => !t.IsAbstract && t.IsAssignableTo(calculator_type))
           .Select(t => new CalculatorViewModel(t))
           .ToArray();

        var cancellation = new CancellationTokenSource();
        _UpdateTaskCancellation = cancellation;
        _UpdateTimeTask = UpdateTime(cancellation.Token);
    }

    private async Task UpdateTime(CancellationToken Cancel)
    {
        while (!Cancel.IsCancellationRequested)
        {
            await Task.Delay(100, Cancel);
            OnPropertyChanged(nameof(Time));
        }
        Cancel.ThrowIfCancellationRequested();
    }
}
