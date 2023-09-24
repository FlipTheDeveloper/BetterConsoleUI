using BetterConsoleUI.Components.Input_Methods;
using BetterConsoleUI.Components.Views;

internal class Program
{
    private static void Main(string[] args)
    {
        RadioButtonView firstView = new RadioButtonView("Please select the view you want to see!");
        RadioButtonView rbView = new RadioButtonView("A RadioButton View!");

        var firstViewSelections =
            new List<Selection>
                {
                    new Selection() { Text = "Next RadioButtonView", MethodToInvoke = () => { rbView.Display(firstView); } },
                    new Selection() { Text = "Hello", MethodToInvoke = () => { Console.WriteLine("You chose : 'Hello'"); } },
                    new Selection() { Text = "How", MethodToInvoke = () => { Console.WriteLine("You chose : 'How'"); } },
                    new Selection() { Text = "Are", MethodToInvoke = () => { Console.WriteLine("You chose : 'Are'"); } },
                    new Selection() { Text = "You", MethodToInvoke = () => { Console.WriteLine("You chose : 'You'"); } },
                };

        var rbViewSelections =
            new List<Selection>
            {
                new Selection() {Text = "You made it!", MethodToInvoke= () => { Console.WriteLine("Press the left_arrow to navigate back a page!"); }}
            };
        
        firstView = new RadioButtonView(firstView.Header, firstViewSelections);
        rbView = new RadioButtonView(rbView.Header, rbViewSelections, firstView);
        
        firstView.Display();

    }
}