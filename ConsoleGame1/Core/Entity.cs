using ConsoleGame1.Items;
using ConsoleGame1.Spells;
using ConsoleGame1.Utils;


namespace ConsoleGame1.Core;


public abstract class Entity : ICanUseSpell, ICanUseItem{
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
    private Team team;

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
    }
    public List<Item> Inventory {
        get => inventory;
    }
    public Team Team {
        get => team;
    }
    
    public bool IsAlive {
        get => Health.Value > 0;
    }
    public bool IsDead {
        get => !IsAlive;
    }

    public void Display() {
        Util.Print($"Name: {Util.Capitalize(Name)}");
        Health.Display();
        Mana.Display();
        Util.Print($"Speed: {speed}");
        Console.Write("Skills: ");
        for (int i = 0; i < SpellBook.Count; i++) {
            Console.Write(Util.Titlecase(SpellBook[i].Name));
            if (i + 1 < SpellBook.Count) {
                Console.Write(", ");
            }
        }
        Util.Spacer.Y();
        Util.Print($"Team: {Team}");
        Util.Spacer.Y(2);
    }

    public void SetSpeed(int amount) {
        Speed = amount;
    }
    
    public void SetTeam(Team teamType) {
        team = teamType;
    }

    public void StoreItem(Item item) {
        Inventory.Add(item);
    }
    
    public void LearnSpells(params Spell[] spells) {
        foreach (var s in spells) {
            s.SetOwner(this);
        }
        SpellBook.AddRange(spells);
    }
    
    public abstract void UseSpell();
    public abstract void UseRandomSpell();
    public abstract void UseItem();
    public abstract void UseRandomItem();
}
