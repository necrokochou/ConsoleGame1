using ConsoleGame1.Commands;
using ConsoleGame1.Core;


namespace ConsoleGame1.Characters;


abstract class Character : Entity {
    protected Character(string name, int health, int mana) : base(name, health, mana) {
        AddActions(new CastSpell(), new CastRandomSpell(), new UseItem(), new UseRandomItem());
    }
}
