using BetterConsoleUI.Common.Interfaces;
using BetterConsoleUI.Common.Settings;

namespace BetterConsoleUI.Components.Input_Methods
{
    /// <summary>
    ///     A RadioButton component. A selection method where only one selection can be made at a time.
    /// </summary>
    public class RadioButton : IInputMethod
    {
        /// <inheritdoc/>
        public IView ParentView { get; set; }

        /// <summary>
        ///     A list of possible selections.
        /// </summary>
        public List<RadioButtonSelection> Selections = new List<RadioButtonSelection>();

        /// <summary>
        ///     Zero-Based index of the users current selection within the <see cref="Selections"/>.
        /// </summary>
        public int CurrentSelection = 0;

        /// <summary>
        ///     Constructor for the <see cref="RadioButton"/> component. 
        /// </summary>
        /// <param name="parentView">The view containing this input method.</param>
        /// <param name="selections">A list of possible selections.</param>
        public RadioButton(IView parentView, List<RadioButtonSelection>? selections)
        {
            ParentView = parentView;
            Selections = selections ?? new List<RadioButtonSelection>();
        }

        /// <inheritdoc/>
        public bool HasControl { get; set; } = false;

        /// <inheritdoc/>
        public void Print()
        {
            string result = string.Empty;
            if (Selections.Where(s => s.IsSelected).Count() == 0)
            {
                var firstSelection = Selections[0];
                firstSelection.IsSelected = true;
                Selections[0] = firstSelection;
            }

            //int paddingX = (Console.WindowWidth - this.Selections.Select(s => s.Text.Length + ((RadioButtonDisplay.Selected.Length + RadioButtonDisplay.NotSelected.Length + 1) / 2)).Max()) / 2;
            foreach (RadioButtonSelection s in Selections)
            {
                result +=  $"{(s.IsSelected ? RadioButtonSettings.Selected : RadioButtonSettings.NotSelected)} {s.Text}\n";
            }

            Console.WriteLine(result);
        }

        /// <inheritdoc/>
        public void Control()
        {
            while (this.HasControl)
            {
                ConsoleKeyInfo keyPress = Console.ReadKey();

                switch (keyPress.Key)
                {
                    // Go to the previous selection.
                    case ConsoleKey.UpArrow:
                        {
                            // Deselect the current selection.
                            var oldSelection = Selections[CurrentSelection];
                            oldSelection.IsSelected = false;
                            Selections[CurrentSelection] = oldSelection;

                            if (CurrentSelection != 0)
                            {
                                CurrentSelection--;
                            }
                            else if (RadioButtonSettings.WrapAround)
                            {
                                CurrentSelection = Selections.Count() - 1;
                            }

                            // Select the current selection.
                            var newSelection = Selections[CurrentSelection];
                            newSelection.IsSelected = true;
                            Selections[CurrentSelection] = newSelection;

                            this.ParentView.Update();
                            break;
                        }

                    // Go to the next selection.
                    case ConsoleKey.DownArrow:
                        {
                            // Deselect the current selection.
                            var oldSelection = Selections[CurrentSelection];
                            oldSelection.IsSelected = false;
                            Selections[CurrentSelection] = oldSelection;

                            if (CurrentSelection != Selections.Count() - 1)
                            {
                                CurrentSelection++;
                            }
                            else if (RadioButtonSettings.WrapAround)
                            {
                                CurrentSelection = 0;
                            }

                            // Select the current selection.
                            var newSelection = Selections[CurrentSelection];
                            newSelection.IsSelected = true;
                            Selections[CurrentSelection] = newSelection;

                            
                            this.ParentView.Update();
                            break;
                        }

                    // Go to the previous view (if any)
                    case ConsoleKey.LeftArrow:
                        {
                            this.ParentView.GoBack();
                            break;
                        }

                    // Invoke the selections function.
                    case ConsoleKey.RightArrow:
                    case ConsoleKey.Enter:
                    case ConsoleKey.Spacebar:
                        {
                            Selections.ToArray()[CurrentSelection].MethodToInvoke.Invoke();
                            break;
                        }

                }
            }

        }

    }


    /// <summary>
    ///     A possible selection.
    /// </summary>
    public struct RadioButtonSelection
    {
        /// <summary>
        ///     The text to display to the user.
        /// </summary>
        public string Text;

        /// <summary>
        ///     The method to invoke if this selection is submitted.
        /// </summary>
        public Action MethodToInvoke;

        /// <summary>
        ///     A boolean indicating whether this selection is currently selected or not.
        /// </summary>
        internal bool IsSelected;
    }


}
