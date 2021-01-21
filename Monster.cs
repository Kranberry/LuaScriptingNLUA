namespace LuaScriptingNLUA
{
    class Monster
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int Experience { get; set; }
        public int PhysicalDamage { get; set; }
        public int Defense { get; set; }

        public Monster(string name, int health, int maxHealth, int exp, int physicalDamage, int defense)
        {
            Name = name;
            Health = health;
            MaxHealth = maxHealth;
            Experience = exp;
            PhysicalDamage = physicalDamage;
            Defense = defense;
        }

        public void SetHealth(int newHealth)
        {
            Health = newHealth;
        }

        public void SetMaxHealth(int newHealth)
        {
            MaxHealth = newHealth;
        }
    }
}
