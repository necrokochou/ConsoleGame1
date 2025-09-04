using ConsoleGame1.Core;
using ConsoleGame1.Utils;


namespace ConsoleGame1.Items;


class SwordItem : Item {
    public SwordItem(string name, float damage) : base(name, true) {
        this.damage = damage;
    }
    
    private float damage;
    
    public float Damage {
        get => damage;
    }

    private Entity GetTarget(string name) {
        foreach (var entity in World.Entities) {
            if (entity.Name == name) {
                return entity;
            }
        }
        
        return null;
    }
}
