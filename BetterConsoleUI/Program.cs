using BetterConsoleUI.Components.Input_Methods;
using BetterConsoleUI.Components.Views;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        View view = new View();
        view.Header = "Some cool new Header!";
        view.Input = new RadioButton(view, 
            new List<Selection> 
                { 
                    new Selection() { IsSelected = true, Text = "Hello", MethodToInvoke = () => { return null; } },
                    new Selection() { IsSelected = true, Text = "How", MethodToInvoke = () => { return null; } },
                    new Selection() { IsSelected = true, Text = "Are", MethodToInvoke = () => { return null; } },
                    new Selection() { IsSelected = true, Text = "You", MethodToInvoke = () => { return null; } },
                }
            );

        view.Display(null);

    }
}