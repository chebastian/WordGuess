using System.Windows.Input;

namespace WordGuess;

public partial class MessageView : ContentView
{
	public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(MessageView), Colors.Blue);
	public Color BorderColor
	{
		get { return (Color)GetValue(BorderColorProperty); }
		set { SetValue(BorderColorProperty, value); }
	}

	public static readonly BindableProperty FillColorProperty = BindableProperty.Create(nameof(FillColor), typeof(Color), typeof(MessageView), Colors.Blue);
	public Color FillColor
	{
		get { return (Color)GetValue(FillColorProperty); }
		set { SetValue(FillColorProperty, value); }
	}

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