using WordGuess.ViewModel;
using Xunit;

namespace WordTests
{
    public class UnitTest1
    {
        [Fact]
        public void WordWithMixedstates()
        {
            var guess = WordRow.MakeGuess("bread","board");
            Assert.Collection(guess,
                x => Assert.Equal(CharState.Correct, x.state),
                x => Assert.Equal(CharState.InWord, x.state),
                x => Assert.Equal(CharState.Wrong, x.state),
                x => Assert.Equal(CharState.InWord, x.state),
                x => Assert.Equal(CharState.Correct, x.state));
        }

        [Fact]
        public void AllIncorrect()
        {
            var guess = WordRow.MakeGuess("xxxxx","board");
            Assert.Collection(guess,
                x => Assert.Equal(CharState.Wrong, x.state),
                x => Assert.Equal(CharState.Wrong, x.state),
                x => Assert.Equal(CharState.Wrong, x.state),
                x => Assert.Equal(CharState.Wrong, x.state),
                x => Assert.Equal(CharState.Wrong, x.state));
        }

        [Fact]
        public void CorrectGuess()
        {
            var guess = WordRow.MakeGuess("board","board");
            Assert.Collection(guess,
                x => Assert.Equal(CharState.Correct, x.state),
                x => Assert.Equal(CharState.Correct, x.state),
                x => Assert.Equal(CharState.Correct, x.state),
                x => Assert.Equal(CharState.Correct, x.state),
                x => Assert.Equal(CharState.Correct, x.state));
        }

        [Fact]
        public void MisplacedTwice()
        {
            var guess = WordRow.MakeGuess("baaaa","abbbb");
            Assert.Collection(guess,
                x => Assert.Equal(CharState.InWord, x.state),
                x => Assert.Equal(CharState.InWord, x.state),
                x => Assert.Equal(CharState.Wrong, x.state),
                x => Assert.Equal(CharState.Wrong, x.state),
                x => Assert.Equal(CharState.Wrong, x.state));
        }

        [Fact]
        public void MisplacedThenCorrect()
        {
            var guess = WordRow.MakeGuess("bbxxx","abccc");
            Assert.Collection(guess,
                x => Assert.Equal(CharState.InWord, x.state),
                x => Assert.Equal(CharState.Wrong, x.state),
                x => Assert.Equal(CharState.Wrong, x.state),
                x => Assert.Equal(CharState.Wrong, x.state),
                x => Assert.Equal(CharState.Wrong, x.state));
        }
    }
}