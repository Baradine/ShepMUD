using ShepMUDClient;
using System;
using System.Text;

class ConsoleHandler
{


    Window1 window; //Stores reference to window to update textblock
    //int logSize = 50; //should make dynamic based on size of console (CHANGED TO BE MAX BACKLOG???)
    //string[] messageLog; //for display
    string logDisplay;

    private Channel currentChannel; //default channel

    private bool didUpdate;


    public ConsoleHandler(Window1 mainWindow)
    {
        window = mainWindow;

        //messageLog = new string[logSize];

        //for(int i = 0; i < logSize; i++){
        //    messageLog[i] = " ";
        //}

        //formatTextblock(); //cant format until currentChannel is set

       

    }


    private void formatTextblock() //This rebuilds the string for the ENTIRE log every time it refeshes....
    {
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < currentChannel.getFilled(); i++)
        {
            //sb.Append(messageLog[i]);
            sb.Append(currentChannel.messageLog[i]);
            sb.Append("\n");
        }

        logDisplay = sb.ToString();
    }

    public void writeLine()
    {
        didUpdate = true;
        //move all the messages up
        //for (int i = 0; i < logSize-1; i++)
        //{
        //messageLog[i] = messageLog[i + 1];
        //}

        //add new message
        //currentChannel.messageLog[currentChannel.messageLog.Length-1] = line; // Cant do this
        formatTextblock();
    }

    public void WriteToCurrentChannel(string line)
    {
        currentChannel.AddToLog(line);
        writeLine();
    }

    //Prefixes the writeline command with the console, makes it easier to edit in the future
    public void writeFromConsole(string line)
    {
        Chat.GetChannel(Channel.GLOBAL).AddMessage("Console: " + line); //This function needs put somewhere else
    }


    public void getUpdate()
    {
        didUpdate = false;
        window.ChangeText(logDisplay);
    }

    public bool IsConsoleUpdated()
    {
        return didUpdate;
    }

    public void SetCurrentChannel(Channel c)
    {
        currentChannel = c;
    }

    //public void executeCommand(string command)
    //{
    //    switch (command) {
    //        case "~test":
    //            writeFromConsole("test");
    //            break;
    //        case "~help":
    //            writeFromConsole("Help command is WIP");
    //            break;

    //        default:
    //            writeFromConsole("Command not found"); 
    //            break;
    //    }

    //}

}
