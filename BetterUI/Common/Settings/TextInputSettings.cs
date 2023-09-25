namespace BetterConsoleUI.Common.Settings
{
    /// <summary>
    ///     Class containing settings for displaying and interacting with the TextInput input method.
    /// </summary>
    public static class TextInputSettings
    {
        /// <summary>
        ///     Searches for selections that contain all of the charicters provided. For instance 'YP' would match 'Yes Please!'.
        /// </summary>
        public static bool AllowFuzzySelectionMatching = true;

        /// <summary>
        ///     Allows for case insensitive matching to selections. For instance 'yEs PleAse!' would match 'Yes Please!'.
        /// </summary>
        public static bool CaseInsensitive = true;

        /// <summary>
        ///     Allows for multiple matches to be made from a single response.
        /// </summary>
        public static bool AllowMultipleMatches = false;

        /// <summary>
        ///     Enables reserved key words and their functionality. This enables keywords like 'help' and 'back'.
        /// </summary>
        public static bool ReservedKeywords = true;
    }
}
