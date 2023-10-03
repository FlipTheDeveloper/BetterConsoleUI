using BetterConsoleUI.Components.Input_Methods;
using BetterConsoleUI.Components.Views;
using BetterConsoleUIDemo;

internal class Program
{
    private static void Main(string[] args)
    {
        // Define Views
        RadioButtonView firstView = new RadioButtonView("Please select the view you want to see!");
        RadioButtonView rbView = new RadioButtonView("A RadioButton View!");
        MultipleSelectView msView = new MultipleSelectView("A Multiple Select View.");
        TextSelectView tsView = new TextSelectView("Foo or Bar?");
        NumberInputView nsView = new NumberInputView("Whats your favorite number?");
        TextInputView tiView = new TextInputView("Type in your name.");
        InfoView infoView = new InfoView("You're a cool guy! \n Press any key to continue...");

        // Define Selections
        var firstViewSelections =
            new List<RadioButtonSelection>
                {
                    new RadioButtonSelection() { Text = "RadioButtonView", MethodToInvoke = () => { firstView.SwitchTo(rbView); } },
                    new RadioButtonSelection() { Text = "MultipleSelectView", MethodToInvoke = () => { firstView.SwitchTo(msView); } },
                    new RadioButtonSelection() { Text = "TextSelectSelection", MethodToInvoke = () => { firstView.SwitchTo(tsView); } },
                    new RadioButtonSelection() { Text = "TextInputSelection", MethodToInvoke = () => { firstView.SwitchTo(tiView); } },
                    new RadioButtonSelection() { Text = "NumberSelection", MethodToInvoke = () => { firstView.SwitchTo(nsView); } },
                    new RadioButtonSelection() { Text = "InfoView", MethodToInvoke = () => { firstView.SwitchTo(infoView); } },
                    new RadioButtonSelection() { Text = "Exit", MethodToInvoke = () => { firstView.RevokeControl();  } },
                };

        var rbViewSelections =
            new List<RadioButtonSelection>
            {
                new RadioButtonSelection() {Text = "You made it!", MethodToInvoke= () => { Console.WriteLine("Press the left_arrow to navigate back a page!"); }},
                new RadioButtonSelection() {Text = "Use arrow keys to navigate up and down!", MethodToInvoke= () => { Console.WriteLine("Press the left_arrow to navigate back a page!"); }},
                new RadioButtonSelection() {Text = "Use spacebar, the right arrow key, or the spacebar to select and option!", MethodToInvoke= () => { Console.WriteLine("Press the left_arrow to navigate back a page!"); }},
                new RadioButtonSelection() {Text = "Use the back arrow to return to main menu!", MethodToInvoke= () => { Console.WriteLine("Press the left_arrow to navigate back a page!"); }},
            };

        var msViewSelections =
            new List<MultipleSelectSelection>
            {
                new MultipleSelectSelection() {Text = "First Selection", MethodToInvoke = () => { Console.WriteLine("1"); } },
                new MultipleSelectSelection() {Text = "Second Selection", MethodToInvoke = () => { Console.WriteLine("2"); } },
                new MultipleSelectSelection() {Text = "Third Selection", MethodToInvoke = () => { Console.WriteLine("3"); } },
                new MultipleSelectSelection() {Text = "Fourth Selection", MethodToInvoke = () => { Console.WriteLine("4"); } },
                new MultipleSelectSelection() {Text = "Fifth Selection", MethodToInvoke = () => { Console.WriteLine("5"); } },
            };

        var tiViewSelections =
            new List<TextSelectSelection>
            {
                new TextSelectSelection() {Text = "Foo", MethodToInvoke = () => { Console.Clear(); Console.WriteLine("Foo is good");}},
                new TextSelectSelection() {Text = "Bar", MethodToInvoke = () => { Console.Clear(); Console.WriteLine("Bar is better");}},
                new TextSelectSelection() {Text = "Baz", MethodToInvoke = () => { Console.Clear(); Console.WriteLine("Baz is best");}},
                new TextSelectSelection() {Text = "1", MethodToInvoke = () => { Console.Clear(); Console.WriteLine("1 is good");}},
                new TextSelectSelection() {Text = "11", MethodToInvoke = () => { Console.Clear(); Console.WriteLine("11 is best");}},
            };

        // Assign selections to views.
        firstView.Selections = firstViewSelections;
        rbView.Selections = rbViewSelections;
        msView.Selections = msViewSelections;
        tsView.Selections = tiViewSelections;
        nsView.MethodToInvoke = (int x) => { Controllers.GuessNumber(x, nsView); };
        tiView.MethodToInvoke = (string s) => { Controllers.LikesToProgram(s); };
        infoView.MethodToInvoke = () => { infoView.SwitchTo(firstView); };

        firstView.Display();
    }
}