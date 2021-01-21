using System;
using System.Collections.Generic;
using System.Linq;
using NLua;
using System.IO;

namespace LuaScriptingNLUA
{
    class LuaScriptInterface
    {
        Lua state;

        private string luaDirectoryPath = "LuaScripts/";
        // Key = filename, value = code
        private Dictionary<string, string> luaFiles;

        Dictionary<string, Monster> monsters = new();

        //private void CreateMonster(string name, int health, int maxHealth, int exp, int physicalDamage, int defense)

        internal void LoadLuaFunctions()
        {
            // Create a new instance of the lua interperter
            state = new Lua();
            state.LoadCLRPackage();

            // Register the methods as functions to the lua instance
                            // Lua function, this instance, this instances method
            state.RegisterFunction("Monster", this, this.GetType().GetMethod("CreateMonster"));
            state.RegisterFunction("GetMonster", this, this.GetType().GetMethod("GetMonster"));
            state.RegisterFunction("GetAllMonsters", this, this.GetType().GetMethod("GetAllMonsters"));
            //LuaScriptInterface LuaObj = new();
            //state["LuaObj"] = LuaObj;
        }

        // Load the scripts
        internal void LoadScripts()
        {
            // Create a dictionary where the filename of the script is the key, and the value is it's code
            luaFiles = new Dictionary<string, string>();
            int scriptCount = Directory.GetFiles(luaDirectoryPath).Length;
            string[] allScripts = Directory.GetFiles(luaDirectoryPath);

            for(int i = 0; i < scriptCount; i++)
            {
                // Remove the path from the script, and keept the name. We do this so we can easily index our dictionary when we runt the script
                string fileName = allScripts[i].Substring(luaDirectoryPath.Length);
                string code = File.ReadAllText(luaDirectoryPath + fileName);
                luaFiles.Add(fileName, code);
            }

            Console.WriteLine("Loaded scripts");
        }

        // Run said script
        internal void RunScript(string scriptName)
        {
            bool hasLuaFileEnd = scriptName.EndsWith(".lua");
            if (hasLuaFileEnd)
            {
                // The scripts can hold any type of error, do not let it crash the main program.
                try
                {
                    state.DoString(luaFiles[scriptName]);
                }
                catch (Exception e)
                {
                    // Send the error out to the user instead, and say what file it was in
                    Console.WriteLine("Error in " + scriptName);
                    Console.WriteLine(e);
                }
            }
            else
            {
                try
                {
                    state.DoString(luaFiles[scriptName + ".lua"]);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error in " + scriptName);
                    Console.WriteLine(e);
                }
            }
        }
        // Run the scripts
        internal void RunScript()
        {
            // The scripts can hold any type of error, do not let it crash the main program.
            foreach(var script in luaFiles)
            {
                try
                {
                    state.DoString(script.Value);
                }
                catch(Exception e)
                {
                    // Send the error out to the user instead, and say what file it was in
                    Console.WriteLine("Error in " + script.Key);
                    Console.WriteLine(e);
                }
            }
        }

        // this will create a Lua table for us, so that we can start building and returning our tables as a table Lua can handle
        private LuaTable CreateTable() => (LuaTable)state.DoString("return {  }")[0];
        
        // Return a LuaTable to the Lua script calling. It will get all monsters created
        public LuaTable GetAllMonsters()
        {
            IEnumerable<Monster> monsterArray = from x in monsters
                                                select x.Value;
            // Create an empty Lua table
            LuaTable table = CreateTable();
            
            // And fill it up with dope data
            for(int i = 1; i <= monsters.Count; i++)
            {
                table[i] = monsterArray.ToArray()[i-1];
            }

            return table;
        }

        // Get said monster from the dictionary of monsters
        public Monster GetMonster(string monsterName)
        {
            Monster monster = monsters[monsterName];
            return monster;
        }

        // Create a new monster
        public Monster CreateMonster(string name, int health, int maxHealth, int exp, int physicalDamage, int defense)
        {
            // Monster(name, health, maxHealth, exp, physicalDamage, defense)
            // Check if this monster already exists. Monster Skeleton, will be the only monster indexed as Skeleton
            // If we were to make a game that might have 3 different skeletons with the same name, the identifiers can and should be different
            // whilst the name property could be the same
            if (monsters.ContainsKey(name))
                return monsters[name];

            Monster monster = new(name, health, maxHealth, exp, physicalDamage, defense);
            monsters.Add(monster.Name, monster);

            return monster;
        }
    }
}
