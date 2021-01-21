local monsters = {
    {Name = "Poltis", Health = 35, MaxHealth = 37, Exp = 250, Dmg = 48, Defense = 645},
    {Name = "Ranken", Health = 18, MaxHealth = 25, Exp = 218, Dmg = 65, Defense = 15},
    {Name = "Rat", Health = 218, MaxHealth = 218, Exp = 268, Dmg = 450, Defense = 38},
    {Name = "Demon", Health = 210, MaxHealth = 210, Exp = 148, Dmg = 20, Defense = 895},
    {Name = "Liftoff", Health = 18, MaxHealth = 19, Exp = 12, Dmg = 46, Defense = 74}
}

for key, m in pairs(monsters) do
    local newMonster = Monster(m.Name, m.Health, m.MaxHealth, m.Exp, m.Dmg, m.Defense)
end

