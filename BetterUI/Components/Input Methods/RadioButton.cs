using BetterConsoleUI.Common.Interfaces;
using BetterConsoleUI.Common.Settings;

namespace BetterConsoleUI.Components.Input_Methods
{
    public class RadioButton : ISelectionInputMethod
    {
        public IView ParentView { get; set; }

        public List<RadioButtonSelection> Selections = new List<RadioButtonSelection>();

        public int CurrentSelection = 0;

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

    public struct RadioButtonSelection
    {
        public string Text;
        public Action MethodToInvoke;
        internal bool IsSelected;
    }


}
