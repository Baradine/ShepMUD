using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
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
        List<string> parameters;
        public string Name { get; }

        public Command(string name, int functionAmt)
        {
            this.Name = name;
            functions = new FunctionType[functionAmt];
        }

        // Initializes the command with an array of functions
        public Command(string name, FunctionType[] function)
        {
            this.Name = name;
            functions = new FunctionType[function.Length];
            for (int i = 0; i < functions.Length; i++)
            {
                this.AddFunction(function[i], i);
            }
        }

        // Initializes a single function command.  If no default Params desired, pass in an array of length 0
        public Command(string name, string functionName, Object[] p, int pCount)
        {
            this.Name = name;
            functions = new FunctionType[1];

            FunctionType f = new FunctionType { defaultParameters = p, methodName = functionName, paramCount = pCount };
            this.AddFunction(f, 0);
        }

        // parseParameters will parse our command string for parameters in the quotation format (" ")
        // and then pass those parameters in, in order.  Thus, order is imperitive when creating a command
        // so the player can know the proper format in which to use a command.

        // There is also a possibility of default parameters (so we can create programatic attacks and the like)
        // So there are four distinct possibilities for a command: no parameters, user parameters - no default,
        // default parameters - no user, both user and default parameters
        public void ExecuteCommand(string com)
        {
            parseParameters(com, this.Name);
            int index = 0;
            foreach (FunctionType f in functions)
            {
                MethodInfo method = this.GetType().GetMethod(f.methodName);
                if ((f.defaultParameters == null || f.defaultParameters.Length == 0) && f.paramCount == 0)
                {
                    method.Invoke(this, null);
                }
                else if ((f.defaultParameters == null || f.defaultParameters.Length == 0))
                {
                    if (parameters.Count > 0)
                    {
                        string[] p = new string[f.paramCount];
                        for (int i = 0; i < f.paramCount; i++)
                        {
                            p[i] = parameters[index];
                            index++;
                        }
                        method.Invoke(this, p);
                    }
                    else
                    {
                        ParameterInfo[] p = method.GetParameters();
                        Object[] o = new Object[p.Length];
                        method.Invoke(this, o);
                    }
                    
                }
                else if (f.paramCount == 0)
                {
                    method.Invoke(this, f.defaultParameters);
                }
                else
                {
                    string[] p = new string[f.paramCount];
                    for (int i = 0; i < f.paramCount; i++)
                    {
                        p[i] = parameters[index];
                        index++;
                    }
                    method.Invoke(this, functionMask(f.parameterMask, p, f.defaultParameters));
                }
            }
        }

        // Gets our parameters all in order, based on the parameter mask within the function
        private Object[] functionMask(int mask, string[] user, Object[] defaultP)
        {
            int fullParamLength = user.Length + defaultP.Length;
            Object[] fullParam = new object[fullParamLength];
            int Pindex = 0;
            int Uindex = 0;
            int Dindex = 0;
            if (mask == 0 || mask == 1)
            {
                for (int i = 0; i < fullParamLength; i++)
                {
                    if (i < defaultP.Length)
                    {
                        fullParam[i] = defaultP[i];
                    }
                    else
                    {
                        fullParam[i] = user[i - defaultP.Length];
                    }
                }
                return fullParam;
            }
            else
            {
                for (int i = 1; i < MathF.Pow(2, fullParamLength); i *= 2)
                {
                    if ((i & mask) != i)
                    {
                        fullParam[Pindex] = user[Uindex];
                        Pindex++;
                        Uindex++;
                    }
                    else
                    {
                        fullParam[Pindex] = user[Dindex];
                        Pindex++;
                        Dindex++;
                    }
                }
                return fullParam;
            }
        }

        public void AddFunction(string name, Object[] parameters, int index)
        {
            if (functions.Length - 1 < index)
            {
                return;
            }
            FunctionType f = new FunctionType { defaultParameters = parameters, methodName = name };
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

        // Below details our list of various functions that each command can utilize.  There are four main types of functions:
        // Parameterless, user defined parameter functions, default parameter functions, and dual parameter functions.

        // We should try keeping some of these safe, so if a default param might pass in an integer, we first see if it's type
        // integer, then if not, try and parse a int from a string.  If we can't do that, then something has gone wrong and the
        // function doesn't fire.

        // Should likely update this for specific channels if need be
        public void WriteToChat(string s)
        {
            Chat.WriteToChannel(s, 1);
        }

        public void SetServerIP(string ip, string port)
        {
            Main.connect = new NetworkConnection(Int32.Parse(port), ip);
        }

        public void ConnectToServer()
        {
            Thread thread = new Thread(new ThreadStart(Main.connect.Connect));
            thread.Start();
        }

        private void parseParameters(string s, string com)
        {
            parameters = new List<string>();
            int firstIn = 0;
            int lastIn = 0;
            // Ignore the command itself in the string, then find our params in quotes
            for (int i = com.Length; i < s.Length; i++)
            {
                if (s[i] == '"')
                {
                    if (firstIn == 0)
                    {
                        firstIn = i;
                    }
                    else
                    {
                        lastIn = i;
                        parameters.Add(s.Substring(firstIn+1, (lastIn - firstIn - 1)));
                        firstIn = 0;
                        lastIn = 0;
                    }
                    continue;
                }
            }
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
        public Object[] defaultParameters;
        // This is how many user parameters that should be parsed.
        public int paramCount;

        // This is a bitwise integer that determines in what order our parameters should be
        // placed into our function.  A 0 bit means that the parameter is user defined, a 1 bit means
        // that the parameter is default.  A parameter mask that equals 0 or 1 does the default behaviour:
        // default parameters first, then user parameters.

        // Example: we have 4 parameters, the first which is user defined (0), the second which is default (1),
        // the third also user (0), then the last which is default(1).  This gives us a mask of 0101, 5 in other words.
        // since we're using default int32, that means we can have up to 32 parameters, which I feel is more than plenty
        public int parameterMask;
    }
}
