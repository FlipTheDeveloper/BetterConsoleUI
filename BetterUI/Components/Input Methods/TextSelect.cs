using BetterConsoleUI.Common.Interfaces;
using BetterConsoleUI.Common.Settings;

namespace BetterConsoleUI.Components.Input_Methods
{
    public class TextSelect : IInputMethod
    {
        public IView ParentView { get; set; }

        private List<TextSelectSelection> selections = new List<TextSelectSelection>();

        public List<TextSelectSelection> Selections
        {
            get 
            { 
                return selections; 
            }
            
            set 
            { 
                selections = value;

                if (TextSelectSettings.ReservedKeywords)
                {
                    Selections.Add(new TextSelectSelection() { Text = "help", MethodToInvoke = this.Help });
                    Selections.Add(new TextSelectSelection() { Text = "back", MethodToInvoke = this.Back });
                }
            }
        }

        public TextSelect(IView parentView, List<TextSelectSelection>? selections)
        {
            ParentView = parentView;
            Selections = selections ?? new List<TextSelectSelection>();
        }

        /// <inheritdoc/>
        public bool HasControl { get; set; } = false;

        /// <inheritdoc/>
        public void Print()
        {
            if (TextSelectSettings.ShowAvailableOptions) 
            {
                Console.WriteLine("Available Options:");

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
                var matches = new List<TextSelectSelection>();

                if (response == null || response == string.Empty)
                {
                    continue;
                }

                if (TextSelectSettings.CaseInsensitive == true)
                {
                    response = response!.ToLower();
                    selections = selections.Select(s => new TextSelectSelection() { Text = s.Text.ToLower(), MethodToInvoke = s.MethodToInvoke }).ToList();
                }

                if (TextSelectSettings.AllowFuzzySelectionMatching)
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

                if (TextSelectSettings.AllowMultipleMatches)
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

            if (!TextSelectSettings.ShowAvailableOptions)
            {
                Console.WriteLine("Available Options:");

                foreach (var s in Selections)
                {
                    Console.WriteLine($"  {s.Text}");
                }
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



    public struct TextSelectSelection
    {
        public string Text;
        public Action MethodToInvoke;
    }


}
