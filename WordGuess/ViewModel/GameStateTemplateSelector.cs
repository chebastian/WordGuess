using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordGuess.ViewModel
{
    internal class GameStateTemplateSelector : DataTemplateSelector
    {
        public DataTemplate PlayingTemplate { get; set; }
        public DataTemplate GameOverTemplate { get; set; }
        public DataTemplate CorrectTemplate { get; set; }
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var game = item as GameViewModel;
            return game.State switch
            {
                GameState.Playing => PlayingTemplate,
                GameState.Won => CorrectTemplate,
                GameState.Lost => GameOverTemplate,
                _ => throw new ArgumentOutOfRangeException(nameof(game.State))
            };
        }
    }
}
