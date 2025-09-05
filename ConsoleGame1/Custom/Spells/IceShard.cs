using ConsoleGame1.Core;
using ConsoleGame1.Interfaces;
using ConsoleGame1.Spells;


namespace ConsoleGame1.Custom.Spells;


class IceShard : Spell {
    public IceShard()
        : base("ice shard", 10f, "mana", 3, new EnemyTarget()) {
        Effects.Add(new DamageEffect(50));
    }
}
