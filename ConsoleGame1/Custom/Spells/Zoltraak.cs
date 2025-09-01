using ConsoleGame1.Core;
using ConsoleGame1.Spells;


namespace ConsoleGame1.Custom.Spells;


class Zoltraak : Spell {
    public Zoltraak() : base(
        null,
        "zoltraak",
        25f,
        10f,
        "mana",
        3,
        new EnemyTarget()
    ) {}
}
