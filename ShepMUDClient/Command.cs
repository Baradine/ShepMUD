using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Reflection;

namespace ShepMUDClient
{


    /// <summary>
    ///     A command is a dynamic method that allows us to do numerous types of functions based on the parameters it has been assigned.
    /// In order to create a command, you first call it's constructor, and determine it's name(sans the ~) and the amount of functions it will perform.
    /// After which, you call the AddFunction method. The first will create a FunctionType for you using a name and list of parameters.
    /// The second will add a FunctionType that has already been created to the Command. Both use the index to determine in what order
    /// the functions will occur.
    /// </summary>
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
        /// <summary>
        /// Executes the command, taking in the parsed string com as arguments to pass into the various command functions defined.
        /// </summary>
        /// <param name="com">The full string inputted by the user to execute the command.</param>
        public void ExecuteCommand(string com)
        {
            ParseParameters(com, this.Name);
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


        /// <summary>
        /// Gets our parameters all in order, based on the parameter mask within the function
        /// </summary>
        /// <param name="mask">A bitwise integer used to determine the order of our parameters</param>
        /// <param name="user">An array of user defined parameters</param>
        /// <param name="defaultP">An array of parameters passed in by code</param>
        /// <returns>The properly sorted array of parameters, for use in a MethodInfo.Invoke function</returns>
        private Object[] functionMask(uint mask, string[] user, Object[] defaultP)
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
                for (uint i = 1; i < MathF.Pow(2, fullParamLength); i *= 2)
                {
                    if ((i & mask) != i)
                    {
                        fullParam[Pindex] = user[Uindex];
                        Pindex++;
                        Uindex++;
                    }
                    else
                    {
                        fullParam[Pindex] = defaultP[Dindex];
                        Pindex++;
                        Dindex++;
                    }
                }
                return fullParam;
            }
        }

        /// <summary>
        /// Creates a new function for this command, and adds it to the specificied index in the function list.
        /// </summary>
        /// <param name="name">The name of the function (must be an actual function, like the WriteToChat method defined below)</param>
        /// <param name="parameters">An array of default parameters</param>
        /// <param name="index">Where to add the array in the command list (later entries are executed last)</param>
        public void AddFunction(string name, Object[] parameters, int index)
        {
            if (functions.Length - 1 < index)
            {
                return;
            }
            FunctionType f = new FunctionType { defaultParameters = parameters, methodName = name };
            functions[index] = f;
        }

        /// <summary>
        /// Adds a new, predefined function, to the command's function list, at the specified index.
        /// </summary>
        /// <param name="function">The FunctionType to add to the command list.  Ideally, it should have the parameter types and mask already defined</param>
        /// <param name="index">Where to add the function in the command list</param>
        public void AddFunction(FunctionType function, int index)
        {
            if (this.functions.Length - 1 < index)
            {
                return;
            }
            this.functions[index] = function;
        }

        /* 
           Below details our list of various functions that each command can utilize.  There are four main types of functions:
           Parameterless, user defined parameter functions, default parameter functions, and dual parameter functions.

           We should try keeping some of these safe, so if a default param might pass in an integer, we first see if it's type
           integer, then if not, try and parse a int from a string.  If we can't do that, then something has gone wrong and the
           function doesn't fire. 
        */


        public void WriteToChat(string s)
        {
            Chat.WriteToChannel(s, 1);  //Need to update this for writing to a specific channel instead of just global
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

        /// <summary>
        /// Moves an entity from one tile to another, in the specified region.  Requires the proper permissions.
        /// </summary>
        /// <param name="region">The region in which the entity will be moved to</param>
        /// <param name="xyPos">The tile at which our entity will be moved to</param>
        /// <param name="entity">The entity to move</param>
        /// <param name="permissionID">The permissions to see if we have control over this object.</param>
        public void MoveEntity(Region region, int[] xyPos, Entity entity, uint permissionID, bool relative)
        {
            bool controlOwner = false;
            // first, check if we have permission to do so
            foreach (uint i in entity.permission.ownerIDs)
            {
                if (i == permissionID)
                {
                    controlOwner = true;
                }
            }
            if ((permissionID == 0) || (entity.permission.publicKey == (int)Permission.PermissionKey.Control) || controlOwner)
            {
                if (!relative)
                {
                    if (xyPos[0] > region.tileGrid.GetLength(0)-1 || xyPos[0] < 0 || xyPos[1] > region.tileGrid.GetLength(1)-1 || xyPos[1] < 0)
                    {
                        return; // Replace this with an error chat message at some point
                    }
                    entity.xPos = xyPos[0];
                    entity.yPos = xyPos[1];
                    region.tileGrid[entity.xPos, entity.yPos].currentEntities.Add(entity);
                }
                else
                {
                    if (xyPos[0] + entity.xPos > region.tileGrid.GetLength(0)-1 || xyPos[0] + entity.xPos < 0 || 
                        xyPos[1] + entity.yPos > region.tileGrid.GetLength(1)-1 || xyPos[1] + entity.yPos < 0)
                    {
                        return; // Replace this with an error chat message at some point
                    }
                    entity.xPos += xyPos[0];
                    entity.yPos += xyPos[1];
                    region.tileGrid[entity.xPos, entity.yPos].currentEntities.Add(entity);
                }
            }
        }

        /// <summary>
        /// Given a dictionary, will translate the input using the desired dictionary and use it as the input for another function.  This function *must* be
        /// called before the given function in the command, otherwise this will do nothing.
        /// </summary>
        /// <param name="dict">The array that functions as a dictionary.  Each index should be an array with length 2, the first index of that array
        ///  dictating the user input, the second the translated term.</param>
        /// <param name="input">The user input parameter</param>
        /// <param name="t">The function in which we're passing our parameter</param>
        /// <param name="maskBit">Determines where this function will be placed in our parameters list, using the function mask.  This will replace
        /// any parameter that currently lies in the mask bit, should there be one.</param>
        /// <param name="sort">By default, we will sort and resize the functions defaultParameter array.  Check this false if the array does not need 
        /// sorting and is properly sized.</param>
        public void ParameterDictionary(Object[] dict, string input, FunctionType t, uint maskBit, bool sort = true)
        {
            foreach (Object[] o in dict)
            {
                if (o[0].GetType() == typeof(string))
                {
                    if ((string)o[0] == input)
                    {
                        // If we have no default parameters, we're done and just slap the new one in there.
                        if (t.defaultParameters == null)
                        {
                            t.defaultParameters = new Object[1] { o[1] };
                            t.parameterMask += maskBit;
                            return;
                        }
                        // Finds the position of our new parameter within the defaultParameter array
                        int index = 0;
                        for (uint i = 1; i <= 4294967295; i *= 2)
                        {
                            if (i == maskBit)
                            {
                                if ((maskBit & t.parameterMask) != 0)  // If we're replacing a parameter, there's no need to sort, we can just replace and go on our way
                                {
                                    t.defaultParameters[index] = o[1];
                                    // No need to add the mask bit since it's already there
                                    return;
                                }
                                break;
                            }
                            if ((i & t.parameterMask) == i)
                            {
                                index++;
                            }
                        }
                        // Checks against our defaultParameter array and our masks to properly sort our parameter within that array.
                        if (sort)
                        {
                            Object[] newArr = new Object[t.defaultParameters.Length + 1];
                            if (index == t.defaultParameters.Length)
                            {
                                for (int i = 0; i < t.defaultParameters.Length; i++)
                                {
                                    newArr[i] = t.defaultParameters[i];
                                }
                                newArr[index] = o[1];
                                t.defaultParameters = newArr;
                                t.parameterMask += maskBit;
                                return;
                            }
                            for (int i = 0; i < t.defaultParameters.Length; i++)
                            {
                                if (i == index)
                                {
                                    newArr[i] = o[1];
                                    newArr[i + 1] = t.defaultParameters[i];
                                }
                                if (i < index)
                                {
                                    newArr[i] = t.defaultParameters[i];
                                }
                                if (i > index)
                                {
                                    newArr[i + 1] = t.defaultParameters[i];
                                }

                            }
                            t.defaultParameters = newArr;
                            t.parameterMask += maskBit;
                            return;
                        }
                        else
                        {
                            t.defaultParameters[index] = o[1];
                            t.parameterMask += maskBit;
                            return;
                        }
                    }
                }
            }
        }

        /* End Function Section */

        /// <summary>
        /// Will parse the parameters out of a string inputted into the client chat box.  These parameters are delimted by quotations, like so: " "
        /// </summary>
        /// <param name="s">The full string that was inputted</param>
        /// <param name="com">The command name, so we can skip a bit for faster parsing</param>
        private void ParseParameters(string s, string com)
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



    /// <summary>
    /// A structure that determines what function is called, and how it is called, within a command.  The methodName string determines what, exactly, the method we are calling is.
    /// The defaultParameters array is our programmed parameters, passed in from code, not user input.  The paramCount determines how many, if any, user inputted parameters we should
    /// expect for this function.  The parameter mask is a bitwise integer used to determine the order of our parameters (0s are user parameters, 1s are default).
    /// </summary>
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
        public uint parameterMask;
    }
}
