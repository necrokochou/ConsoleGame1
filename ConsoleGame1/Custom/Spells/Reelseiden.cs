using ConsoleGame1.Core;
using ConsoleGame1.Interfaces;
using ConsoleGame1.Spells;


namespace ConsoleGame1.Custom.Spells;


class Reelseiden : Spell {
    public Reelseiden()
        : base("reelseiden", 10f, "mana", 3, new EnemyTarget()) {
        Effects.Add(new DamageEffect(50));
    }
}
