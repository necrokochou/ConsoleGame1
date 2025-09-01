using ConsoleGame1.Core;
using ConsoleGame1.Custom.Spells;
using ConsoleGame1.Factories;
using ConsoleGame1.Items;
using ConsoleGame1.Spells;
using ConsoleGame1.Utils;


namespace ConsoleGame1;


class Program {
    private static void Main() {
        var a1 = new Character("hero1", 100, 100);
        var a2 = new Character("hero2", 100, 100);
        var a3 = new Character("hero3", 100, 100);
        
        // var soulStrike = SpellFactory.Create("soul strike", 10f, 10f, "mana", 1, new EnemyTarget());
        
        a1.LearnSpells(new Zoltraak());
        a2.LearnSpells(new Reelseiden(), new Sorganeil());
        a3.LearnSpells(new Fireball(), new IceShard());

        a1.SetTeam(Team.Ally);
        a2.SetTeam(Team.Ally);
        a3.SetTeam(Team.Enemy);
        
        World.Entities.AddRange(a1, a2, a3);
        World.SetRandomSpeed();

        var turnOrder = new List<Entity>(World.Entities).OrderByDescending(e => e.Speed).ToList();
        var turn = 0;
        var turnDisplay = 0;

        while (true) {
            var currentTurn = turnOrder[turn % turnOrder.Count];

            turn++;
            
            if (!currentTurn.IsAlive) continue;
            
            Util.Print($"Turn {++turnDisplay}");
            Util.Spacer.Y();

            currentTurn.Display();
            currentTurn.UseRandomSpell();

            if (turnDisplay >= 10) break;
        }
    }
}
