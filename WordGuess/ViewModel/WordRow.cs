using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace WordGuess.ViewModel
{
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
                new SingleCharViewModel(' ',CharState.Empty),
                new SingleCharViewModel(' ',CharState.Empty),
                new SingleCharViewModel(' ',CharState.Empty),
                new SingleCharViewModel(' ',CharState.Empty),
                new SingleCharViewModel(' ',CharState.Empty),
            };
        }

        public static WordRow CreateGuess(string guess, string answer)
        {
            var row = new WordRow();
            row.Guess = WordRow.MakeGuess(guess.ToUpper(), answer.ToUpper()).Select(x => new ViewModel.SingleCharViewModel(x.c, x.state)).ToList();
            return row;
        }
        public static WordRow CreateHint(string guess)
        {
            var row = new WordRow();
            row.Guess = guess.ToUpper()
                .ToCharArray()
                .Select(x => new SingleCharViewModel(x, x == ' ' ? CharState.Empty : CharState.Guess))
                .ToList();
            return row;
        }

        public static WordRow EmptyGuess()
        {
            return CreateHint("     ");
        }
        public static List<(char c, CharState state)> MakeGuess(string guess, string answer)
        {
            var res = new List<(char c, CharState)>();
            var answerArr = answer.ToCharArray();
            var availableChars = answer.ToCharArray().ToList();
            var grouped = guess.GroupBy(x => x);
            foreach(var c in guess.Select((x,i) => (character: x,index: i)))
            {
                if (answerArr[c.index] == c.character && availableChars.Contains(c.character))
                {
                    res.Add((c.character, CharState.Correct));
                    availableChars.Remove(c.character);
                }
                else if (availableChars.Contains(c.character))
                {
                    res.Add((c.character, CharState.InWord));
                    availableChars.Remove(c.character);
                }
                else
                {
                    res.Add((c.character, CharState.Wrong)); 
                }
            }
            return res;
        }
    }
}
