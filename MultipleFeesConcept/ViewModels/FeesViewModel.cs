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

        public FeesViewModel(Loan loan)
        {
            _context = new MortgageDbContext();

            Loan = loan;

            _context.Attach(Loan);

            ObservableFees = new ObservableCollection<Fee>(Loan.Fees);

            ShowDialog = new Interaction<AddFeeViewModel, FeeType?>();

            AddFeeCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                var addFeeViewModel = new AddFeeViewModel();
                var result = await ShowDialog.Handle(addFeeViewModel);
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
