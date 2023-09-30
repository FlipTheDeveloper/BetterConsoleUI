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

        private List<TextInputSelection> selections = new List<TextInputSelection>();

        public List<TextInputSelection> Selections
        {
            get 
            { 
                return selections; 
            }
            
            set 
            { 
                selections = value;

                if (TextInputSettings.ReservedKeywords)
                {
                    Selections.Add(new TextInputSelection() { Text = "help", MethodToInvoke = this.Help });
                    Selections.Add(new TextInputSelection() { Text = "back", MethodToInvoke = this.Back });
                }
            }
        }

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
                    response = response!.ToLower();
                    selections = selections.Select(s => new TextInputSelection() { Text = s.Text.ToLower(), MethodToInvoke = s.MethodToInvoke }).ToList();
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

                if (matches.Count == 0)
                {
                    this.ParentView.Update();
                    this.Help();
                    continue;
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

        public void Help()
        {
            this.ParentView.Update();
            Console.WriteLine("Available Options:");

            foreach (var s in Selections)
            {
                Console.WriteLine($"  {s.Text}");
            }
        }

        public void Back()
        {
            if (this.ParentView.PreviousView != null)
            {
                this.ParentView.PreviousView.Display(this.ParentView.PreviousView.PreviousView, this.ParentView);
            }
        }

    }



    public struct TextInputSelection
    {
        public string Text;
        public Action MethodToInvoke;
    }


}
