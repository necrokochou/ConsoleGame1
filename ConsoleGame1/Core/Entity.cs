using ConsoleGame1.Items;
using ConsoleGame1.Spells;
using ConsoleGame1.Utils;


namespace ConsoleGame1.Core;


abstract class Entity {
    public Entity(string name, int health, int mana) {
        Name = name;
        Health = new Attribute("health", health);
        Mana = new Attribute("mana", mana);
    }
    
    // private string id;
    private string? name;
    private Attribute? health;
    private Attribute? mana;
    private int speed;
    private List<Spell>? spellBook = [];
    private List<Item>? inventory = [];

    public string Name {
        get => name;
        protected set => name = value;
    }
    public Attribute Health {
        get => health;
        protected set => health = value;
    }
    public Attribute Mana {
        get => mana;
        protected set => mana = value;
    }
    public int Speed {
        get => speed;
        protected set => speed = value;
    }
    public List<Spell> SpellBook {
        get => spellBook;
        protected set => spellBook = value;
    }
    
    public List<Item> Inventory {
        get => inventory;
        protected set => inventory = value;
    }

    public void Display() {
        Console.WriteLine($"Name: {Util.Capitalize(name)}");
        health.Display();
        mana.Display();
        Console.WriteLine($"Speed: {speed}");
        Console.Write("Skills: ");
        for (int i = 0; i < spellBook.Count; i++) {
            Console.Write(Util.Titlecase(spellBook[i].Name));
            if (i + 1 < spellBook.Count) {
                Console.Write(", ");
            }
        }
        Div.Y(2);
    }

    public void SetSpeed(int amount) {
        Speed = amount;
    }

    public void StoreItem(Item item) {
        inventory.Add(item);
    }
    
    public void LearnSpells(params Spell[] spells) {
        foreach (var s in spells) {
            s.SetOwner(this);
        }
        spellBook.AddRange(spells);
    }
    
    public abstract void UseSkill();
    public abstract void UseRandomSkill();
    public abstract void UseItem();
    public abstract void UseRandomItem();
}
