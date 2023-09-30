using BetterConsoleUI.Common.Interfaces;
using BetterConsoleUI.Components.Input_Methods;

namespace BetterConsoleUI.Components.Views
{
    public class MultipleSelectView : View
    {
        public List<MultipleSelectSelection>? Selections
        {
            get
            {
                var input = this.Input as MultipleSelect;

                if (input != null)
                {
                    return input.Selections;
                }

                return null;
            }

            set
            {
                var input = this.Input as MultipleSelect;

                if (input != null && value != null)
                {
                    input.Selections = value;
                }
            }
        }

        public MultipleSelectView(string? header, List<MultipleSelectSelection>? selections = null, IView? previousView = null)
        {
            this.Header = header;
            this.Input = new MultipleSelect(this, selections);
            this.PreviousView = previousView;
        }
    }
}
