namespace ConsoleGame1.Core;


public class SelfTarget : ITarget {
    public bool CanTarget(Entity caster, Entity target) => caster == target;
}

public class AllyTarget : ITarget {
    public bool CanTarget(Entity caster, Entity target) => caster.Team == target.Team;
}

public class EnemyTarget : ITarget {
    public bool CanTarget(Entity caster, Entity target) => caster.Team != target.Team;
}
