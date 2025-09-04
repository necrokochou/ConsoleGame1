using ConsoleGame1.Core;
using ConsoleGame1.Spells;


namespace ConsoleGame1.Custom.Spells;


class Heal : Spell {
    public Heal() : base(
        "fireball",
        25f, 
        10f, 
        "mana",
        3,
        new SelfTarget()
    ) {}
}
