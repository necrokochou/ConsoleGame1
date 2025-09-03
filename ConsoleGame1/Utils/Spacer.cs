namespace ConsoleGame1.Utils;


static class Spacer {
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
