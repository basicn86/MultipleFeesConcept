using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using MultipleFeesConcept.Models;
using MultipleFeesConcept.ViewModels;
using ReactiveUI;
using System.Threading.Tasks;

namespace MultipleFeesConcept.Views;

public partial class FeesView : ReactiveWindow<ViewModels.FeesViewModel>
{
    public FeesView()
    {
        InitializeComponent();

        //if design mode, return
        if (Design.IsDesignMode) return;

        //notify viewmodel of closing
        Closing += async (s, e) =>
        {
            await ViewModel!.Closing();
        };

        //register the DoShowDialogAsync method
        this.WhenActivated(action => action(ViewModel!.ShowDialog.RegisterHandler(DoShowDialogAsync)));
    }

    private async Task DoShowDialogAsync(InteractionContext<AddFeeViewModel, FeeType?> interactionContext)
    {
        var dialog = new AddFeeView();
        dialog.DataContext = interactionContext.Input;
        var result = await dialog.ShowDialog<FeeType?>(this);
        interactionContext.SetOutput(result);
    }
}