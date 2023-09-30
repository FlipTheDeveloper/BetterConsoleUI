using BetterConsoleUI.Common.Interfaces;
using BetterConsoleUI.Components.Input_Methods;

namespace BetterConsoleUI.Components.Views
{
    public class NumberSelectView : View
    {
        public Action<int>? MethodToInvoke
        {
            get
            {
                var input = this.Input as NumberSelect;

                if (input != null)
                {
                    return input.MethodToInvoke;
                }

                return null;
            }

            set
            {
                var input = this.Input as NumberSelect;

                if (input != null && value != null)
                {
                    input.MethodToInvoke = value;
                }
            }
        }

        public NumberSelectView(string? header, Action<int>? methodToInvoke = null, int minNumber = 0, int maxNumber = 100, IView? previousView = null)
        {
            this.Header = header;
            this.MethodToInvoke = methodToInvoke ?? ((int _) => { this.Update(); });
            this.Input = new NumberSelect(this, methodToInvoke, minNumber, maxNumber);
            this.PreviousView = previousView;
        }
    }
}
