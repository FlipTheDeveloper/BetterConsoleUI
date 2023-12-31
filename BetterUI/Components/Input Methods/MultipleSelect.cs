﻿using BetterConsoleUI.Common.Interfaces;
using BetterConsoleUI.Common.Settings;

namespace BetterConsoleUI.Components.Input_Methods
{
    /// <summary>
    ///     A multiple select component. Allowing the user to make several
    ///     selections at once. And invoking all of the <see cref="MultipleSelectSelection.MethodToInvoke"/>.
    /// </summary>
    public class MultipleSelect : IInputMethod
    {
        /// <inheritdoc/>
        public IView ParentView { get; set; }

        /// <summary>
        ///     A list of possible selections.
        /// </summary>
        public List<MultipleSelectSelection> Selections = new List<MultipleSelectSelection>();

        /// <summary>
        ///     The current selection within the <see cref="Selections"/>
        /// </summary>
        public int CurrentSelection = 0;

        /// <summary>
        ///     Constructor for the <see cref="MultipleSelect"/> component. Allowing users to make several selections at once using the arrowkeys and the spacebar.
        /// </summary>
        /// <param name="parentView">The view containing this component.</param>
        /// <param name="selections">A list of possible selections.</param>
        public MultipleSelect(IView parentView, List<MultipleSelectSelection>? selections)
        {
            ParentView = parentView;
            Selections = selections ?? new List<MultipleSelectSelection>();
        }

        /// <inheritdoc/>
        public bool HasControl { get; set; } = false;

        /// <inheritdoc/>
        public void Print()
        {
            if (Selections.Where(s => s.IsHighlighted).Count() == 0)
            {
                var firstSelection = Selections[0];
                firstSelection.IsHighlighted = true;
                Selections[0] = firstSelection;
            }

            //int paddingX = (Console.WindowWidth - this.Selections.Select(s => s.Text.Length + ((RadioButtonDisplay.Selected.Length + RadioButtonDisplay.NotSelected.Length + 1) / 2)).Max()) / 2;
            foreach (MultipleSelectSelection s in Selections)
            {
                string result = string.Empty;
                result +=  $"{(s.IsSelected ? MultipleSelectSettings.Selected : MultipleSelectSettings.NotSelected)} {s.Text}";
                if (s.IsHighlighted)
                {
                    Console.BackgroundColor = MultipleSelectSettings.HighlightColors.BackgroundColor;
                    Console.ForegroundColor = MultipleSelectSettings.HighlightColors.ForegroundColor;
                }
                Console.WriteLine(result);
                Console.ResetColor();
            }
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
                            this.ParentView.GoBack();
                            break;
                        }

                    // Invoke the selections function.
                    case ConsoleKey.RightArrow:
                    case ConsoleKey.Spacebar:
                        {
                            var selection = Selections[CurrentSelection];
                            selection.IsSelected = !selection.IsSelected;
                            Selections[CurrentSelection] = selection;
                            this.ParentView.Update();
                            break;
                        }

                    case ConsoleKey.Enter:
                        {
                            Selections.Where(s => s.IsSelected).ToList().ForEach(s => s.MethodToInvoke.Invoke());
                            break;
                        }

                }
            }

        }

    }

    /// <summary>
    ///     A possible selection.
    /// </summary>
    public struct MultipleSelectSelection
    {
        /// <summary>
        ///     The text to display to the user.
        /// </summary>
        public string Text;

        /// <summary>
        ///     The method to invoke if this selection is one of the selections submitted.
        /// </summary>
        public Action MethodToInvoke;

        /// <summary>
        ///     A boolean indicating whether the selection is currently selected.
        /// </summary>
        internal bool IsSelected;

        /// <summary>
        ///     A boolean indicating whether the selection is currently highlighted by the user.
        /// </summary>
        internal bool IsHighlighted;
    }
}
