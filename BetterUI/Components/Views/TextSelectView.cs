using BetterConsoleUI.Common.Interfaces;
using BetterConsoleUI.Components.Input_Methods;

namespace BetterConsoleUI.Components.Views
{
    /// <summary>
    ///     A View using the <see cref="TextSelect"/> component.
    /// </summary>
    public class TextSelectView : View
    {
        /// <summary>
        ///     A <see cref="List{TextSelectSelection}"/> describing possible selections and their attached <see cref="Action"/>.
        /// </summary>
        public List<TextSelectSelection>? Selections 
        {
            get
            {
                var input = this.Input as TextSelect;

                if (input != null)
                {
                    return input.Selections;
                }

                return null;
            }

            set
            {
                var input = this.Input as TextSelect;

                if (input != null && value != null)
                {
                    input.Selections = value;
                }
            }
        }

        
        public TextSelectView(string? header, List<TextSelectSelection>? selections = null, IView? previousView = null)
        {
            this.Header = header;
            this.Input = new TextSelect(this, selections);
            this.PreviousView = previousView;
        }

        /// <inheritdoc/>
        public override void Update()
        {
            // If something calls to update this view, but this view's input
            // is not null and doesn't have control. It is probably a mistake.

            if (this.Input == null || this.Input.HasControl != false)
            {
                Console.Clear();
                Console.WriteLine(this.Header ?? string.Empty);

                if (this.Input != null)
                {
                    this.Input.Print();
                }
            }
            else
            {
                throw new InvalidOperationException("Tried to update a view with an input that doesn't have control.");
            }
        }
    }
}
