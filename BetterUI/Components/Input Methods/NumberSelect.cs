using BetterConsoleUI.Common.Interfaces;

namespace BetterConsoleUI.Components.Input_Methods
{
    public class NumberSelect: ISelectionInputMethod
    {
        public IView ParentView { get; set; }

        public int CurrentNumber = 0;
        public int MinNumber = 0;
        public int MaxNumber = 0;

        public NumberSelect(IView parentView, Action<int> methodToInvoke, int minNumber, int maxNumber)
        {
            ParentView = parentView;
            MethodToInvoke = methodToInvoke;

            if (minNumber > maxNumber)
            {
                throw new ArgumentOutOfRangeException("The minimum value on the NumberSelect input method is larger than the maximum value.");
            }

            MinNumber = minNumber;
            MaxNumber = maxNumber;
        }

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
