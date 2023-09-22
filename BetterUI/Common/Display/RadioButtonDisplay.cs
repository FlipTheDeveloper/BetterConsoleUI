using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterConsoleUI.Common.Display
{
    /// <summary>
    ///     Class containing settings for displaying and interacting with the RadioButton input method.
    /// </summary>
    public static class RadioButtonDisplay
    {
        /// <summary>
        ///     The icon displayed when a selection is selected.
        /// </summary>
        public static string Selected = "[X]";

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
