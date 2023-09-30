using BetterConsoleUI.Common.Interfaces;

namespace BetterConsoleUIDemo
{
    internal static class Controllers
    {

        public static void GuessNumber(int guessedNumber, IView view)
        {
            Random random= new Random();
            int targetNumber = random.Next(1, 100);
            int delta = targetNumber - guessedNumber;
            delta = delta < 0 ? delta * -1 : delta;
            string text = $"\nYou guessed the number {guessedNumber}, and I was thinking about the number {targetNumber}, {(delta == 0 ? "Good Job! you guessed the number!" : $"You were {delta} numbers off.")}";
            Console.WriteLine(text);
            Console.ReadKey();
            Console.Clear();
            view.Update();
        }
    }
}
