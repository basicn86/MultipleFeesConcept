using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using MultipleFeesConcept.Models;
using MultipleFeesConcept.ViewModels;
using ReactiveUI;
using System.Reactive;
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
        this.WhenActivated(action => action(ViewModel!.ShowAddFeeDialog.RegisterHandler(DoShowAddFeeDialogAsync)));
        this.WhenActivated(action => action(ViewModel!.ShowChangeTrackerDialog.RegisterHandler(DoShowChangeTrackerDialogAsync)));
        this.WhenActivated(action => action(ViewModel!.ShowClosingDisclosureDialog.RegisterHandler(DoShowClosingDisclosureDialogAsync)));
    }

    private async Task DoShowAddFeeDialogAsync(InteractionContext<AddFeeViewModel, FeeType?> interactionContext)
    {
        var dialog = new AddFeeView();
        dialog.DataContext = interactionContext.Input;
        var result = await dialog.ShowDialog<FeeType?>(this);
        interactionContext.SetOutput(result);
    }

    private async Task DoShowChangeTrackerDialogAsync(InteractionContext<ChangeTrackerViewModel, Unit?> interactionContext)
    {
        var dialog = new ChangeTrackerView();
        dialog.DataContext = interactionContext.Input;

        var result = await dialog.ShowDialog<Unit?>(this);
        interactionContext.SetOutput(result);
    }

    private async Task DoShowClosingDisclosureDialogAsync(InteractionContext<ClosingDisclosureViewModel, Unit?> interactionContext)
    {
        var dialog = new ClosingDisclosureView();
        dialog.DataContext = interactionContext.Input;

        var result = await dialog.ShowDialog<Unit?>(this);
        interactionContext.SetOutput(result);
    }
}