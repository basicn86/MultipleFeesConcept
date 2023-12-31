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
    public class TestFee
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class FeesViewModel : ViewModelBase
    {

        public Loan Loan { get; set; }
        public ObservableCollection<TestFee> Fees { get; } = new ObservableCollection<TestFee> {
            new TestFee { Id = 1, Name = "Potato" },
            new TestFee { Id = 3, Name = "Apple" },
            new TestFee { Id = 2, Name = "Orange" },
            new TestFee { Id = 4, Name = "Banana" },
            new TestFee { Id = 5, Name = "Pineapple" },
            new TestFee { Id = 6, Name = "Mango" },
            new TestFee { Id = 7, Name = "Peach" }
        };

        private TestFee _selectedFee;
        public TestFee SelectedFee {
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
        }

        public void RemoveFee()
        {
            Fees.Remove(SelectedFee);
        }
    }
}
