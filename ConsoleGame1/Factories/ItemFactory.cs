using ConsoleGame1.Items;


namespace ConsoleGame1.Factories;


static class ItemFactory {
    public static BasicItem Create(string name, bool needsTarget = false) {
        return new BasicItem(name, needsTarget);
    }
}
