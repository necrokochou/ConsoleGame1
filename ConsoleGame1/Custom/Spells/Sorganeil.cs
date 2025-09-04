using ConsoleGame1.Core;
using ConsoleGame1.Spells;


namespace ConsoleGame1.Custom.Spells;


class Sorganeil : Spell {
    public Sorganeil() : base(
        "sorganeil",
        25f,
        10f,
        "mana",
        3,
        new EnemyTarget()
    ) {}
}
