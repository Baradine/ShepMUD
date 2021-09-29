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
        public static void HandleCommand(string c)
        {
            string command = ParseCommand(c);
            foreach (Command com in commandList)
            {
                if (com.Name == command)
                {
                    com.ExecuteCommand();
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

            FunctionType f = new FunctionType { parameters = new object[1] {"test"}, methodName = "WriteToChat" };
            Command c = new Command("test", 1);
            c.AddFunction(f, 0);

            commandList[0] = c;
        }
    }
}
