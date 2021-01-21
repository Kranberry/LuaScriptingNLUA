using System;

namespace LuaScriptingNLUA
{
    class Program
    {
        static void Main(string[] args)
        {
            LuaScriptInterface luaScriptInterface = new();
            // Load all scripts
            luaScriptInterface.LoadLuaFunctions();
            luaScriptInterface.LoadScripts();

            while (true)
            {
                Console.WriteLine("press enter to run all scripts, or enter said script and press enter. Load to load new scripts");
                string choosenScript = Console.ReadLine();
                if (choosenScript.ToLower() == "load")
                {
                    luaScriptInterface.LoadLuaFunctions();
                    luaScriptInterface.LoadScripts();
                }
                else if (choosenScript.Length > 0)
                    luaScriptInterface.RunScript(choosenScript);
                else
                    luaScriptInterface.RunScript();
            }


        }
    }
}
