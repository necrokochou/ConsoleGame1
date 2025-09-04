namespace ConsoleGame1.Core;


static class World {
    private static List<Entity> entities = [];
    
    public static List<Entity> Entities {
        get => entities;
    }

    public static void SetRandomSpeed() {
        var rand = new Random();

        foreach (var entity in entities) {
            entity.SetSpeed(rand.Next(10));
        }
    }
    
    public static Entity? GetEntityByName(string name) {
        return entities.Find(e => e.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));
    }
    
    public static Entity GetRandomEntity() {
        var rand = new Random();
        return entities[rand.Next(entities.Count)];
    }

    public static Team? GetWinningTeam() { 
        var aliveTeams = entities.Where(e => e.IsAlive)
            .Select(e => e.Team)
            .Distinct()
            .ToList();
        
        return aliveTeams.Count == 1 ? aliveTeams[0] : null;
    }
}
