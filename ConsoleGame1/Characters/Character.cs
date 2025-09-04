using ConsoleGame1.Core;
using ConsoleGame1.Interfaces;


namespace ConsoleGame1.Characters;


abstract class Character : Entity {
    protected Character(string name, int health, int mana) : base(name, health, mana) {
        AddActions(new CastSpellCommand(), new CastRandomSpellCommand(), new UseItemCommand(), new UseRandomItemCommand());
    }
}
