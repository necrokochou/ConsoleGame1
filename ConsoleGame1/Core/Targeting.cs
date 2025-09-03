namespace ConsoleGame1.Core;


public class SelfTarget : ITarget {
    public bool CanTarget(Entity self, Entity target) => self == target;
}

public class AllyTarget : ITarget {
    public bool CanTarget(Entity self, Entity target) => self.Team == target.Team;
}

public class EnemyTarget : ITarget {
    public bool CanTarget(Entity self, Entity target) => self.Team != target.Team;
}
