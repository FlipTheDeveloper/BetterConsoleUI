using BetterConsoleUI.Common.Interfaces;
using BetterConsoleUI.Components.Input_Methods;
using BetterConsoleUI.Common.Settings;

namespace BetterConsoleUI.Components.Views
{
    /// <summary>
    ///     A View using the <see cref="TextSelect"/> component.
    /// </summary>
    public class InfoView : View
    {
        /// <summary>
        ///     A <see cref="Action"/> to invoke upoon submissal of component.
        /// </summary>
        public Action? MethodToInvoke
        {
            get
            {
                var input = this.Input as AnyKeyToContinue;

                if (input != null)
                {
                    return input.MethodToInvoke;
                }

                return null;
            }

            set
            {
                var input = this.Input as AnyKeyToContinue;

                if (input != null && value != null)
                {
                    input.MethodToInvoke = value;
                }
            }
        }

        /// <summary>
        ///     Contructor for the <see cref="InfoView"/> class. This view displays some text to the user, then awaits for the user to press any key on the keyboard before invoking the <see cref="MethodToInvoke"/>
        /// </summary>
        /// <param name="header">The text to display at the top of the view.</param>
        /// <param name="methodToInvoke">Method to invoke upon submissal of this view.</param>
        /// <param name="previousView">
        ///     The previous view to return to upon the user pressing the left arrow on the keyboard.
        /// </param>
        public InfoView(string? header, Action? methodToInvoke = null, IView? previousView = null)
        {
            this.Header = header;
            this.Input = new AnyKeyToContinue(this, methodToInvoke);
            this.PreviousView = previousView;
        }

        /// <inheritdoc/>
        public override void Update()
        {
            // If something calls to update this view, but this view's input
            // is not null and doesn't have control. It is probably a mistake.

            if (this.Input == null || this.Input.HasControl != false)
            {
                Console.Clear();
                Console.WriteLine(this.Header ?? string.Empty);

                if (this.Input != null)
                {
                    this.Input.Print();
                }
            }
            else
            {
                throw new InvalidOperationException("Tried to update a view with an input that doesn't have control.");
            }
        }
    }
}
