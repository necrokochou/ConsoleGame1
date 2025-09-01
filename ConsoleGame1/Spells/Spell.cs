using ConsoleGame1.Core;
using ConsoleGame1.Utils;


namespace ConsoleGame1.Spells;


public abstract class Spell {
    public Spell(Entity? owner, string name, float damage, float cost, string costType, int cooldown, ITarget? target) {
        this.owner = owner;
        this.name = name;
        this.damage = damage;
        this.cost = cost;
        this.costType = costType;
        this.cooldown = cooldown;
        this.target = target;
    }

    private Entity? owner;
    private string name;
    private float damage;
    private float cost;
    private string costType;
    private int cooldown;
    private ITarget? target;

    private Entity? Owner {
        get => owner;
        set => owner = value;
    }
    public string Name {
        get => name;
    }
    private float Damage {
        get => damage;
    }
    private float Cost {
        get => cost;
    }
    private string CostType {
        get => costType;
    }
    private int Cooldown {
        get => cooldown;
    }
    private ITarget? Target {
        get => target;
    }

    public void Display() {
        Util.Print($"{Util.Capitalize(Name)} [ {Owner?.Name} - {Damage} - {Cost} {Util.Capitalize(CostType)} ]");
    }

    public void Cast(string targetName) {
        var spellTarget = GetTarget(targetName);
        
        if (!Target.CanTarget(Owner, spellTarget)) {
            Util.Print("Target is not valid");
            return;
        }
        
        Util.Print("Casting spell...");
        Owner?.Mana.Decrease(Cost);
        spellTarget.Health.Decrease(Damage);
    }

    private Entity? GetTarget(string targetName) {
        foreach (var entity in World.Entities) {
            if (entity.Name == targetName) {
                return entity;
            }
        }
        
        return null;
    }
    
    public void SetOwner(Entity spellOwner) {
        Owner = spellOwner;
    }
}
