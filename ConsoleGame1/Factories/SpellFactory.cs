using ConsoleGame1.Core;
using ConsoleGame1.Spells;


namespace ConsoleGame1.Factories;


static class SpellFactory {
    public static BasicSpell Create(string name, float damage = 0f, float cost = 0f, string costType = "", int cooldown = 0, ITarget? target = null) {
        return new BasicSpell(null, name, damage, cost, costType, cooldown, target);
    }
}
