using BetterConsoleUI.Common.Interfaces;
using BetterConsoleUI.Components.Input_Methods;

namespace BetterConsoleUI.Components.Views
{
    /// <summary>
    ///     A View using the <see cref="MultipleSelect"/> component.
    /// </summary>
    public class MultipleSelectView : View
    {
        /// <summary>
        ///     A <see cref="List{MultipleSelectSelection}"/> describing possible selections and their attached <see cref="Action"/>.
        /// </summary>
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

        /// <summary>
        ///     Constructor for the <see cref="MultipleSelectView"/> class. This view contains a <see cref="MultipleSelect"/> component. 
        ///     Allowing the user to make multiple selections by pressing the up_arrow, down_arrow, and right_arrow buttons and upon submitting the selections with the enter key.
        ///     When the selections are submitted the <see cref="MultipleSelect"/> component invokes all of the selected <see cref="MultipleSelectSelection.MethodToInvoke"/>.
        /// </summary>
        /// <param name="header">The text to display at the top of the view.</param>
        /// <param name="selections">A list of possible selections.</param>
        /// <param name="previousView">The previous view to return to upon the user pressing the left_arrow key.</param>
        public MultipleSelectView(string? header, List<MultipleSelectSelection>? selections = null, IView? previousView = null)
        {
            this.Header = header;
            this.Input = new MultipleSelect(this, selections);
            this.PreviousView = previousView;
        }
    }
}
