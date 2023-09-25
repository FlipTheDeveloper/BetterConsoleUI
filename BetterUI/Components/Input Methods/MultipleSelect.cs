using BetterConsoleUI.Common.Settings;
using BetterConsoleUI.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterConsoleUI.Components.Input_Methods
{
    public class MultipleSelect: ISelectionInputMethod
    {
        public IView ParentView { get; set; }

        public List<MultipleSelectSelection> Selections = new List<MultipleSelectSelection>();

        public int CurrentSelection = 0;

        public MultipleSelect(IView parentView, List<MultipleSelectSelection>? selections)
        {
            ParentView = parentView;
            Selections = selections ?? new List<MultipleSelectSelection>();
        }

        /// <inheritdoc/>
        public bool HasControl { get; set; } = false;

        /// <inheritdoc/>
        public override string ToString()
        {
            string result = string.Empty;
            if (Selections.Where(s => s.IsSelected).Count() == 0)
            {
                var firstSelection = Selections[0];
                firstSelection.IsSelected = true;
                Selections[0] = firstSelection;
            }

            //int paddingX = (Console.WindowWidth - this.Selections.Select(s => s.Text.Length + ((RadioButtonDisplay.Selected.Length + RadioButtonDisplay.NotSelected.Length + 1) / 2)).Max()) / 2;
            foreach (MultipleSelectSelection s in Selections)
            {
                result +=  $"{(s.IsSelected ? RadioButtonSettings.Selected : RadioButtonSettings.NotSelected)} {s.Text}\n";
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
                            // Unhighlight the current selection.
                            var oldSelection = Selections[CurrentSelection];
                            oldSelection.IsHighlighted = false;
                            Selections[CurrentSelection] = oldSelection;

                            if (CurrentSelection != 0)
                            {
                                CurrentSelection--;
                            }
                            else if (RadioButtonSettings.WrapAround)
                            {
                                CurrentSelection = Selections.Count() - 1;
                            }

                            // Highlight the current selection.s
                            var newSelection = Selections[CurrentSelection];
                            newSelection.IsHighlighted = true;
                            Selections[CurrentSelection] = newSelection;

                            this.ParentView.Update();
                            break;
                        }

                    // Go to the next selection.
                    case ConsoleKey.DownArrow:
                        {
                            // Deselect the current selection.
                            var oldSelection = Selections[CurrentSelection];
                            oldSelection.IsHighlighted = false;
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
                            newSelection.IsHighlighted = true;
                            Selections[CurrentSelection] = newSelection;

                            
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
                        {
                            var selection = Selections[CurrentSelection];
                            selection.IsSelected = !selection.IsSelected;
                            Selections[CurrentSelection] = selection;
                            break;
                        }

                    case ConsoleKey.Enter:
                        {
                            Selections.ForEach(s => s.MethodToInvoke.Invoke());
                            break;
                        }

                }
            }

        }

    }

    public struct MultipleSelectSelection
    {
        public string Text;
        public Action MethodToInvoke;
        internal bool IsSelected;
        internal bool IsHighlighted;
    }


}
