using ConsoleGame1.Spells;
using ConsoleGame1.Utils;


namespace ConsoleGame1.Core;


class Character : Entity {
    public Character(string name, int health, int mana) : base(name, health, mana) { }

    public override void UseSpell() {
        Console.Write("Select spell > ");
        var spellName = Console.ReadLine();

        Console.Write("Select target > ");
        var targetName = Console.ReadLine();
        
        foreach (var spell in SpellBook) {
            if (spell.Name == spellName) {
                spell.Cast(targetName);
                Util.Print($"{Util.Capitalize(Name)} used {Util.Titlecase(spell.Name)} on {Util.Capitalize(targetName)}");
                Util.Spacer.Y();
            }
        }
    }
    
    public override void UseRandomSpell() {
        var rand = new Random();
        
        var spell = SpellBook[rand.Next(SpellBook.Count)];
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
            if (target.IsDead || target == this || target.Team == Team) {
                targetsFound.Add(target);
                continue;
            }
            
            break;
        }
        
        spell.Cast(target.Name);
        Util.Print($"{Util.Capitalize(Name)} used {Util.Titlecase(spell.Name)} on {Util.Capitalize(target.Name)}");
        Util.Spacer.Y();
    }

    public override void UseItem() {
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

    public override void UseRandomItem() { }
}
