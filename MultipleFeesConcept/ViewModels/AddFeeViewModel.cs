using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.ReactiveUI;
using DynamicData;
using MultipleFeesConcept.Models;
using ReactiveUI;

namespace MultipleFeesConcept.ViewModels
{
    public class AddFeeViewModel : ViewModelBase
    {
        private FeeType _selectedFeeType;
        public FeeType SelectedFeeType
        {
            get => _selectedFeeType;
            set => this.RaiseAndSetIfChanged(ref _selectedFeeType, value);
        }
        public ObservableCollection<FeeType> FeeTypes { get; }
        public AddFeeViewModel(MortgageDbContext mortgageDbContext)
        {
            AddBtn = ReactiveCommand.Create(() =>
            {
                return SelectedFeeType;
            });

            CancelBtn = ReactiveCommand.Create(() => { return; });

            FeeTypes = new ObservableCollection<FeeType>();
            FeeTypes.AddRange(mortgageDbContext.FeeType.ToList());
        }

        public ReactiveCommand<Unit, Models.FeeType> AddBtn { get; }

        public ReactiveCommand<Unit, Unit> CancelBtn { get; }
    }
}
