using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleFeesConcept.ViewModels
{
    public class FeesViewModel : ViewModelBase
    {
        public string loanAmount { get; }

        public FeesViewModel(int _loanAmount)
        {
            loanAmount = _loanAmount.ToString();
        }
    }
}
