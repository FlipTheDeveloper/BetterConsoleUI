using BetterConsoleUI.Interfaces;
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


        public string Selected = "[X]";

        public string NotSelected = "[ ]";

        public List<Selection> Selections = new List<Selection>();

        public int CurrentSelection = 0;

        public RadioButton(IView parentView)
        {
            ParentView = parentView;
        }

        public bool WrapAround { get; set; } = true;

        public bool HasControl { get; set; } = false;

        public override string ToString()
        {

        }

        public async void Controll(bool hasControll = true)
        {
            // Return Controll.
            await Task.Yield();
            HasControl = hasControll;

            while (HasControl)
            {
                ConsoleKeyInfo keyPress = Console.ReadKey();

                switch (keyPress.Key)
                {
                    // Go to the previous selection.
                    case ConsoleKey.UpArrow:
                        {
                            // Deselect the current selection.
                            Selections.ToArray()[CurrentSelection].IsSelected = false;

                            if (CurrentSelection != 0)
                            {
                                CurrentSelection--;
                            }
                            else if (WrapAround)
                            {
                                CurrentSelection = Selections.Count() - 1;
                            }

                            // Select the current selection.
                            Selections.ToArray()[CurrentSelection].IsSelected = true;

                            this.ParentView.Update();
                            break;
                        }

                    // Go to the next selection.
                    case ConsoleKey.DownArrow:
                        {
                            // Deselect the current selection.
                            Selections.ToArray()[CurrentSelection].IsSelected = false;

                            if (CurrentSelection == Selections.Count() - 1)
                            {
                                CurrentSelection++;
                            }
                            else if (WrapAround)
                            {
                                CurrentSelection = 0;
                            }

                            // Select the current selection.
                            Selections.ToArray()[CurrentSelection].IsSelected = true;

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
