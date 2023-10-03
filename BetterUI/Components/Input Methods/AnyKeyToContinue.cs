using BetterConsoleUI.Common.Interfaces;
using BetterConsoleUI.Common.Settings;

namespace BetterConsoleUI.Components.Input_Methods
{
    /// <summary>
    ///     A text selection component.
    /// </summary>
    public class AnyKeyToContinue : IInputMethod
    {
        /// <inheritdoc/>
        public IView ParentView { get; set; }

        /// <summary>
        ///     Method to invoke once the user presses a key.
        /// </summary>
        public Action MethodToInvoke { get; set; }

        /// <summary>
        ///     Constructor for the <see cref="AnyKeyToContinue"/> class.
        /// </summary>
        /// <param name="parentView">The view containing this input method.</param>
        public AnyKeyToContinue(IView parentView, Action? methodToInvoke)
        {
            ParentView = parentView;
            MethodToInvoke = methodToInvoke ?? (() => { });
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
                ConsoleKeyInfo keyPress = Console.ReadKey();

                switch (keyPress.Key)
                {
                    // Go to the previous view (if any)
                    case ConsoleKey.LeftArrow:
                        {
                            if (this.ParentView.PreviousView is not null)
                            {
                                // If there is a previous view to display we do, and we set the sender as the PreviousView's PreviousView.
                                // So we can go back two steps and so on.
                                this.ParentView.PreviousView.Display(this.ParentView.PreviousView.PreviousView, this.ParentView);
                                continue;
                            }

                            break;
                        }
                }

                this.MethodToInvoke.Invoke();
            }
        }
    }
}
