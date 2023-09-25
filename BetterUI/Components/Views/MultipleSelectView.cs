using BetterConsoleUI.Common.Interfaces;
using BetterConsoleUI.Components.Input_Methods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterConsoleUI.Components.Views
{
    public class MultipleSelectView : View
    {
        public MultipleSelectView(string? header, List<MultipleSelectSelection>? selections = null, IView? previousView = null)
        {
            this.Header = header;
            this.Input = new MultipleSelect(this, selections);
            this.PreviousView = previousView;
        }
    }
}
