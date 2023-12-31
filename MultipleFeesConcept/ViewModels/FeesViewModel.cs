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

        public Loan Loan { get; set; }
        public ObservableCollection<Fee> Fees { get; } = new ObservableCollection<Fee> {

        };

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

        public FeesViewModel(Loan loan)
        {
            Loan = loan;

            Fees.Add(Loan.Fees.FirstOrDefault());
        }

        public void RemoveFee()
        {
            Fees.Remove(SelectedFee);
        }
    }
}
