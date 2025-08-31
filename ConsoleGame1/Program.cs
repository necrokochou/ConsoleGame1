using System.Globalization;


namespace ConsoleGame1;


class Program {
    private static void Main(string[] args) {
        // var frieren = new Character("frieren", 100, 100, [new Zoltraak("frieren")]);
        // var fern = new Character("fern", 100, 100, [new Zoltraak("fern")]);
        // var ubel = new Enemy("ubel", 100, 100, [new Reelseiden("ubel"), new Sorganeil("ubel")]);
        //
        // World.Entities.AddRange(frieren, fern, ubel);
        
        var hero1 = new Character("hero1", 100, 100, [new Zoltraak("hero1")]);
        var mage2 = new Character("mage2", 100, 100, [new Fireball("mage2"), new IceShard("mage2")]);
        var mage3 = new Character("mage3", 100, 100, [new IceShard("mage3"), new Reelseiden("mage3")]);;

        var bow = new BowItem("bow", 30f);
        hero1.StoreItem(bow);
        var sword = new SwordItem("sword", 20f);
        mage2.StoreItem(sword);
        mage3.StoreItem(sword);
        
        World.Entities.AddRange(hero1, mage2, mage3);
        
        World.SetRandomSpeed();

        var turnOrder = new List<Entity>(World.Entities).OrderByDescending(e => e.Speed).ToList();
        
        int turn = 0;

        while (true) {
            Console.WriteLine($"Turn {turn + 1}");
            Div.Y();
            
            var currentTurn = turnOrder[turn % turnOrder.Count];

            currentTurn.Display();
            if (currentTurn is Character character)
                character.UseSkill();
                // character.UseItem();
            else if (currentTurn is Enemy enemy)
                enemy.UseRandomSkill();

            turn++;

            if (turn >= 10) break;
        }
    }
}


static class Util {
    public static string Capitalize(string text) {
        return char.ToUpper(text[0]) + text.Substring(1);
    }
    public static string Titlecase(string text) {
        return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text);
    }
}


static class Div {
    public static void X(int spaces = 1) {
        for (int i = 0; i < spaces; i++) {
            Console.Write(' ');
        }
    }

    public static void Y(int spaces = 1) {
        for (int i = 0; i < spaces; i++) {
            Console.WriteLine();
        }
    }
}


static class World {
    private static List<Entity> entities = [];
    
    public static List<Entity> Entities {
        get => entities;
    }

    public static void SetRandomSpeed() {
        var rand = new Random();

        foreach (var entity in entities) {
            entity.SetSpeed(rand.Next(10));
        }
    }
}


class Entity {
    public Entity(string name, int health, int mana, List<Skill> skills) {
        Name = name;
        Health = new Attribute("health", health);
        Mana = new Attribute("mana", mana);
        Skills = skills;
    }
    
    // private string id;
    private string? name;
    private Attribute? health;
    private Attribute? mana;
    private int speed;
    private List<Skill>? skills;
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
    public List<Skill> Skills {
        get => skills;
        protected set => skills = value;
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
        for (int i = 0; i < skills.Count; i++) {
            Console.Write(Util.Titlecase(skills[i].Name));
            if (i + 1 < skills.Count) {
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
}


class Character : Entity {
    public Character(string name, int health, int mana, List<Skill> skills) : base(name, health, mana, skills) { }

    public void UseSkill() {
        Console.Write("Select skill > ");
        var skillName = Console.ReadLine();

        Console.Write("Select target > ");
        var targetName = Console.ReadLine();
        
        foreach (var skill in Skills) {
            if (skill.Name == skillName) {
                skill.Cast(targetName);
                Console.WriteLine($"{Util.Capitalize(Name)} used {Util.Titlecase(skill.Name)} on {Util.Capitalize(targetName)}");
                Div.Y();
            }
        }
    }
    
    public void UseRandomSkill() {
        var rand = new Random();
        
        var skill = Skills[rand.Next(Skills.Count)];
        var target = World.Entities[rand.Next(World.Entities.Count)];
        
        skill.Cast(target.Name);
        Console.WriteLine($"{Util.Capitalize(Name)} used {Util.Titlecase(skill.Name)} on {Util.Capitalize(target.Name)}");
        Div.Y();
    }

    public void UseItem() {
        Console.Write("Select item > ");
        var itemName = Console.ReadLine();

        foreach (var item in Inventory) {
            if (item.Name == itemName) {
                if (item.NeedsTarget) {
                    Console.Write("Select target > ");
                    var targetName = Console.ReadLine();
                    
                    item.Use(targetName);
                }
            }
        }
    }
}


class Enemy : Entity {
    public Enemy(string name, int health, int mana, List<Skill> skills) : base(name, health, mana, skills) { }

    public void UseRandomSkill() {
        var rand = new Random();
        
        var skill = Skills[rand.Next(Skills.Count)];
        var target = GetRandomTarget();
        
        skill.Cast(target.Name);
        Console.WriteLine($"{Util.Capitalize(Name)} used {Util.Titlecase(skill.Name)} on {Util.Capitalize(target.Name)}");
        Div.Y();
    }

    private Entity GetRandomTarget() {
        while (true) {
            var rand = new Random();
            var target = World.Entities[rand.Next(World.Entities.Count)];
            
            if (target != this) {
                return target;
            }
        }
    }
}


class Attribute {
    public Attribute(string name, float max) {
        this.name = name;
        value = max;
        this.max = max;
    }
    
    private string name;
    private float value;
    private float max;

    public float Value {
        get => value;
    }

    public void Display() {
        Console.WriteLine($"{Util.Capitalize(name)}: {value}/{max}");
    }

    public void Increase(float amount) {
        value = Math.Min(value + amount, max);
    }

    public void Decrease(float amount) {
        value = Math.Min(value - amount, max);
    }
}


class Skill {
    public Skill(string ownerName, string name, float damage, float cost, string costType, int cooldown) {
        this.ownerName = ownerName;
        this.name = name;
        this.damage = damage;
        this.cost = cost;
        this.costType = costType;
        this.cooldown = cooldown;
    }

    private string ownerName;
    private string name;
    private float damage;
    private float cost;
    private string costType;
    private int cooldown;

    public string OwnerName {
        get => ownerName;
    }
    public string Name {
        get => name;
    }

    public void Display() {
        Console.WriteLine($"{Util.Capitalize(name)} [ {ownerName} - {damage} - {cost} {Util.Capitalize(costType)} ]");
    }

    public void Cast(string targetName) {
        var owner = GetOwner(ownerName);
        var target = GetTarget(targetName);

        owner.Mana.Decrease(cost);
        target.Health.Decrease(damage);
    }

    private Entity GetOwner(string name) {
        foreach (var entity in World.Entities) {
            if (entity.Name == name) {
                return entity;
            }
        }
        
        return null;
    }

    private Entity GetTarget(string name) {
        foreach (var entity in World.Entities) {
            if (entity.Name == name) {
                return entity;
            }
        }
        
        return null;
    }
}

class Zoltraak : Skill {
    public Zoltraak(string ownerName) : base(ownerName, "zoltraak",25f, 10f, "mana", 3) {}
}

class Reelseiden : Skill {
    public Reelseiden(string ownerName) : base(ownerName, "reelseiden", 25f, 10f, "mana", 3) {}
}


class Sorganeil : Skill {
    public Sorganeil(string ownerName) : base(ownerName, "sorganeil", 25f, 10f, "mana", 3) {}
}


class Fireball : Skill {
    public Fireball(string ownerName) : base(ownerName, "fireball", 25f, 10f, "mana", 3) {}
}

class IceShard : Skill {
    public IceShard(string ownerName) : base(ownerName, "ice shard", 25f, 10f, "mana", 3) {}
}


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


class SwordItem : Item {
    public SwordItem(string name, float damage) : base(name, true) {
        this.damage = damage;
    }
    
    private float damage;
    
    public float Damage {
        get => damage;
    }

    public override void Use(string targetName) {
        var target = GetTarget(targetName);
        
        Console.WriteLine();
        target.Health.Decrease(damage);
    }

    private Entity GetTarget(string name) {
        foreach (var entity in World.Entities) {
            if (entity.Name == name) {
                return entity;
            }
        }
        
        return null;
    }
}

class BowItem : Item {
    public BowItem(string name, float damage) : base(name, true) {
        this.damage = damage;
    }
    
    private float damage;
    
    public float Damage {
        get => damage;
    }

    public override void Use(string targetName) {
        var target = GetTarget(targetName);
        
        Console.WriteLine();
        target.Health.Decrease(damage);
    }

    private Entity GetTarget(string name) {
        foreach (var entity in World.Entities) {
            if (entity.Name == name) {
                return entity;
            }
        }
        
        return null;
    }
}
