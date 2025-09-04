using ConsoleGame1.Core;
using ConsoleGame1.Interfaces;


namespace ConsoleGame1.Items;


public abstract class Item : IExecutable {
    public Item(string name, bool needsTarget) {
        this.name = name;
        this.needsTarget = needsTarget;
    }
    
    private string name;
    private int count;
    private bool needsTarget;
    
    public string Name {
        get => name;
    }
    public int Count {
        get => count;
    }
    public bool NeedsTarget {
        get => needsTarget;
    }

    public virtual Result Execute(Entity self, Entity? target = null) {
        return Result.Success;
    }
}
