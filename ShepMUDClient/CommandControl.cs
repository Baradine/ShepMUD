using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ShepMUDClient
{
    static class CommandControl
    {
        static Command[] commandList;
        static Command[] defaultCommands =
        {
            new Command("SetIP", "SetServerIP", null, 2),
            new Command("Connect", "ConnectToServer", null, 0)
        };
        static Dictionary<string, Command> shortcuts;
        public static void HandleCommand(string c)
        {
            string command = ParseCommand(c);
            foreach (Command com in commandList)
            {
                if (com == null)
                {
                    continue;
                }
                if (com.Name == command)
                {
                    com.ExecuteCommand(c);
                    break;
                }
            }
            //check our dictionary for common spelling mistakes and shortcuts
            try
            {
                shortcuts[command.ToLower()].ExecuteCommand(c);
            }
            catch (Exception e)
            {
                // Write an error to chat
            }
        }

        public static string ParseCommand(string c)
        {
            List<char> temp = new List<char>();

            foreach (char l in c)
            {
                if (l == (char)32)
                {
                    break;
                }
                if (l == '~')
                {
                    continue;
                }
                temp.Add(l);
            }
            char[] command = temp.ToArray();
            string s = new string(command);
            return s;
        }

        public static void InitCommands()
        {
            commandList = new Command[1000];
            commandList[10] = SetupMoveCommand();
            for (int i = 0; i < defaultCommands.Length; i++)
            {
                
                commandList[i] = defaultCommands[i];
            }


            shortcuts = new Dictionary<string, Command>();
            setupShortcutDictionary();
        }

        private static void setupShortcutDictionary()
        {
            shortcuts.Add("move", commandList[10]);
            shortcuts.Add("mv", commandList[10]);
        }

        private static Command SetupMoveCommand()
        {
            FunctionType move = new FunctionType { methodName = "MoveEntity", defaultParameters = new Object[] { Player.creature.region, null, Player.creature, (uint)0, true }, 
                                                   parameterMask = 29};
            // Movement Dictionary
            Object[] moveDict = new Object[]
                {
                    new Object[] { "North", new int[] { 0, 1 }},
                    new Object[] { "Northeast", new int[] { 1, 1 }},
                    new Object[] { "East", new int[] { 1, 0 }},
                    new Object[] { "Southeast", new int[] { 1, -1 }},
                    new Object[] { "South", new int[] { 0, -1 }},
                    new Object[] { "Southwest", new int[] { -1, -1 }},
                    new Object[] { "West", new int[] { -1, 0 }},
                    new Object[] { "Northwest", new int[] { -1, 1 }},
                    new Object[] { "n", new int[] { 0, 1 }},
                    new Object[] { "ne", new int[] { 1, 1 }},
                    new Object[] { "e", new int[] { 1, 0 }},
                    new Object[] { "se", new int[] { 1, -1 }},
                    new Object[] { "s", new int[] { 0, -1 }},
                    new Object[] { "sw", new int[] { -1, -1 }},
                    new Object[] { "w", new int[] { -1, 0 }},
                    new Object[] { "nw", new int[] { -1, 1 }},
                    new Object[] { "up", new int[] { 0, 1 }},
                    new Object[] { "right", new int[] { 1, 0 }},
                    new Object[] { "down", new int[] { 0, -1 }},
                    new Object[] { "left", new int[] { -1, 0 }},
                };
            FunctionType tParam = new FunctionType { methodName = "ParameterDictionary", defaultParameters = new Object[]{ moveDict, move, (uint)2, false  }, paramCount = 1, 
                                                     parameterMask = 29};

            return new Command("Move", new FunctionType[] { tParam, move});
        }
        
    }
}
