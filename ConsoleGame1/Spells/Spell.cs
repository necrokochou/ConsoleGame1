using ConsoleGame1.Core;
using ConsoleGame1.Utils;


namespace ConsoleGame1.Spells;


public abstract class Spell {
    public Spell(Entity owner, string name, float damage, float cost, string costType, int cooldown, ITarget target) {
        this.owner = owner;
        this.name = name;
        this.damage = damage;
        this.cost = cost;
        this.costType = costType;
        this.cooldown = cooldown;
        this.target = target;
    }

    private Entity owner;
    private string name;
    private float damage;
    private float cost;
    private string costType;
    private int cooldown;
    private ITarget target;

    public string Name {
        get => name;
    }
    
    public ITarget Target {
        get => target;
    }

    public void Display() {
        Console.WriteLine($"{Text.Capitalize(name)} [ {owner?.Name} - {damage} - {cost} {Text.Capitalize(costType)} ]");
    }

    public void Cast(Entity spellTarget) {
        
        Console.WriteLine("Casting spell...");
        owner.Mana.Decrease(cost);
        spellTarget.Health.Decrease(damage);
    }

    public Entity? GetTarget(string? targetName) {
        foreach (var entity in World.Entities) {
            if (entity.Name == targetName) {
                return entity;
            }
        }
        
        return null;
    }
    
    public void SetOwner(Entity spellOwner) {
        owner = spellOwner;
    }
}
