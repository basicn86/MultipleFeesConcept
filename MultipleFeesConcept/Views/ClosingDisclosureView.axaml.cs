using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;

namespace MultipleFeesConcept.Views;

public partial class ClosingDisclosureView : ReactiveWindow<ViewModels.ClosingDisclosureViewModel>
{
    public ClosingDisclosureView()
    {
        InitializeComponent();
    }
}