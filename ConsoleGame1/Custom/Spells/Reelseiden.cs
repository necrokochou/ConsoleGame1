using ConsoleGame1.Core;
using ConsoleGame1.Spells;


namespace ConsoleGame1.Custom.Spells;


class Reelseiden : Spell {
    public Reelseiden() : base(
        null,
        "reelseiden",
        25f,
        10f,
        "mana",
        3,
        new EnemyTarget()
    ) { }
}
