using ConsoleGame1.Core;


namespace ConsoleGame1.Interfaces;


class DamageEffect : IEffect {
    public DamageEffect(float damage) {
        this.damage = damage;
    }
    
    private readonly float damage;

    public void Apply(Entity target) {
        target.Health.Decrease(damage);
    }
}


class HealEffect : IEffect {
    public HealEffect(float heal) {
        this.heal = heal;
    }
    
    private readonly float heal;
    
    public void Apply(Entity target) {
        target.Health.Increase(heal);
    }
}
