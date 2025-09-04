using System.Globalization;


namespace ConsoleGame1.Utils;


static class String {
    public static string ToProperCase(string value) {
        return char.ToUpper(value[0]) + value.Substring(1);
    }
    
    public static string ToTitleCase(string value) {
        return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value);
    }
}
