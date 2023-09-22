using BetterConsoleUI.Common.Display;
using BetterConsoleUI.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterConsoleUI.Components.Input_Methods
{
    public class RadioButton : ISelectionInputMethod
    {
        public IView ParentView { get; set; }

        public List<Selection> Selections = new List<Selection>();

        public int CurrentSelection = 0;

        public RadioButton(IView parentView, List<Selection> selections)
        {
            ParentView = parentView;
            Selections = selections;
        }

        /// <inheritdoc/>
        public bool HasControl { get; set; } = false;

        /// <inheritdoc/>
        public override string ToString()
        {
            string result = string.Empty;

            int paddingX = (Console.WindowWidth - this.Selections.Select(s => s.Text.Length + ((RadioButtonDisplay.Selected.Length + RadioButtonDisplay.NotSelected.Length + 1) / 2)).Max()) / 2;
            foreach (Selection s in Selections)
            {
                result += $"{(s.IsSelected ? RadioButtonDisplay.Selected : RadioButtonDisplay.NotSelected)} {s.Text}\n".PadLeft(paddingX).PadRight(paddingX);
            }

            return result;
        }

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
                            var oldSelection = Selections.ToArray()[CurrentSelection];
                            oldSelection.IsSelected = false;

                            if (CurrentSelection != 0)
                            {
                                CurrentSelection--;
                            }
                            else if (RadioButtonDisplay.WrapAround)
                            {
                                CurrentSelection = Selections.Count() - 1;
                            }

                            // Select the current selection.
                            var newSelection = Selections.ToArray()[CurrentSelection];
                            newSelection.IsSelected = true;

                            this.ParentView.Update();
                            break;
                        }

                    // Go to the next selection.
                    case ConsoleKey.DownArrow:
                        {
                            // Deselect the current selection.
                            var oldSelection = Selections.ToArray()[CurrentSelection];
                            oldSelection.IsSelected = false;

                            if (CurrentSelection != Selections.Count() - 1)
                            {
                                CurrentSelection++;
                            }
                            else if (RadioButtonDisplay.WrapAround)
                            {
                                CurrentSelection = 0;
                            }

                            // Select the current selection.
                            var newSelection = Selections.ToArray()[CurrentSelection];
                            newSelection.IsSelected = true;

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
                                this.ParentView.Display(this.ParentView.PreviousView.PreviousView, this.ParentView);
                            }

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
                Common.Utils.Utilities.WaitForKeyUp();
            }

        }

    }

    public enum UserInput
    {
        Up,
        Down,
        Left,
        Right,
        Enter,
        Space,
    }

    public struct Selection
    {
        public string Text;
        public Func<bool?> MethodToInvoke;
        public bool IsSelected;
    }


}
