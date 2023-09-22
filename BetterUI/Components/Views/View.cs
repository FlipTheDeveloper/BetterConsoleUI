using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetterConsoleUI.Interfaces;

namespace BetterConsoleUI.Components.Views
{
    public class View : IView
    {
        /// <inheritdoc/>
        public IView? PreviousView { get; set; }

        /// <inheritdoc/>
        public string? Header { get; set; }

        /// <inheritdoc/>
        public IInputMethod? Input { get; set; }

        /// <inheritdoc/>
        public void Display(IView? previousView, IView? sender = null)
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
            }
        }

        /// <inheritdoc/>
        public void Update()
        {

        }
    }
}
