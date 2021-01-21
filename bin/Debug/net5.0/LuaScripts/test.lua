local monster = Monster(extromanso, 15, 15, 10, 4, 6)
local dashBan = GetMonster("Lintromantso")

print("Created instance: " .. monster.Name)
print("Created instance: " .. monster.Health)
print("Extracted instance: " .. dashBan.Name)
print("Extracted instance: " .. dashBan.Health)
print("----------------------------------------------")

local allMonsters = GetAllMonsters()

print(allMonsters)

for key, monster in pairs(allMonsters) do
    print("Killing " .. monster.Name .. " will yield in " .. monster.Experience .. " experience points")
end

-- for monster, value in pairs(allMonsters) do
--     print(value.Name)
-- end