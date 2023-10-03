using BetterConsoleUI.Common.Interfaces;
using BetterConsoleUI.Common.Settings;

namespace BetterConsoleUI.Components.Input_Methods
{
    /// <summary>
    ///     A text input component. The user provides a string and it gets sent to the <see cref="MethodToInvoke"/>.
    /// </summary>
    public class TextInput : IInputMethod
    {
        /// <inheritdoc/>
        public IView ParentView { get; set; }

        /// <summary>
        ///     Method to invoke whent the user's input has been submitted.
        /// </summary>
        public Action<string> MethodToInvoke { get; set; }

        /// <summary>
        ///     Constructor for the <see cref="TextInput"/> class.
        /// </summary>
        /// <param name="parentView">The view containing this input method.</param>
        /// <param name="selections">Possible selections and their <see cref="TextInputSelection.MethodToInvoke"/>.</param>
        public TextInput(IView parentView, Action<string>? selections)
        {
            ParentView = parentView;
            MethodToInvoke = selections ?? ((string x) => { });
        }

        /// <inheritdoc/>
        public bool HasControl { get; set; } = false;

        /// <inheritdoc/>
        public void Print()
        {
        }
        
        /// <inheritdoc/>
        public void Control()
        {
            while (this.HasControl)
            {
                string response = Console.ReadLine() ?? string.Empty;

                if (string.IsNullOrEmpty(response))
                {
                    this.ParentView.Update();
                    continue;
                }

                if (TextInputSettings.ReservedKeywords)
                {
                    if (response == "back")
                    {
                        this.ParentView.GoBack();
                        continue;
                    }

                }

                // Invoke the provided method.
                this.MethodToInvoke(response);
            }
        }

        /// <summary>
        ///     A method to return to the previous view.
        /// </summary>
        public void Back()
        {
            if (this.ParentView.PreviousView != null)
            {
                this.ParentView.PreviousView.Display(this.ParentView.PreviousView.PreviousView, this.ParentView);
            }
        }
    }
}
