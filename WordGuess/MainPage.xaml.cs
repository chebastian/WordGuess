namespace WordGuess;

using Microsoft.Toolkit.Mvvm.Input;
using WordGuess.ViewModel;

public partial class MainPage : ContentPage
{
	public MainPage(GameViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}

    private void OnCompleted(object sender, EventArgs e)
    {
    }
}

