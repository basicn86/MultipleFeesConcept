using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleFeesConcept.ViewModels
{
    public class ChangeTrackerViewModel : ViewModelBase
    {
        private string _allChanges;
        public string AllChanges
        {
            get { return _allChanges; }
            set { this.RaiseAndSetIfChanged(ref _allChanges, value); }
        }
        public ChangeTrackerViewModel(string changes)
        {
            AllChanges = changes;
        }
    }
}
