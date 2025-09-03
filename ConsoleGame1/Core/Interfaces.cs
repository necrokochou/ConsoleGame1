namespace ConsoleGame1.Core;


public interface ICanUseSpell {
    void UseSpell();
    void UseRandomSpell();
}

public interface ICanUseItem {
    void UseItem();
    void UseRandomItem();
}


public interface ICommand {
    string Name { get; }
    
    void Execute(Entity self);
}

public interface ITarget {
    bool CanTarget(Entity self, Entity target);
}