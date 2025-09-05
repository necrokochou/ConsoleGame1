using ConsoleGame1.Core;
using ConsoleGame1.Interfaces;


namespace ConsoleGame1.Items;


public abstract class Item : IExecutable {
    public Item(string name, ITarget? target = null) {
        this.name = name;
        this.target = target;
    }
    
    private string name;
    private ITarget? target;
    private int count;
    
    public string Name {
        get => name;
    }
    public ITarget? Target {
        get => target;
    }
    public int Count {
        get => count;
    }

    public virtual Result Execute(Entity self, Entity? target = null) {
        return Result.Success;
    }
}
