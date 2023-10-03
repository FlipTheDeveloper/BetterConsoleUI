using BetterConsoleUI.Common.Interfaces;
using BetterConsoleUI.Components.Input_Methods;
using BetterConsoleUI.Common.Settings;

namespace BetterConsoleUI.Components.Views
{
    /// <summary>
    ///     A View using the <see cref="TextInput"/> component.
    /// </summary>
    public class TextInputView : View
    {
        /// <summary>
        ///     A <see cref="Action{string}"/> to invoke upoon submissal of component.
        /// </summary>
        public Action<string>? MethodToInvoke 
        {
            get
            {
                var input = this.Input as TextInput;

                if (input != null)
                {
                    return input.MethodToInvoke;
                }

                return null;
            }

            set
            {
                var input = this.Input as TextInput;

                if (input != null && value != null)
                {
                    input.MethodToInvoke = value;
                }
            }
        }

        /// <summary>
        ///     Contructor for the <see cref="TextInputView"/> class. This view contains the <see cref="TextInput"/> component,
        ///     which allows the user to type in some input to be passed to the <see cref="TextInput.MethodToInvoke"/>.
        /// </summary>
        /// <param name="header">The text to display at the top of the view.</param>
        /// <param name="methodToInvoke">A method to invoke upon the submissal of this view.</param>
        /// <param name="previousView">
        ///     The previous view to return to upon the user typing the "back" keyword. 
        ///     This functionality can be changed in the <see cref="TextInputSettings"/> static class.
        /// </param>
        public TextInputView(string? header, Action<string>? methodToInvoke = null, IView? previousView = null)
        {
            this.Header = header;
            this.Input = new TextInput(this, methodToInvoke);
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
