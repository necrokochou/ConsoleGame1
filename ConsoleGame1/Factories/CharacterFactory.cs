using ConsoleGame1.Characters;
using ConsoleGame1.Core;


namespace ConsoleGame1.Factories;


static class CharacterFactory {
    public static BasicCharacter Create(string name, int health = 0, int mana = 0) {
        return new BasicCharacter(name, health, mana);
    }
}
