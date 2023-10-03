using BetterConsoleUI.Common.Interfaces;
using BetterConsoleUI.Components.Input_Methods;

namespace BetterConsoleUI.Components.Views
{
    /// <summary>
    ///     A View using the <see cref="NumberInput"/> component.
    /// </summary>
    public class RadioButtonView : View
    {
        /// <summary>
        ///     A <see cref="List{RadioButtonSelection}"/> describing possible selections and their attached <see cref="Action"/>.
        /// </summary>
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

        /// <summary>
        ///     Constructor for the <see cref="RadioButtonView"/> class. This view contains a <see cref="RadioButton"/> component. 
        ///     Allowing the user to make a selection by pressing the up_arrow or the down_arrow. And submitting the selections with the enter key or the right_arrow key.
        ///     When the selections are submitted the <see cref="RadioButton"/> component invokes the selected <see cref="RadioButtonSelection.MethodToInvoke"/>.
        /// </summary>
        /// <param name="header">The text to display at the top of the view.</param>
        /// <param name="selections">A list of possible selections.</param>
        /// <param name="previousView">The previous view to return to upon the user pressing the left_arrow key.</param>
        public RadioButtonView(string? header, List<RadioButtonSelection>? selections = null, IView? previousView = null)
        {
            this.Header = header;
            this.Input = new RadioButton(this, selections);
            this.PreviousView = previousView;
        }
    }
}
