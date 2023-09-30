using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            if (PreviousView != null)
            {
                PreviousView.RevokeControl();
            }

            // If this view is the previous view, and we returned to it. Make the latter view lose control.
            if (sender != null)
            {
                sender.RevokeControl();
            }

            // Give control to this input.
            if (Input != null)
            {
                this.Input.HasControl = true;
                Console.Clear();
                this.Update();
                this.Input.Control();
            }

        }

        /// <inheritdoc/>
        public void SwitchTo(View view)
        {
            view.Display(this);
            view.PreviousView = this;
        }

        /// <inheritdoc/>
        public virtual void Update()
        {
            // If something calls to update this view, but this view's input
            // is not null and doesn't have control. It is probably a mistake.

            if (this.Input == null || this.Input.HasControl != false)
            {
                Console.SetCursorPosition(0, 0);
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

        public void RevokeControl()
        {
            if (this.Input != null)
            {
                this.Input.HasControl = false;
            }
        }
    }
}
