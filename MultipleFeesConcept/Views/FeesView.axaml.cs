using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;

namespace MultipleFeesConcept.Views;

public partial class FeesView : ReactiveWindow<ViewModels.FeesViewModel>
{
    public FeesView()
    {
        InitializeComponent();
    }
}