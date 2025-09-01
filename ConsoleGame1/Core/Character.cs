using ConsoleGame1.Spells;
using ConsoleGame1.Utils;


namespace ConsoleGame1.Core;


class Character : Entity {
    public Character(string name, int health, int mana) : base(name, health, mana) { }

    public override void UseSkill() {
        Console.Write("Select spell > ");
        var spellName = Console.ReadLine();

        Console.Write("Select target > ");
        var targetName = Console.ReadLine();
        
        foreach (var spell in SpellBook) {
            if (spell.Name == spellName) {
                spell.Cast(targetName);
                Console.WriteLine($"{Util.Capitalize(Name)} used {Util.Titlecase(spell.Name)} on {Util.Capitalize(targetName)}");
                Div.Y();
            }
        }
    }
    
    public override void UseRandomSkill() {
        var rand = new Random();
        
        var spell = SpellBook[rand.Next(SpellBook.Count)];
        var target = World.Entities[rand.Next(World.Entities.Count)];
        
        spell.Cast(target.Name);
        Console.WriteLine($"{Util.Capitalize(Name)} used {Util.Titlecase(spell.Name)} on {Util.Capitalize(target.Name)}");
        Div.Y();
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
