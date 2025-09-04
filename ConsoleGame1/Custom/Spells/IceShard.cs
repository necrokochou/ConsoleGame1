using ConsoleGame1.Core;
using ConsoleGame1.Spells;


namespace ConsoleGame1.Custom.Spells;


class IceShard : Spell {
    public IceShard() : base(
        "ice shard",
        25f, 
        10f, 
        "mana",
        3,
        new EnemyTarget()
    ) {}
}
