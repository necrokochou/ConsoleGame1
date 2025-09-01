using System.Globalization;


namespace ConsoleGame1.Utils;


static class Util {
    public static string Capitalize(string text) {
        return char.ToUpper(text[0]) + text.Substring(1);
    }
    public static string Titlecase(string text) {
        return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text);
    }
}

static class Div {
    public static void X(int spaces = 1) {
        for (int i = 0; i < spaces; i++) {
            Console.Write(' ');
        }
    }

    public static void Y(int spaces = 1) {
        for (int i = 0; i < spaces; i++) {
            Console.WriteLine();
        }
    }
}
