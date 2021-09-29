using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace ShepMUDClient
{
    // A command is a dynamic method that allows us to do numerous types of functions based on the parameters it has been assigned.
    // In order to create a command, you first call it's constructor, and determine it's name (sans the ~) and the amount of functions it will perform.
    // After which, you call the AddFunction method.  The first will create a FunctionType for you using a name and list of parameters.
    // The second will add a FunctionType that has already been created to the Command.  Both use the index to determine in what order
    // The functions will occur.  That may be important, so keep it in mind.
    class Command
    {
        FunctionType[] functions;
        public string Name { get; }

        public Command(string name, int functionAmt)
        {
            this.Name = name;
            functions = new FunctionType[functionAmt];
        }

        // We should be able to use the command string for extra stuff later.  Possibly pass it into a variable
        // Which each individual function can parse, if need be.
        public void ExecuteCommand(string com)
        {
            foreach (FunctionType f in functions)
            {
                MethodInfo method = this.GetType().GetMethod(f.methodName);
                if (f.parameters.Length == 0)
                {
                    method.Invoke(this, null);
                    continue;
                }
                method.Invoke(this, f.parameters);
            }
        }

        public void AddFunction(string name, Object[] parameters, int index)
        {
            if (functions.Length - 1 < index)
            {
                return;
            }
            FunctionType f = new FunctionType { parameters = parameters, methodName = name };
            functions[index] = f;
        }

        public void AddFunction(FunctionType function, int index)
        {
            if (this.functions.Length - 1 < index)
            {
                return;
            }
            this.functions[index] = function;
        }

        // Should likely update this for specific channels if need be
        public void WriteToChat(string s)
        {
            Chat.WriteToChannel(s, 1);
        }


    }


    // The FunctionType struct is simple enough - the string determines what the name of the command is (sans the ~), and the object array
    // is a list of parameters.  This array of parameters can be of any type of object (such as arrays themselves, integers, anything).
    // For the method, you should have a number of objects within the parameters array equal to the parameters that the method takes.
    // (Not certain about this, but I'm fairly certain order is important in the parameter array, so make sure the first index lines up
    // to the first parameter, and so forth).
    public struct FunctionType
    {
        public string methodName;
        public Object[] parameters;
    }
}
