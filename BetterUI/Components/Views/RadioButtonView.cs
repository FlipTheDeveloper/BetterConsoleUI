using BetterConsoleUI.Common.Interfaces;
using BetterConsoleUI.Components.Input_Methods;

namespace BetterConsoleUI.Components.Views
{
    public class RadioButtonView : View
    {
        public List<RadioButtonSelection>? Selections
        {
            get
            {
                var input = this.Input as RadioButton;

                if (input != null)
                {
                    return input.Selections;
                }

                return null;
            }

            set
            {
                var input = this.Input as RadioButton;

                if (input != null && value != null)
                {
                    input.Selections = value;
                }
            }
        }


        public RadioButtonView(string? header, List<RadioButtonSelection>? selections = null, IView? previousView = null)
        {
            this.Header = header;
            this.Input = new RadioButton(this, selections);
            this.PreviousView = previousView;
        }
    }
}
