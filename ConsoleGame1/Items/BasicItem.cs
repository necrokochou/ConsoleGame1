using ConsoleGame1.Core;
using ConsoleGame1.Interfaces;


namespace ConsoleGame1.Items;


class BasicItem : Item {
    public BasicItem(string name, ITarget? target = null) : base(name, target) { }
}
