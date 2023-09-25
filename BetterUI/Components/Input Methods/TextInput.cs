using BetterConsoleUI.Common.Settings;
using BetterConsoleUI.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterConsoleUI.Components.Input_Methods
{
    public class TextInput : ISelectionInputMethod
    {
        public IView ParentView { get; set; }

        public List<TextInputSelection> Selections = new List<TextInputSelection>();

        public bool NeedsHelp = false;

        public TextInput(IView parentView, List<TextInputSelection>? selections)
        {
            ParentView = parentView;
            Selections = selections ?? new List<TextInputSelection>();
        }

        /// <inheritdoc/>
        public bool HasControl { get; set; } = false;

        /// <inheritdoc/>
        public void Print()
        {
            if (this.NeedsHelp)
            {
                Console.WriteLine("Available Options:");

                // TODO: Make a new list and append the existing one to it. Add methods to invoke instead of handling them manually. Maybe make use of the Common.Utils namespace.
                if (TextInputSettings.ReservedKeywords)
                {
                    Console.WriteLine("help");
                    Console.WriteLine("back");
                }
                
                foreach (var s in Selections)
                {
                    Console.WriteLine($"  {s.Text}");
                }
            }
        }

        public void Control()
        {
            while (this.HasControl)
            {
                var response = Console.ReadLine();
                var selections = Selections.ToList();
                var matches = new List<TextInputSelection>();

                if (response == null || response == string.Empty)
                {
                    continue;
                }

                if (TextInputSettings.CaseInsensitive == true)
                {
                    response = response!.ToUpper();
                    selections.ForEach(s => s.Text.ToUpper());
                }

                if (TextInputSettings.AllowFuzzySelectionMatching)
                {
                    foreach (var s in selections)
                    {
                        // Do you contain all of the values within the search?
                        bool isMatch = response!.Where(c => s.Text.Contains(c)).Count() == response.Count();

                        if (isMatch)
                        {
                            matches.Add(s);
                        }
                    }
                }

                if (matches.Count == 0 || response.ToLower() == "help")
                {
                    this.NeedsHelp = true;
                    this.ParentView.Update();
                    continue;
                }

                if (response.ToLower() == "back")
                {
                    if (this.ParentView.PreviousView == null)
                    {
                        continue;
                    }

                    this.ParentView.PreviousView.Display(this.ParentView.PreviousView.PreviousView, this.ParentView);
                }


                if (TextInputSettings.AllowMultipleMatches)
                {
                    matches.ForEach(m => m.MethodToInvoke.Invoke());
                }
                else
                {
                    matches.First().MethodToInvoke.Invoke();
                }
            }

        }

    }

    public struct TextInputSelection
    {
        public string Text;
        public Action MethodToInvoke;
    }


}
