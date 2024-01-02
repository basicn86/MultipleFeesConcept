using DynamicData;
using Microsoft.EntityFrameworkCore;
using MultipleFeesConcept.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
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

            ShowAddFeeDialog = new Interaction<AddFeeViewModel, FeeType?>();
            ShowChangeTrackerDialog = new Interaction<ChangeTrackerViewModel, Unit?>();

            AddFeeCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                var addFeeViewModel = new AddFeeViewModel(_context);
                FeeType? result = await ShowAddFeeDialog.Handle(addFeeViewModel);

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

            ShowChangeTrackerCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                //get a list of additions in the context
                var additions = _context.ChangeTracker.Entries<Fee>().Where(x => x.State == Microsoft.EntityFrameworkCore.EntityState.Added).Select(x => x.Entity).ToList();
                string TotalChanges = "";

                foreach (var fee in additions)
                {
                    TotalChanges += $"Added Fee: {fee.FeeType.name}\n";
                    //list all the properties of the fee
                    TotalChanges += $"->Amount: {fee.amount}\n";
                    TotalChanges += $"->Payee: {fee.payee}\n";
                    TotalChanges += $"->POC Amount: {fee.poc_amount}\n";
                    TotalChanges += $"->POC By: {fee.PocBy?.name}\n";
                    TotalChanges += $"\n";
                }

                var deletions = _context.ChangeTracker.Entries<Fee>().Where(x => x.State == Microsoft.EntityFrameworkCore.EntityState.Deleted).Select(x => x.Entity).ToList();
                foreach (var fee in deletions)
                {
                    TotalChanges += $"Deleted Fee: {fee.FeeType.name}\n";
                    //list all the properties of the fee
                    TotalChanges += $"->Amount: {fee.amount}\n";
                    TotalChanges += $"->Payee: {fee.payee}\n";
                    TotalChanges += $"->POC Amount: {fee.poc_amount}\n";
                    TotalChanges += $"->POC By: {fee.PocBy?.name}\n";
                    TotalChanges += $"\n";
                }

                using MortgageDbContext ComparisonContext = new MortgageDbContext();

                //modifications
                var modifications = _context.ChangeTracker.Entries<Fee>().Where(x => x.State == Microsoft.EntityFrameworkCore.EntityState.Modified).Select(x => x.Entity).ToList();
                foreach (var fee in modifications)
                {
                    TotalChanges += $"Modified Fee: {fee.FeeType.name}\n";
                    //get the original fee
                    var originalFee = ComparisonContext.Fee.Where(x => x.ID == fee.ID).Include(_ => _.PocBy).FirstOrDefault();
                    if (originalFee == null) continue;

                    //compare the original fee to the current fee
                    if (originalFee.amount != fee.amount)
                    {
                        TotalChanges += $"->Amount changed from {originalFee.amount} to {fee.amount}\n";
                    }
                    if (originalFee.payee != fee.payee)
                    {
                        TotalChanges += $"->Payee changed from {originalFee.payee} to {fee.payee}\n";
                    }
                    if (originalFee.poc_amount != fee.poc_amount)
                    {
                        TotalChanges += $"->POC Amount changed from {originalFee.poc_amount} to {fee.poc_amount}\n";
                    }
                    if (originalFee.PocBy != fee.PocBy)
                    {
                        TotalChanges += $"->POC By changed from {originalFee.PocBy?.name} to {fee.PocBy?.name}\n";
                    }
                    TotalChanges += $"\n";
                }

                var changeTrackerViewModel = new ChangeTrackerViewModel(TotalChanges);
                await ShowChangeTrackerDialog.Handle(changeTrackerViewModel);
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
        public ICommand ShowChangeTrackerCommand { get; }

        public Interaction<AddFeeViewModel, FeeType?> ShowAddFeeDialog { get; set; }
        public Interaction<ChangeTrackerViewModel, Unit?> ShowChangeTrackerDialog { get; set; }
    }
}
