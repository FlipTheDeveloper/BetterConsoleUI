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
        ///     Method used to update the view.
        /// </summary>
        public abstract void Update();
    }
}