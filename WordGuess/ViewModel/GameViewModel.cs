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

        public WordRow(string guess, string answer)
        {
        }

        public WordRow()
        {
            Guess = new List<SingleCharViewModel>()
            { 
                new SingleCharViewModel(' ',Colors.White),
                new SingleCharViewModel(' ',Colors.White),
                new SingleCharViewModel(' ',Colors.White),
                new SingleCharViewModel(' ',Colors.White),
                new SingleCharViewModel(' ',Colors.White),
            };
        }

        public static WordRow CreateGuess(string guess, string answer)
        {
            var row = new WordRow();
            row.Guess = WordRow.MakeGuess(guess.ToUpper(), answer.ToUpper()).Select(x => new ViewModel.SingleCharViewModel(x.c, x.state switch
            { 
                GuessState.InWord => Colors.Yellow,
                GuessState.Correct => Colors.Green,
                _ => Colors.Gray
            })).ToList();
            return row;
        }
        public static WordRow CreateHint(string guess)
        {
            var row = new WordRow();
            row.Guess = guess.ToUpper().ToCharArray().Select(x => new SingleCharViewModel(x, x == ' ' ? Colors.White : Colors.LightBlue)).ToList();
            return row;
        }

        public static WordRow EmptyGuess()
        {
            return CreateHint("     ");
        }

        public enum GuessState { Incorrect, InWord, Correct, NoGuess }
        public static List<(char c, GuessState state)> MakeGuess(string guess, string answer)
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

        public int Current { get; private set; }

        [ObservableProperty]
        private string _nextGuess;
        [ObservableProperty]
        private List<char> topRowKeys;
        [ObservableProperty]
        private List<char> midRowKeys;
        [ObservableProperty]
        private List<char> bottomRowKeys;
        public ICommand EnterCommand { get; set; }
        public ICommand NewGameCommand { get; set; }

        private string _correctWord;


        public GameViewModel()
        {
            _name = "swello";
            EnterCommand = new RelayCommand(OnEnter);
            NewGameCommand = new RelayCommand(OnNewGame);
            TopRowKeys = "qwertyuiop".ToCharArray().ToList();
            MidRowKeys = "asdfghjkl".ToCharArray().ToList();
            BottomRowKeys = "zxcvbnm".ToCharArray().ToList();
            InitGame("board");
        }

        private void InitGame(string word)
        {
            _correctWord = word;
            _guesses = new ObservableCollection<WordRow>()
            { 
                WordRow.EmptyGuess(),
                WordRow.EmptyGuess(),
                WordRow.EmptyGuess(),
                WordRow.EmptyGuess(),
                WordRow.EmptyGuess(),
            };

            Current = 0;
            NextGuess = ""; 
        }

        [ICommand]
        public void EnterLetter(char letter)
        {
            //if (Guesses.Any())
            //    Guesses.Remove(Guesses.Last());

            if(NextGuess.Length + 1 <= 5)
                NextGuess = $"{NextGuess}{letter}";

            Guesses[Current] = WordRow.CreateHint(NextGuess.PadRight(5));
        }

        [ICommand]
        public void RemoveLetter()
        {
            if(NextGuess.Length > 0)
                NextGuess = NextGuess.Substring(0,NextGuess.Length - 1);
            Guesses[Current] = WordRow.CreateHint(NextGuess.PadRight(5));
        }

        private void OnNewGame()
        {
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

            InitGame(words[new Random().Next(words.Count - 1)]);
        }

        private void AddGuess(string guess)
        {
            _guesses[Current] = WordRow.CreateGuess(guess,_correctWord);
        }

        private void OnEnter()
        {
            if (NextGuess.Length < 5)
                return;

            if(Current +1 == 5 && _correctWord != NextGuess)
            {
                OnNewGame();
            }

            AddGuess(NextGuess);
            OnPropertyChanged(nameof(Guesses));
            NextGuess = "";
            Current++;
        }
    }
}
