using ConsoleGame1.Core;
using ConsoleGame1.Items;
using ConsoleGame1.Spells;
using ConsoleGame1.Utils;


namespace ConsoleGame1;


class Program {
    private static void Main(string[] args) {
        var a1 = new Character("hero1", 100, 100);
        var a2 = new Character("hero2", 100, 100);
        var a3 = new Character("hero3", 100, 100);
        
        a1.LearnSpells(new Zoltraak());
        a2.LearnSpells(new Reelseiden(), new Sorganeil());
        a3.LearnSpells(new Fireball(), new IceShard());
        
        World.Entities.AddRange(a1, a2, a3);
        World.SetRandomSpeed();

        var turnOrder = new List<Entity>(World.Entities).OrderByDescending(e => e.Speed).ToList();
        var turn = 0;

        while (true) {
            Console.WriteLine($"Turn {turn + 1}");
            Div.Y();
            
            var currentTurn = turnOrder[turn % turnOrder.Count];

            currentTurn.Display();
            currentTurn.UseRandomSkill();

            turn++;

            if (turn >= 10) break;
        }
    }
}
