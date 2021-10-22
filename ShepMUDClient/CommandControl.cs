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
            for (int i = 0; i < defaultCommands.Length; i++)
            {
                commandList[i] = defaultCommands[i];
            }
        }

        
    }
}
