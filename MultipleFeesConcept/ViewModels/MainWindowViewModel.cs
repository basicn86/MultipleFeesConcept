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

        private string availableLoans = "";
        public string AvailableLoans
        {
            set
            {
                this.RaiseAndSetIfChanged(ref availableLoans, value);
            }
            get
            {
                return availableLoans;
            }
        }

        public MainWindowViewModel()
        {

            ShowDialog = new Interaction<FeesViewModel, MainWindowViewModel?>();

            ShowFeesCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                if (_loanNumber == null) return;

                try
                {
                    using MortgageDbContext db = new MortgageDbContext();
                    db.Database.EnsureCreated();

                    //get the loan from the database
                    Loan? loan = (from l in db.Loan where l.ID == _loanNumber select l).ToArray()[0];

                    //if loan is null, then the loan number was not found
                    if (loan == null) return;

                    var feesViewModel = new FeesViewModel(loan);
                    await ShowDialog.Handle(feesViewModel);
                }
                catch (Exception e)
                {
                    
                }
            });

            using MortgageDbContext db = new MortgageDbContext();
            db.Database.EnsureCreated();
            
            //get a max of 3 loans
            List<Loan> loans = db.Loan.Take(3).ToList();

            AvailableLoans = "";
            foreach(Loan loan in loans)
            {
                AvailableLoans += loan.ID + " / " + loan.borrower_name + " / " + loan.address + "\n";
            }
        }

        public ICommand ShowFeesCommand { get; }

        public Interaction<FeesViewModel, MainWindowViewModel?> ShowDialog { get; }
    }
}
