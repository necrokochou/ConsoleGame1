namespace ConsoleGame1.Core;


public interface ICanUseSpell {
    void UseSpell();
    void UseRandomSpell();
}

public interface ICanUseItem {
    void UseItem();
    void UseRandomItem();
}

public interface ITarget {
    bool CanTarget(Entity caster, Entity target);
}