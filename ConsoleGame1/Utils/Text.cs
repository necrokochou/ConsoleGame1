using System.Globalization;


namespace ConsoleGame1.Utils;


static class Text {
    public static void Print(string? value = null) {
        Console.Write(value);
    }
    
    public static void PrintLine(string? value = null) {
        Console.WriteLine(value);
    }
    
    public static string Capitalize(string value) {
        return char.ToUpper(value[0]) + value.Substring(1);
    }
    
    public static string Titlecase(string value) {
        return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value);
    }
}
