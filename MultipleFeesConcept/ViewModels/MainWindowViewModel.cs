using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Windows.Input;
using MultipleFeesConcept.Models;
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

        private string borrowerName;
        public string BorrowerName
        {
            set
            {
                this.RaiseAndSetIfChanged(ref borrowerName, value);
            }
            get
            {
                return borrowerName;
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

            MortgageDbContext db = new MortgageDbContext();
            db.Database.EnsureCreated();
            //get any loan
            Loan loan = db.Loan.FirstOrDefault();
        }

        public ICommand ShowFeesCommand { get; }

        public Interaction<FeesViewModel, MainWindowViewModel?> ShowDialog { get; }
    }
}
