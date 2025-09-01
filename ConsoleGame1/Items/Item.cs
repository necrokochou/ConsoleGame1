namespace ConsoleGame1.Items;


abstract class Item {
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

    public abstract void Use(string targetName);
}
