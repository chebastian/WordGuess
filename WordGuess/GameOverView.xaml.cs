namespace WordGuess;

public partial class GameOverView : ContentView
{
	public static readonly BindableProperty MessageProperty = BindableProperty.Create(nameof(Message),typeof(string),typeof(GameOverView),string.Empty);

	public string Message
	{
		get { return (string)GetValue(MessageProperty); }
		set { SetValue(MessageProperty, value); }
	}
	public GameOverView()
	{
		InitializeComponent();
	}
}