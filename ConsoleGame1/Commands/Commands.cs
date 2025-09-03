using ConsoleGame1.Core;


namespace ConsoleGame1.Commands;


class CastSpell : ICommand {
    public string Name {
        get => "cast spell";
    }
    
    public void Execute(Entity self) {
        self.CastSpell();
    }
}

class CastRandomSpell : ICommand {
    public string Name {
        get => "cast random spell";
    }
    
    public void Execute(Entity self) {
        self.CastRandomSpell();
    }
}


class UseItem : ICommand {
    public string Name {
        get => "use item";
    }
    
    public void Execute(Entity self) {
        self.UseItem();
    }
}

class UseRandomItem : ICommand {
    public string Name {
        get => "use random item";
    }

    public void Execute(Entity self) {
        self.UseRandomItem();
    }
}
