using DynamicData;
using MultipleFeesConcept.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MultipleFeesConcept.ViewModels
{
    public class FeesViewModel : ViewModelBase
    {
        private MortgageDbContext _context;

        public Loan Loan { get; set; }
        private Fee? _selectedFee;
        public Fee? SelectedFee {
            get
            {
                return _selectedFee;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedFee, value);
            }
        }

        public ObservableCollection<Fee> ObservableFees { get; }
        public ObservableCollection<PocBy> PocByCollection { get; }

        public FeesViewModel(Loan loan)
        {
            _context = new MortgageDbContext();

            Loan = loan;

            _context.Attach(Loan);

            PocByCollection = new ObservableCollection<PocBy>(_context.PocBy);
            ObservableFees = new ObservableCollection<Fee>(Loan.Fees);

            ShowDialog = new Interaction<AddFeeViewModel, FeeType?>();

            AddFeeCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                var addFeeViewModel = new AddFeeViewModel(_context);
                FeeType? result = await ShowDialog.Handle(addFeeViewModel);

                //return if null
                if (result == null) return;

                //add the fee to the loan
                Fee pendingFee = new Fee()
                {
                    FeeType = result,
                    Loan = Loan
                };

                //add the fee to the loan and observation collection
                Loan.Fees.Add(pendingFee);
                ObservableFees.Add(pendingFee);
            });
        }

        //save changes on closing
        public async Task Closing()
        {
            await _context.SaveChangesAsync();
        }

        ~FeesViewModel()
        {
            //save changes
            _context.Dispose();
        }

        public void RemoveFee()
        {
            if (SelectedFee == null) return;
            Loan.Fees.Remove(SelectedFee);
            ObservableFees.Remove(SelectedFee);
        }

        public ICommand AddFeeCommand { get; }

        public Interaction<AddFeeViewModel, FeeType?> ShowDialog { get; set; }
    }
}
