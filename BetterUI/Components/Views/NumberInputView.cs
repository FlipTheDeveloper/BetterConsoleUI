using BetterConsoleUI.Common.Interfaces;
using BetterConsoleUI.Components.Input_Methods;

namespace BetterConsoleUI.Components.Views
{
    /// <summary>
    ///     A View using the <see cref="NumberInput"/> component.
    /// </summary>
    public class NumberInputView : View
    {
        /// <summary>
        ///     The <see cref="Action{int}"/> to invoke when the user attempts to submit the selected number. 
        /// </summary>
        public Action<int>? MethodToInvoke
        {
            get
            {
                var input = this.Input as NumberInput;

                if (input != null)
                {
                    return input.MethodToInvoke;
                }

                return null;
            }

            set
            {
                var input = this.Input as NumberInput;

                if (input != null && value != null)
                {
                    input.MethodToInvoke = value;
                }
            }
        }

        /// <summary>
        ///     A number input view. The user uses the up_arrow, and down_arrow to select an integer 
        ///     between <paramref name="minNumber"/> and <paramref name="maxNumber"/>. When the user attempts 
        ///     to sumbit the input, <see cref="NumberInput"/> invokes the <paramref name="methodToInvoke"/> 
        ///     passing in the selected int as a parameter.
        /// </summary>
        /// <param name="header">The text to display at the top of the view.</param>
        /// <param name="methodToInvoke">The action to invoke when the user submits their input</param>
        /// <param name="minNumber">The minimum number to allow the user to select.</param>
        /// <param name="maxNumber">The maximum number to allow the user to select.</param>
        /// <param name="previousView">The view to return to when the user pressed the left_arrow button.</param>
        public NumberInputView(string? header, Action<int>? methodToInvoke = null, int minNumber = 0, int maxNumber = 100, IView? previousView = null)
        {
            this.Header = header;
            this.MethodToInvoke = methodToInvoke ?? ((int _) => { this.Update(); });
            this.Input = new NumberInput(this, methodToInvoke!, minNumber, maxNumber);
            this.PreviousView = previousView;
        }
    }
}
