using ConsoleGame1.Core;
using ConsoleGame1.Spells;


namespace ConsoleGame1.Custom.Spells;


class Fireball : Spell {
    public Fireball() : base(
        null, 
        "fireball",
        25f, 
        10f, 
        "mana",
        3,
        new EnemyTarget()
    ) {}
}
