using ConsoleGame1.Core;
using ConsoleGame1.Interfaces;


namespace ConsoleGame1.Spells;


public class BasicSpell : Spell {
    public BasicSpell(string name, float cost, string costType, int cooldown, ITarget? target) 
        : base(name, cost, costType, cooldown, target) { }
}
