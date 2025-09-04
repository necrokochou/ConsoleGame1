using ConsoleGame1.Characters;
using ConsoleGame1.Core;
using ConsoleGame1.Custom.Spells;
using ConsoleGame1.Factories;
using ConsoleGame1.Items;
using ConsoleGame1.Spells;
using ConsoleGame1.Utils;


namespace ConsoleGame1;


class Program {
    private static void Main() {
        var a1 = CharacterFactory.Create("A1", 100, 100);
        var a2 = CharacterFactory.Create("A2", 100, 100);
        var a3 = CharacterFactory.Create("A3", 100, 100);
        var a4 = CharacterFactory.Create("A4", 100, 100);
        
        a1.LearnSpells(new Zoltraak());
        a2.LearnSpells(new Reelseiden(), new Sorganeil());
        a3.LearnSpells(new Fireball(), new IceShard());
        a4.LearnSpells(new Fireball(), new IceShard());

        a1.SetTeam(Team.Ally);
        a2.SetTeam(Team.Ally);
        a3.SetTeam(Team.Enemy);
        a4.SetTeam(Team.Enemy);

        var apple = ItemFactory.Create("apple");
        var lemon = ItemFactory.Create("lemon");

        a1.StoreItems(apple);
        a3.StoreItems(lemon);
        
        World.Entities.AddRange(a1, a2, a3, a4);
        World.SetRandomSpeed();

        var turn = 0;
        var turnDisplay = 0;

        while (true) {
            var turnOrder = World.Entities
                .Where(e => e.IsAlive)
                .OrderByDescending(e => e.Speed)
                .ToList();
            
            if (turnOrder.Count == 0) break;
            
            var currentTurn = turnOrder[turn % turnOrder.Count];

            turn++;
            
            Console.WriteLine($"Turn {++turnDisplay}");
            Spacer.Y();

            currentTurn.Display();
            currentTurn.TakeTurn();
            
            var winningTeam = World.GetWinningTeam();
            if (winningTeam == null)
                continue;

            Console.WriteLine($"{winningTeam} team wins!\n");
            foreach (var entity in World.Entities.Where(e => e.Team == winningTeam)) {
                entity.Display();
            }
            break;
        }
    }
}
