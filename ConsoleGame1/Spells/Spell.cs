using ConsoleGame1.Core;
using ConsoleGame1.Interfaces;


namespace ConsoleGame1.Spells;


public abstract class Spell : IExecutable {
    public Spell(string name, float cost, string costType, int cooldown, ITarget? target) {
        // this.owner = owner;
        this.name = name;
        this.cost = cost;
        this.costType = costType;
        this.cooldown = cooldown;
        this.target = target;
    }

    // private Entity owner;
    private string name;
    private float cost;
    private string costType;
    private int cooldown;
    private ITarget? target;
    private List<IEffect> effects = [];

    public string Name {
        get => name;
    }
    public ITarget? Target {
        get => target;
    }
    protected List<IEffect> Effects {
        get => effects;
    }

    // public void Display() {
    //     Console.WriteLine($"{Text.ToProperCase(name)} [ {owner?.Name} - {damage} - {cost} {Text.ToProperCase(costType)} ]");
    // }

    public virtual Result Execute(Entity caster, Entity? spellTarget = null) {
        Console.WriteLine("Casting spell...");
        caster.Mana.Decrease(cost);
        effects.ForEach(effect => effect.Apply(spellTarget ?? caster));
        return Result.Success;
    }

    // public Entity? GetTarget(string? targetName) {
    //     foreach (var entity in World.Entities) {
    //         if (entity.Name == targetName) {
    //             return entity;
    //         }
    //     }
    //     
    //     return null;
    // }

    // public void SetOwner(Entity spellOwner) {
    //     owner = spellOwner;
    // }
}
