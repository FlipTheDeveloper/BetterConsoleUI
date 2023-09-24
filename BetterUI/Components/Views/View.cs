using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetterConsoleUI.Common.Interfaces;

namespace BetterConsoleUI.Components.Views
{
    /// <summary>
    ///     A generic view, 
    /// </summary>
    public class View : IView
    {
        /// <inheritdoc/>
        public IView? PreviousView { get; set; }

        /// <inheritdoc/>
        public string? Header { get; set; }

        /// <inheritdoc/>
        public IInputMethod? Input { get; set; }

        /// <inheritdoc/>
        public void Display(IView? previousView = null, IView? sender = null)
        {
            PreviousView = previousView;

            // Make the previous view lose control.
            if (PreviousView != null && PreviousView.Input != null)
            {
                PreviousView.Input.HasControl = false;
            }

            // If this view is the previous view, and we returned to it. Make the latter view lose control.
            if (sender != null && sender.Input != null)
            {
                sender.Input.HasControl = false;
            }

            // Give control to this input.
            if (Input != null)
            {
                this.Input.HasControl = true;
                this.Update();
                this.Input.Control();
            }

        }

        /// <inheritdoc/>
        public void Update()
        {
            // If something calls to update this view, but this view's input
            // is not null and doesn't have control. It is probably a mistake.

            if (this.Input == null || this.Input.HasControl != false)
            {
                Console.Clear();
                Console.WriteLine(this.Header ?? string.Empty);

                if (this.Input != null)
                {
                    Console.WriteLine(this.Input);
                }
            }
            else
            {
                throw new InvalidOperationException("Tried to update a view with an input that doesn't have control.");
            }
        }
    }
}
