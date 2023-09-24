using BetterConsoleUI.Common.Interfaces;
using BetterConsoleUI.Components.Input_Methods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterConsoleUI.Components.Views
{
    public class RadioButtonView : View
    {
        public RadioButtonView(string? header, List<Selection>? selections = null, IView? previousView = null)
        {
            this.Header = header;
            this.Input = new RadioButton(this, selections);
            this.PreviousView = previousView;
        }
    }
}
