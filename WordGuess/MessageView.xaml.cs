using System.Windows.Input;

namespace WordGuess;

public partial class MessageView : ContentView
{
	public static readonly BindableProperty MessageProperty = BindableProperty.Create(nameof(Message),typeof(string),typeof(MessageView),string.Empty);
	public string Message
	{
		get { return (string)GetValue(MessageProperty); }
		set { SetValue(MessageProperty, value); }
	}

	public static readonly BindableProperty ButtonLableProperty = BindableProperty.Create(nameof(ButtonLabel),typeof(string),typeof(MessageView),string.Empty);
	public string ButtonLabel
	{
		get { return (string)GetValue(ButtonLableProperty); }
		set { SetValue(ButtonLableProperty, value); }
	}

	public static readonly BindableProperty ButtonCommandProperty = BindableProperty.Create(nameof(ButtonCommand), typeof(ICommand), typeof(MessageView), null);
	public ICommand ButtonCommand
    {
        get { return (ICommand)GetValue(ButtonCommandProperty); }
        set { SetValue(ButtonCommandProperty, value); }
    }

	public MessageView()
	{
		InitializeComponent();
	}
}