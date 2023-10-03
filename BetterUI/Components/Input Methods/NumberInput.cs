using BetterConsoleUI.Common.Interfaces;

namespace BetterConsoleUI.Components.Input_Methods
{
    /// <summary>
    ///     A number input component. This component allows the user to
    ///     select a number between the <see cref="MinNumber"/> and the <see cref="MaxNumber"/>.
    /// </summary>
    public class NumberInput: IInputMethod
    {
        /// <inheritdoc/>
        public IView ParentView { get; set; }

        /// <summary>
        ///     The number currently selected by the user.
        /// </summary>
        public int CurrentNumber = 0;

        /// <summary>
        ///     The smallest number the user may select.
        /// </summary>
        public int MinNumber = 0;

        /// <summary>
        ///     The largest number the user may select.
        /// </summary>
        public int MaxNumber = 0;

        /// <summary>
        ///     A constructor for the <see cref="NumberInput"/> component.
        /// </summary>
        /// <param name="parentView">The view containing this input method.</param>
        /// <param name="methodToInvoke">The method to invoke, passing in the user's selected number.</param>
        /// <param name="minNumber">The smallest number the user may select.</param>
        /// <param name="maxNumber">The largest number the user may select.</param>
        /// <exception cref="ArgumentOutOfRangeException">Exception thrown if the <paramref name="minNumber"/> is larger than the <paramref name="maxNumber"/>.</exception>
        public NumberInput(IView parentView, Action<int> methodToInvoke, int minNumber, int maxNumber)
        {
            ParentView = parentView;
            MethodToInvoke = methodToInvoke;

            if (minNumber > maxNumber)
            {
                throw new ArgumentOutOfRangeException("The minimum value on the NumberInput input method is larger than the maximum value.");
            }

            MinNumber = minNumber;
            MaxNumber = maxNumber;
        }

        /// <summary>
        ///     The method to pass in the <see cref="CurrentNumber"/> when the user attempts to sumbit the view.
        /// </summary>
        public Action<int> MethodToInvoke { get; set; }

        /// <inheritdoc/>
        public bool HasControl { get; set; } = false;

        /// <inheritdoc/>
        public void Print()
        {
            if (CurrentNumber < MinNumber)
            {
                CurrentNumber = MinNumber;
            }

            if (CurrentNumber > MaxNumber)
            {
                CurrentNumber= MaxNumber;
            }

            Console.WriteLine("▲".PadLeft(4).PadRight(5));
            Console.WriteLine(CurrentNumber.ToString().PadLeft(4).PadRight(5));
            Console.WriteLine("▼".PadLeft(4).PadRight(5));
        }

        /// <inheritdoc/>
        public void Control()
        {
            while (this.HasControl)
            {
                ConsoleKeyInfo keyPress = Console.ReadKey();

                switch (keyPress.Key)
                {
                    // Go to the previous selection.70
                    case ConsoleKey.UpArrow:
                        {
                            CurrentNumber++;
                            this.ParentView.Update();
                            break;
                        }

                    // Go to the next selection.
                    case ConsoleKey.DownArrow:
                        {
                            CurrentNumber--;
                            this.ParentView.Update();
                            break;
                        }

                    // Go to the previous view (if any)
                    case ConsoleKey.LeftArrow:
                        {
                            if (this.ParentView.PreviousView is not null)
                            {
                                // If there is a previous view to display we do, and we set the sender as the PreviousView's PreviousView.
                                // So we can go back two steps and so on.
                                this.ParentView.PreviousView.Display(this.ParentView.PreviousView.PreviousView, this.ParentView);
                            }

                            break;
                        }

                    // Invoke the selections function.
                    case ConsoleKey.RightArrow:
                    case ConsoleKey.Spacebar:
                    case ConsoleKey.Enter:
                        {
                            this.MethodToInvoke.Invoke(CurrentNumber);
                            break;
                        }

                }
            }

        }

    }
}
