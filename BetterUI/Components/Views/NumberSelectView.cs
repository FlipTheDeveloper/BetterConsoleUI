using BetterConsoleUI.Common.Interfaces;
using BetterConsoleUI.Components.Input_Methods;

namespace BetterConsoleUI.Components.Views
{
    /// <summary>
    ///     A View using the <see cref="NumberSelect"/> component.
    /// </summary>
    public class NumberSelectView : View
    {
        /// <summary>
        ///     The <see cref="Action{int}"/> to invoke when the user attempts to submit the selected number. 
        /// </summary>
        public Action<int>? MethodToInvoke
        {
            get
            {
                var input = this.Input as NumberSelect;

                if (input != null)
                {
                    return input.MethodToInvoke;
                }

                return null;
            }

            set
            {
                var input = this.Input as NumberSelect;

                if (input != null && value != null)
                {
                    input.MethodToInvoke = value;
                }
            }
        }

        /// <summary>
        ///     A number selection view. The user uses the up_arrow, and down_arrow to select an integer 
        ///     between <paramref name="minNumber"/> and <paramref name="maxNumber"/>. When the user attempts 
        ///     to sumbit the selection, <see cref="NumberSelect"/> invokes the <paramref name="methodToInvoke"/> 
        ///     passing in the selected int as a parameter.
        /// </summary>
        /// <param name="header">The text to display at the top of the view.</param>
        /// <param name="methodToInvoke">The action to invoke when the user submits their selection</param>
        /// <param name="minNumber">The minimum number to allow the user to select.</param>
        /// <param name="maxNumber">The maximum number to allow the user to select.</param>
        /// <param name="previousView">The view to return to when the user pressed the left_arrow button.</param>
        public NumberSelectView(string? header, Action<int>? methodToInvoke = null, int minNumber = 0, int maxNumber = 100, IView? previousView = null)
        {
            this.Header = header;
            this.MethodToInvoke = methodToInvoke ?? ((int _) => { this.Update(); });
            this.Input = new NumberSelect(this, methodToInvoke!, minNumber, maxNumber);
            this.PreviousView = previousView;
        }
    }
}
