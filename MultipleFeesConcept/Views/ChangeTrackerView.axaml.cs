using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;

namespace MultipleFeesConcept.Views;

public partial class ChangeTrackerView : ReactiveWindow<ViewModels.ChangeTrackerViewModel>
{
    public ChangeTrackerView()
    {
        InitializeComponent();
    }
}