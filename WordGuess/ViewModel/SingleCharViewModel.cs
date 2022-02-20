namespace WordGuess.ViewModel
{
    public class SingleCharViewModel
    { 
        public SingleCharViewModel(char ch, CharState state)
        {
            Char = ch;
            State = state;
        }
        public char Char { get; set; }
        public CharState State { get; }
    }
}
