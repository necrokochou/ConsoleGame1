using ConsoleGame1.Core;


namespace ConsoleGame1.Interfaces;


public interface ICanCastSpell {
    void CastSpell();
    void CastRandomSpell();
}

public interface ICanUseItem {
    void UseItem();
    void UseRandomItem();
}


public interface INameable {
    string Name { get; }
}

public interface IExecutable : INameable {
    Result Execute(Entity self, Entity target);

    Result Execute(Entity self) {
        return Execute(self, self);
    }
}


public interface ITarget {
    bool CanTarget(Entity self, Entity target);
}