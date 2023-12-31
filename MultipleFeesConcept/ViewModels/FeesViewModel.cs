using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleFeesConcept.ViewModels
{
    public struct TestFee
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class FeesViewModel : ViewModelBase
    {
        public string loanAmount { get; }
        public ObservableCollection<TestFee> fees { get; } = new ObservableCollection<TestFee> {
            new TestFee { Id = 1, Name = "Potato" },
            new TestFee { Id = 3, Name = "Apple" },
            new TestFee { Id = 2, Name = "Orange" }
        };

        public FeesViewModel(int _loanAmount)
        {
            loanAmount = _loanAmount.ToString();
        }
    }
}
