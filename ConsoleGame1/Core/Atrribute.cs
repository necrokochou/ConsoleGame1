using ConsoleGame1.Utils;
using String = ConsoleGame1.Utils.String;


namespace ConsoleGame1.Core;


public class Attribute {
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
        Console.WriteLine($"{String.ToProperCase(name)}: {value}/{max}");
    }

    public void Increase(float amount) {
        value = Math.Min(value + amount, max);
    }

    public void Decrease(float amount) {
        value = Math.Min(value - amount, max);
    }
}
