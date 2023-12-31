using DynamicData;
using MultipleFeesConcept.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleFeesConcept.ViewModels
{
    public class FeesViewModel : ViewModelBase
    {
        private MortgageDbContext _context;

        public Loan Loan { get; set; }
        private Fee _selectedFee;
        public Fee SelectedFee {
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
            Loan.Fees.Remove(SelectedFee);
            ObservableFees.Remove(SelectedFee);
        }
    }
}
