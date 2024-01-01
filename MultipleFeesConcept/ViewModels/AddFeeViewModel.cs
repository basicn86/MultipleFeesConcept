using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.ReactiveUI;
using ReactiveUI;

namespace MultipleFeesConcept.ViewModels
{
    public class AddFeeViewModel : ViewModelBase
    {
        public AddFeeViewModel()
        {
            AddBtn = ReactiveCommand.Create(() =>
            {
                return new Models.FeeType();
            });

            CancelBtn = ReactiveCommand.Create(() => { return; });
        }

        public ReactiveCommand<Unit, Models.FeeType> AddBtn { get; }

        public ReactiveCommand<Unit, Unit> CancelBtn { get; }
    }
}
