using ConsoleGame1.Core;
using ConsoleGame1.Interfaces;
using ConsoleGame1.Spells;


namespace ConsoleGame1.Factories;


static class SpellFactory {
    public static BasicSpell Create(string name, float cost = 0f, string costType = "", int cooldown = 0, ITarget? target = null) {
        return new BasicSpell(name, cost, costType, cooldown, target);
    }
}
