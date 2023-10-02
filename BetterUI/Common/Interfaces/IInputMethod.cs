namespace BetterConsoleUI.Common.Interfaces
{
    public interface IInputMethod
    {
        /// <summary>
        ///     The view containing the input method.
        /// </summary>
        public IView ParentView { get; set; }

        /// <summary>
        ///     A bool indicating whether or not the InputMethod has controll.
        /// </summary>
        public bool HasControl { get; set; }

        /// <summary>
        ///     A method to provide control to the input method.
        /// </summary>
        public void Control();

        /// <summary>
        ///     Writes the selection method to the console.
        /// </summary>
        public void Print();
    }
}