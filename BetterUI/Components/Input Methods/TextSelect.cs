using BetterConsoleUI.Common.Interfaces;
using BetterConsoleUI.Common.Settings;

namespace BetterConsoleUI.Components.Input_Methods
{
    /// <summary>
    ///     A text selection component.
    /// </summary>
    public class TextSelect : IInputMethod
    {
        /// <inheritdoc/>
        public IView ParentView { get; set; }

        private List<TextSelectSelection> selections = new List<TextSelectSelection>();

        /// <summary>
        ///     A list of possible selections.
        /// </summary>
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

        /// <summary>
        ///     Constructor for the <see cref="TextSelect"/> class.
        /// </summary>
        /// <param name="parentView">The view containing this input method.</param>
        /// <param name="selections">Possible selections and their <see cref="TextSelectSelection.MethodToInvoke"/>.</param>
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
        
        /// <inheritdoc/>
        public void Control()
        {
            while (this.HasControl)
            {
                var response = Console.ReadLine();
                var selections = Selections.ToList();
                var matches = new List<TextSelectSelection>();

                if (response == null || response == string.Empty)
                {
                    this.ParentView.Update();
                    continue;
                }

                if (TextSelectSettings.CaseInsensitive == true)
                {
                    response = response!.ToLower();
                    selections = selections.Select(s => new TextSelectSelection() { Text = s.Text.ToLower(), MethodToInvoke = s.MethodToInvoke }).ToList();
                }

                // Find exact matches first.
                matches.AddRange(selections.Where(s => s.Text == response));

                // Then find fuzzy matches.
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

        /// <summary>
        ///     A method to display possible options within the console.
        /// </summary>
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

        /// <summary>
        ///     A method to return to the previous view.
        /// </summary>
        public void Back()
        {
            if (this.ParentView.PreviousView != null)
            {
                this.ParentView.PreviousView.Display(this.ParentView.PreviousView.PreviousView, this.ParentView);
            }
        }
    }

    /// <summary>
    ///     A possible selection.
    /// </summary>
    public struct TextSelectSelection
    {
        /// <summary>
        ///     The text to match against the keyword.
        /// </summary>
        public string Text;

        /// <summary>
        ///     The method to invoke if the keyword matches.
        /// </summary>
        public Action MethodToInvoke;
    }


}
