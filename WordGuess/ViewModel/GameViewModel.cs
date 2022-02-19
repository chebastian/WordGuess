using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WordGuess.ViewModel
{ 
    public class SingleCharViewModel
    { 
        public SingleCharViewModel(char ch, Color col)
        {
            Char = ch;
            Color = col; 
        }
        public char Char { get; set; }
        public Color Color { get; }
    }

    public partial class WordRow : ObservableObject
    {
        [ObservableProperty]
        private List<SingleCharViewModel> _guess;
        public string ReadableGuess { get; set; }

        public WordRow(string guess, string answer)
        {
            ReadableGuess = guess;
            Guess = MakeGuess(guess.ToUpper(), answer.ToUpper()).Select(x => new ViewModel.SingleCharViewModel(x.c, x.state switch
            { 
                GuessState.InWord => Colors.Yellow,
                GuessState.Correct => Colors.Green,
                _ => Colors.Gray
            })).ToList();
        }

        public enum GuessState { Incorrect, InWord, Correct }
        public List<(char c, GuessState state)> MakeGuess(string guess, string answer)
        {
            var res = new List<(char c, GuessState)>();
            var answerArr = answer.ToCharArray();
            foreach(var c in guess.Select((x,i) => (character: x,index: i)))
            {
                if (answerArr[c.index] == c.character)
                    res.Add((c.character, GuessState.Correct));
                else if (answer.Contains(c.character))
                    res.Add((c.character, GuessState.InWord));
                else
                    res.Add((c.character, GuessState.Incorrect));
            }
            return res;
        }
    }

    public partial class GameViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _name;
        [ObservableProperty]
        private ObservableCollection<WordRow> _guesses;
        [ObservableProperty]
        private string _nextGuess;
        public ICommand EnterCommand { get; set; }
        public ICommand NewGameCommand { get; set; }

        private string _correctWord;


        public GameViewModel()
        {
            _name = "swello";
            _correctWord = "board";
            _guesses = new ObservableCollection<WordRow>();

            EnterCommand = new RelayCommand(OnEnter);
            NewGameCommand = new RelayCommand(OnNewGame);
        }

        private void OnNewGame()
        {
            Guesses.Clear();
            NextGuess = "";
            var words = new List<string>
            { 
                "board",
                "hoard",
                "clear",
                "coral",
                "lakes",
                "snake",
                "codes",
            };

            _correctWord = words[new Random().Next(words.Count - 1)];
        }

        private void AddGuess(string guess)
        {
            _guesses.Add(new WordRow(guess, _correctWord));
        }

        private void OnEnter()
        {
            AddGuess(NextGuess);
            OnPropertyChanged(nameof(Guesses));
            NextGuess = "";
        }
    }
}
