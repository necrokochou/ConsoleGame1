using ConsoleGame1.Core;
using ConsoleGame1.Interfaces;
using ConsoleGame1.Spells;


namespace ConsoleGame1.Custom.Spells;


class Heal : Spell {
    public Heal()
        : base("heal", 10f, "mana", 3, new SelfTarget()) {
        Effects.Add(new HealEffect(25));
    }
}
