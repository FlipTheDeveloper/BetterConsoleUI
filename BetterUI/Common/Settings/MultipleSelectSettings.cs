namespace BetterConsoleUI.Common.Settings
{
    /// <summary>
    ///     Class containing settings for displaying and interacting with the MultipleSelect input method.
    /// </summary>
    public static class MultipleSelectSettings
    {
        /// <summary>
        ///     The icon displayed when a selection is selected.
        /// </summary>
        public static string Selected = "[X]";

        /// <summary>
        ///     The colors displayed when a selection is highlighted.
        /// </summary>
        public static (ConsoleColor BackgroundColor, ConsoleColor ForegroundColor) HighlightColors = (ConsoleColor.White, ConsoleColor.Black);

        /// <summary>
        ///     The icon displayed when a selection is not selected.
        /// </summary>
        public static string NotSelected = "[ ]";

        /// <summary>
        ///     Boolean representing whether the input will jump to the beginning if we reach the end and attempt to go to the next input.
        /// </summary>
        public static bool WrapAround = true;
    }
}
