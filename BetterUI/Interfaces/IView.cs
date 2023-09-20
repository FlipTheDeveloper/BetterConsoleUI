namespace BetterConsoleUI.Interfaces
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
        public string Text { get; }

        /// <summary>
        ///     The input of the view.
        /// </summary>
        public IInput Input { get; }

        /// <summary>
        ///     The method used to display the view.
        /// </summary>
        public void Display() { }
        
    }
}