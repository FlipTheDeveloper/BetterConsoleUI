using BetterConsoleUI.Components.Views;

namespace BetterConsoleUI.Common.Interfaces
{
    public interface IView
    {
        /// <summary>
        ///     The previous view (if any).
        /// </summary>
        public IView? PreviousView { get; }

        /// <summary>
        ///     The header or prompt of the view.
        /// </summary>
        public string? Header { get; }

        /// <summary>
        ///     The input of the view.
        /// </summary>
        public IInputMethod? Input { get; }

        /// <summary>
        ///     The method used to switch to this view.
        /// </summary>
        public abstract void Display(IView? previousView, IView? sendingView = null);

        /// <summary>
        ///     Method used to switch to the previous display.
        /// </summary>
        public void GoBack();

        /// <summary>
        ///     Switches the view to the <paramref name="view"/> specified, 
        ///     and removes the controlls from the previous view. Additionally 
        ///     sets the view's <see cref="IView.PreviousView""/> to be this view.
        /// </summary>
        /// <param name="view">The view to switch to.</param>
        public void SwitchTo(View view);

        /// <summary>
        ///     Method used to update the view.
        /// </summary>
        public abstract void Update();

        /// <summary>
        ///     Method used to revoke control from this view's <see cref="Input"/>
        /// </summary>
        public void RevokeControl();
    }
}