using ConsoleGame1.Core;


namespace ConsoleGame1.Utils;


static class UserInterface {
    private static string lastInput = "";
    
    public static void DisplayAsChoices(List<ICommand> choices) {
        const int maxColumns = 2;
        
        for (int i = 0; i < choices.Count ; i++) {
            if (i % maxColumns == 0) {
                Spacer.Y();
            } else {
                Spacer.X(5);
            }
            
            Console.Write($"[{Text.Titlecase(choices[i].Name)}]");
        }
        
        Spacer.Y();
    }
    
    public static string GetInput(string prompt) {
        Console.Write($"{prompt} > ");
        
        while (true) {
            var input = Console.ReadLine();
            if (input != null) return input;
        }
    }
}
