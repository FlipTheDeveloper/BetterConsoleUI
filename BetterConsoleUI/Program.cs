using BetterConsoleUI.Components.Input_Methods;
using BetterConsoleUI.Components.Views;
using System.Runtime.CompilerServices;

internal class Program
{
    private static void Main(string[] args)
    {
        // Define Views
        RadioButtonView firstView = new RadioButtonView("Please select the view you want to see!");
        RadioButtonView rbView = new RadioButtonView("A RadioButton View!");
        MultipleSelectView msView = new MultipleSelectView("A Multiple Select View.");
        TextInputView tiView = new TextInputView("Foo or Bar?");

        // Define Selections
        var firstViewSelections =
            new List<RadioButtonSelection>
                {
                    new RadioButtonSelection() { Text = "RadioButtonView", MethodToInvoke = () => { firstView.SwitchTo(rbView); } },
                    new RadioButtonSelection() { Text = "MultipleSelectView", MethodToInvoke = () => { firstView.SwitchTo(msView); } },
                    new RadioButtonSelection() { Text = "TextInputSelection", MethodToInvoke = () => { firstView.SwitchTo(tiView); } },
                    new RadioButtonSelection() { Text = "Exit", MethodToInvoke = () => { firstView.RevokeControl();  } },
                };

        var rbViewSelections =
            new List<RadioButtonSelection>
            {
                new RadioButtonSelection() {Text = "You made it!", MethodToInvoke= () => { Console.WriteLine("Press the left_arrow to navigate back a page!"); }}
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
            new List<TextInputSelection>
            {
                new TextInputSelection() {Text = "Foo", MethodToInvoke = () => { Console.Clear(); Console.WriteLine("Foo is good");}},
                new TextInputSelection() {Text = "Bar", MethodToInvoke = () => { Console.Clear(); Console.WriteLine("Bar is better");}},
                new TextInputSelection() {Text = "Baz", MethodToInvoke = () => { Console.Clear(); Console.WriteLine("Baz is best");}},
            };

        // Assign selections to views.
        firstView = new RadioButtonView(firstView.Header, firstViewSelections);
        rbView = new RadioButtonView(rbView.Header, rbViewSelections);
        msView = new MultipleSelectView(msView.Header, msViewSelections);
        tiView = new TextInputView(tiView.Header, tiViewSelections);

        firstView.Display();

    }
}