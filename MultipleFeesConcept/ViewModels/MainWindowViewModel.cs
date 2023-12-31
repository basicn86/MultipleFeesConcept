using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;
using System.Windows.Input;
using ReactiveUI;

namespace MultipleFeesConcept.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private int? _loanNumber;
        public string LoanNumber
        {
            set
            {
                if (int.TryParse(value, out int temp))
                {
                    _loanNumber = temp;
                }
                else
                {
                    _loanNumber = null;
                }
            }
            get
            {
                if (_loanNumber == null)
                {
                    return "";
                }
                else
                {

                    return _loanNumber.ToString();
                }
            }
        }

        public MainWindowViewModel()
        {

            ShowDialog = new Interaction<FeesViewModel, MainWindowViewModel?>();

            ShowFeesCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                if (_loanNumber == null) return;
                var feesViewModel = new FeesViewModel((int)_loanNumber);
                await ShowDialog.Handle(feesViewModel);
            });
        }

        public ICommand ShowFeesCommand { get; }

        public Interaction<FeesViewModel, MainWindowViewModel?> ShowDialog { get; }
    }
}
