using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using System;

namespace MultipleFeesConcept.Views;

public partial class AddFeeView : ReactiveWindow<ViewModels.AddFeeViewModel>
{ 
    public AddFeeView()
    {
        InitializeComponent();

        this.WhenActivated(d => d(ViewModel.AddBtn.Subscribe(Close)));
    }
}