using ConsoleGame1.Core;
using ConsoleGame1.Utils;


namespace ConsoleGame1.Spells;


class Spell {
    public Spell(Entity? owner, string name, float damage, float cost, string costType, int cooldown) {
        this.owner = owner;
        this.name = name;
        this.damage = damage;
        this.cost = cost;
        this.costType = costType;
        this.cooldown = cooldown;
    }

    private Entity? owner;
    private string name;
    private float damage;
    private float cost;
    private string costType;
    private int cooldown;

    public string Name {
        get => name;
    }

    public void Display() {
        Console.WriteLine($"{Util.Capitalize(name)} [ {owner?.Name} - {damage} - {cost} {Util.Capitalize(costType)} ]");
    }

    public void Cast(string targetName) {
        var target = GetTarget(targetName);

        owner?.Mana.Decrease(cost);
        target?.Health.Decrease(damage);
    }

    // private Entity GetOwner(string name) {
    //     foreach (var entity in World.Entities) {
    //         if (entity.Name == name) {
    //             return entity;
    //         }
    //     }
    //     
    //     return null;
    // }

    private Entity? GetTarget(string name) {
        foreach (var entity in World.Entities) {
            if (entity.Name == name) {
                return entity;
            }
        }
        
        return null;
    }
    
    public void SetOwner(Entity owner) {
        this.owner = owner;
    }
}
