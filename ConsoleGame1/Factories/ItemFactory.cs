using ConsoleGame1.Interfaces;
using ConsoleGame1.Items;


namespace ConsoleGame1.Factories;


static class ItemFactory {
    public static BasicItem Create(string name, ITarget? target = null) {
        return new BasicItem(name, target);
    }
}
