using ConsoleGame1.Core;
using ConsoleGame1.Interfaces;


namespace ConsoleGame1.Utils;


static class UserInterface {
    private static string lastInput = "";
    
    public static void DisplayAsChoices<T>(List<T> choices, int maxColumns = 2) where T : INameable {
        if (choices.Count == 0) return;
        
        for (var i = 0; i < choices.Count ; i++) {
            if (i % maxColumns == 0) {
                Spacer.Y();
            } else {
                Spacer.X(5);
            }
            
            Console.Write($"[{String.ToTitleCase(choices[i].Name)}]");
        }
        Spacer.Y();
    }
    
    public static string GetInput(string prompt, bool toLower = true) {
        
        while (true) {
            Console.Write($"{prompt} > ");
            var input = Console.ReadLine() ?? "default";
            if (input is "default" or "\n" or "") {
                Console.WriteLine("Empty input");
                continue;
            } 
            
            return toLower ? input.ToLower() : input;
        }
    }
}
