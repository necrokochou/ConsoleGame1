using ConsoleGame1.Items;
using ConsoleGame1.Spells;
using ConsoleGame1.Utils;


namespace ConsoleGame1.Core;


public abstract class Entity {
    public Entity(string name, int health, int mana) {
        this.name = name;
        this.health = new Attribute("health", health);
        this.mana = new Attribute("mana", mana);
    }
    
    // private string id;
    private string name;
    private Attribute health;
    private Attribute mana;
    private int speed;
    private List<ICommand> commands = [];
    private List<Spell> spellBook = [];
    private List<Item> inventory = [];
    private Team team;

    public string Name {
        get => name;
    }
    public Attribute Health {
        get => health;
    }
    public Attribute Mana {
        get => mana;
    }
    public int Speed {
        get => speed;
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
        Console.WriteLine($"Name: {Text.Capitalize(name)}");
        health.Display();
        mana.Display();
        Console.WriteLine($"Speed: {speed}");
        Console.Write("Skills: ");
        for (var i = 0; i < spellBook.Count; i++) {
            Console.Write(Text.Titlecase(spellBook[i].Name));
            if (i + 1 < spellBook.Count) {
                Console.Write(", ");
            }
        }
        Spacer.Y();
        Console.WriteLine($"Team: {team}");
        Spacer.Y();
    }

    public void SetSpeed(int amount) {
        speed = amount;
    }
    
    public void SetTeam(Team teamType) {
        team = teamType;
    }
    
    protected void AddActions(params ICommand[] actions) {
        commands.AddRange(actions);
    }

    public void StoreItems(params Item[] items) {
        inventory.AddRange(items);
    }
    
    public void LearnSpells(params Spell[] spells) {
        foreach (var s in spells) {
            s.SetOwner(this);
        }
        spellBook.AddRange(spells);
    }

    public virtual void TakeTurn(bool doRandomAction = false) {
        if (doRandomAction) {
            RandomAction();
            return;
        }

        while (true) {
            UserInterface.DisplayAsChoices(commands);
            var choice = UserInterface.GetInput("Select action");

            foreach (var command in commands) {
                if (choice != command.Name) continue;

                command.Execute(this);
                return;
            }
        }
    }

    private void RandomAction() {
        var rand = new Random();
        var action = commands[rand.Next(commands.Count)];
        
        action.Execute(this);
    }

    public virtual void CastSpell() {
        Console.Write("Select spell > ");
        var spellName = Console.ReadLine();

        Console.Write("Select target > ");
        var targetName = Console.ReadLine();
        
        foreach (var spell in spellBook) {
            if (spell.Name != spellName.ToLower())
                continue;

            var target = spell.GetTarget(targetName);
            if (target == null) {
                Console.WriteLine("Target not found");
                return;
            }
            
            if (!spell.Target.CanTarget(this, target)) {
                Console.WriteLine("Target is not valid");
                return;
            }
            
            spell.Cast(target);
            Console.WriteLine($"{Text.Capitalize(name)} used {Text.Titlecase(spell.Name)} on {Text.Capitalize(targetName)}");
            Spacer.Y();
        }
    }
    
    public virtual void CastRandomSpell() {
        var rand = new Random();
        
        var spell = spellBook[rand.Next(spellBook.Count)];
        Entity target;
        List<Entity> targetsFound = [];
        
        while (true) {
            if (targetsFound.Count >= World.Entities.Count) {
                return;
            }
            
            target = World.GetRandomEntity();

            if (targetsFound.Contains(target)) {
                continue;
            }
            if (target.IsDead || target == this || target.Team == team) {
                targetsFound.Add(target);
                continue;
            }
            
            break;
        }
        
        spell.Cast(target);
        Console.WriteLine($"{Text.Capitalize(name)} used {Text.Titlecase(spell.Name)} on {Text.Capitalize(target.Name)}");
        Spacer.Y();
    }

    // TODO: Fix UseItem
    public virtual void UseItem() {
        Console.Write("Select item > ");
        var itemName = Console.ReadLine();
        
        var item = GetItem(itemName);

        if (item.NeedsTarget) {
            Console.Write("Select target > ");
            var targetName = Console.ReadLine();
            
            item.Use(targetName);
            Console.WriteLine($"{Text.Capitalize(name)} used {Text.Titlecase(item.Name)} on {Text.Capitalize(targetName)}");
            Spacer.Y();
        }
    }

    private Item? GetItem(string? name) {
        foreach (var item in inventory) {
            if (item.Name == name) {
                return item;
            }
        }
        
        return null;
    }

    public virtual void UseRandomItem() {
        var rand = new Random();
        
        var item = inventory[rand.Next(inventory.Count)];
        Entity target;
        List<Entity> targetsFound = [];
        var attempts = 0;
        
        while (true) {
            target = World.Entities[rand.Next(World.Entities.Count)];
            attempts++;
            if (attempts > World.Entities.Count) {
                return;
            }
            if (targetsFound.Contains(target)) {
                continue;
            }
            if (target.IsDead || target == this || target.Team == team) {
                targetsFound.Add(target);
                continue;
            }
            
            break;
        }
        
        item.Use(target.Name);
        Console.WriteLine($"{Text.Capitalize(name)} used {Text.Titlecase(item.Name)} on {Text.Capitalize(target.Name)}");
        Spacer.Y();
    }
}
