using Avalonia.Controls;
using Avalonia.ReactiveUI;
using MultipleFeesConcept.ViewModels;
using ReactiveUI;
using System.Threading.Tasks;

namespace MultipleFeesConcept.Views
{
    public partial class MainWindow : ReactiveWindow<ViewModels.MainWindowViewModel>
    {
        public MainWindow()
        {
            InitializeComponent();

            this.WhenActivated(action => action(ViewModel!.ShowDialog.RegisterHandler(DoShowDialogAsync)));
        }

        private async Task DoShowDialogAsync(InteractionContext<FeesViewModel, MainWindowViewModel?> interaction)
        {
            var dialog = new FeesView();
            dialog.DataContext = interaction.Input;
            var result = await dialog.ShowDialog<MainWindowViewModel?>(this);
            interaction.SetOutput(result);
        }
    }
}