﻿namespace WordGuess;
using WordGuess.ViewModel;

public partial class MainPage : ContentPage
{
	public MainPage(GameViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}
}
