using ConsoleGame1.Core;


namespace ConsoleGame1.Interfaces;


class CastSpellCommand : IExecutable {
    public string Name {
        get => "cast spell";
    }
    
    public Result Execute(Entity self, Entity target) {
        return self.CastSpell();
    }
}

class CastRandomSpellCommand : IExecutable {
    public string Name {
        get => "cast random spell";
    }
    
    public Result Execute(Entity self, Entity target) {
        return self.CastRandomSpell();
    }
}


class UseItemCommand : IExecutable {
    public string Name {
        get => "use item";
    }
    
    public Result Execute(Entity self, Entity target) {
        return self.UseItem();
    }
}

class UseRandomItemCommand : IExecutable {
    public string Name {
        get => "use random item";
    }

    public Result Execute(Entity self, Entity target) {
        return self.UseRandomItem();
    }
}
