using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordGuess.ViewModel
{
    internal class GuessTemplateSelector : DataTemplateSelector
    {
        public DataTemplate Empty { get; set; }
        public DataTemplate Guess { get; set; }
        public DataTemplate Correct { get; set; }
        public DataTemplate Wrong { get; set; }
        public DataTemplate Misplaced { get; set; }
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var ch = item as SingleCharViewModel;
            return ch.State switch
            { 
                CharState.Correct => Correct,
                CharState.Wrong => Wrong,
                CharState.InWord => Misplaced,
                CharState.Guess => Guess,
                _ => Empty
            };
        }
    }
}
