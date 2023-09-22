

namespace BetterConsoleUI.Common.Utils
{
    internal static class Utilities
    {
        public static void WaitForKeyUp()
        {
            while (!Console.KeyAvailable)
            {

            }
        }
    }
}
