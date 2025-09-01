using ConsoleGame1.Spells;
using ConsoleGame1.Utils;


namespace ConsoleGame1.Core;


class Enemy : Entity {
    public Enemy(string name, int health, int mana) : base(name, health, mana) { }

    public override void UseSpell() { }

    public override void UseRandomSpell() {
        var rand = new Random();
        
        var spell = SpellBook[rand.Next(SpellBook.Count)];
        var target = GetRandomTarget();
        
        spell.Cast(target.Name);
        Util.Print($"{Util.Capitalize(Name)} used {Util.Titlecase(spell.Name)} on {Util.Capitalize(target.Name)}");
        Util.Spacer.Y();
    }

    public override void UseItem() { }

    public override void UseRandomItem() { }

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
