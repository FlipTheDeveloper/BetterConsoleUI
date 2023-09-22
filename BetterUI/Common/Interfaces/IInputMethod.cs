namespace BetterConsoleUI.Common.Interfaces
{
    public interface IInputMethod
    {
        /// <summary>
        ///     A bool indicating whether or not the InputMethod has controll.
        /// </summary>
        public bool HasControl { get; set; }

        /// <summary>
        ///     A method to provide control to the input method.
        /// </summary>
        public void Control();
    }
}