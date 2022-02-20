using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WordGuess.ViewModel
{

    public enum GameState
    { 
        Playing,
        Lost,
        Won
    }

    public partial class GameViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _name;
        [ObservableProperty]
        private ObservableCollection<WordRow> _guesses;
        [ObservableProperty]
        private GameState _state;

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

        [ObservableProperty]
        private bool _isGameOver = false;
        [ObservableProperty]
        private bool _isWin = false;

        [ICommand]
        public void RestartGame()
        {
            IsWin = false;
            IsGameOver = false;
            OnNewGame();
        }

        [ICommand]
        public void ToggleGameOver()
        { 
            IsGameOver = !IsGameOver;
        }


        public GameViewModel()
        {
            _name = "swello";
            EnterCommand = new RelayCommand(OnEnter,() => NextGuess.Length == _correctWord.Length);
            NewGameCommand = new RelayCommand(OnNewGame);
            TopRowKeys = "qwertyuiop".ToCharArray().ToList();
            MidRowKeys = "asdfghjkl".ToCharArray().ToList();
            BottomRowKeys = "zxcvbnm".ToCharArray().ToList();
            InitGame("board");
            AddGuess("bread");
            AddGuess("xrrxx");
        }

        private void InitGame(string word)
        {
            State = GameState.Lost;
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
            OnPropertyChanged(nameof(Guesses));
        }

        [ICommand]
        public void EnterLetter(char letter)
        {
            //if (Guesses.Any())
            //    Guesses.Remove(Guesses.Last());

            if(NextGuess.Length + 1 <= 5)
                NextGuess = $"{NextGuess}{letter}";

            Guesses[Current] = WordRow.CreateHint(NextGuess.PadRight(5));
            (EnterCommand as RelayCommand).NotifyCanExecuteChanged();
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
            OnPropertyChanged(nameof(Guesses));
            NextGuess = "";
            Current++;
            (EnterCommand as RelayCommand).NotifyCanExecuteChanged();
        }

        private void OnEnter()
        {
            if (NextGuess.Length < 5)
                return;
            var currentGuess = NextGuess;
            AddGuess(NextGuess);

            if(Current == 5 && _correctWord != currentGuess)
            {
                IsGameOver = true;
            }
            else if (_correctWord == currentGuess)
            {
                IsWin = true;
            }
        }
    }
}
