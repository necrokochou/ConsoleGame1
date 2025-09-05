using ConsoleGame1.Interfaces;
using ConsoleGame1.Items;
using ConsoleGame1.Spells;
using ConsoleGame1.Utils;
using String = ConsoleGame1.Utils.String;


namespace ConsoleGame1.Core;


public abstract class Entity : INameable {
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
    private List<IExecutable> commands = [];
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
        Console.WriteLine($"Name: {String.ToProperCase(name)}");
        health.Display();
        mana.Display();
        Console.WriteLine($"Speed: {speed}");
        Console.Write("Skills: ");
        for (var i = 0; i < spellBook.Count; i++) {
            Console.Write(String.ToTitleCase(spellBook[i].Name));
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

    protected void AddActions(params IExecutable[] actions) {
        commands.AddRange(actions);
    }

    public void StoreItems(params Item[] items) {
        inventory.AddRange(items);
    }

    public void LearnSpells(params Spell[] spells) {
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

            var command = commands.Find(c => c.Name == choice);
            if (command == null) {
                Console.WriteLine("Invalid action");
                continue;
            }

            var result = command.Execute(this);
            if (result is Result.Success) return;
        }
    }

    private void RandomAction() {
        var rand = new Random();
        var action = commands[rand.Next(commands.Count)];

        action.Execute(this);
    }

    public virtual Result CastSpell() {
        if (spellBook.Count <= 0) {
            Console.WriteLine("You have no spells");
            return Result.Fail;
        }

        Spell? spell;

        while (true) {
            UserInterface.DisplayAsChoices(spellBook);
            var spellName = UserInterface.GetInput("Select spell");
            spell = spellBook.Find(s => s.Name.Equals(spellName, StringComparison.CurrentCultureIgnoreCase));
            if (spell != null) {
                break;
            }

            Console.WriteLine("Spell not found");
        }

        if (spell.Target is not SelfTarget) {
            Entity? target;
            
            while (true) {
                UserInterface.DisplayAsChoices(World.Entities);
                var targetName = UserInterface.GetInput("Select target", false);
                target = World.GetEntityByName(targetName);
                if (target != null) {
                    break;
                }

                Console.WriteLine("Target not found");
            }

            spell.Execute(this, target);
            Console.WriteLine($"{String.ToProperCase(name)} used {String.ToTitleCase(spell.Name)} on {String.ToProperCase(target.Name)}");
        } else {
            spell.Execute(this);
            Console.WriteLine($"{String.ToProperCase(name)} used {String.ToTitleCase(spell.Name)}");
        }

        Spacer.Y();
        return Result.Success;
    }

    public virtual Result CastRandomSpell() {
        if (spellBook.Count <= 0) {
            Console.WriteLine("You have no spells");
            return Result.Fail;
        }

        var rand = new Random();

        var spell = spellBook[rand.Next(spellBook.Count)];

        if (spell.Target is not SelfTarget) {
            Entity target;
            List<Entity> targetsFound = [];

            while (true) {
                if (targetsFound.Count >= World.Entities.Count) {
                    return Result.Fail;
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

            spell.Execute(this, target);
            Console.WriteLine($"{String.ToProperCase(name)} used {String.ToTitleCase(spell.Name)} on {String.ToProperCase(target.Name)}");
        } else {
            spell.Execute(this);
            Console.WriteLine($"{String.ToProperCase(name)} used {String.ToTitleCase(spell.Name)}");
        }

        Spacer.Y();
        return Result.Success;
    }

    public virtual Result UseItem() {
        if (inventory.Count <= 0) {
            Console.WriteLine("You have no items");
            return Result.Fail;
        }

        Item? item;

        while (true) {
            UserInterface.DisplayAsChoices(inventory);
            var itemName = UserInterface.GetInput("Select item", false);
            item = GetItem(itemName);
            if (item != null) break;
            Console.WriteLine("Item not found");
        }

        if (item.Target is not SelfTarget) {
            Entity? target;
            
            while (true) {
                UserInterface.DisplayAsChoices(World.Entities);
                var targetName = UserInterface.GetInput("Select target", false);
                target = World.GetEntityByName(targetName);
                if (target != null) break;
                Console.WriteLine("Target not found");
            }

            item.Execute(this, target);
            Console.WriteLine($"{String.ToProperCase(name)} used {String.ToTitleCase(item.Name)} on {String.ToProperCase(target.Name)}");
        } else {
            item.Execute(this);
            Console.WriteLine($"{String.ToProperCase(name)} used {String.ToTitleCase(item.Name)}");
        }

        Spacer.Y();
        return Result.Success;
    }

    private Item? GetItem(string? name) {
        return inventory.FirstOrDefault(item => item.Name == name);
    }

    public virtual Result UseRandomItem() {
        if (inventory.Count <= 0) {
            Console.WriteLine("You have no items");
            return Result.Fail;
        }

        var rand = new Random();

        var item = inventory[rand.Next(inventory.Count)];
        if (item.Target is not SelfTarget) {
            Entity target;
            List<Entity> targetsFound = [];
            
            while (true) {
                if (targetsFound.Count >= World.Entities.Count) {
                    return Result.Fail;
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

            item.Execute(this, target);
            Console.WriteLine($"{String.ToProperCase(name)} used {String.ToTitleCase(item.Name)} on {String.ToProperCase(target.Name)}");
        } else {
            item.Execute(this);
            Console.WriteLine($"{String.ToProperCase(name)} used {String.ToTitleCase(item.Name)}");
        }

        Spacer.Y();
        return Result.Success;
    }
}
