using ConsoleGame1.Core;


namespace ConsoleGame1.Spells;


public class BasicSpell : Spell {
    public BasicSpell(Entity? owner, string name, float damage, float cost, string costType, int cooldown, ITarget? target) 
        : base(owner, name, damage, cost, costType, cooldown, target) { }
}
