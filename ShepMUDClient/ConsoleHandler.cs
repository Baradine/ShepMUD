using ShepMUDClient;
using System;
using System.Text;

class ConsoleHandler
{


    Window1 window; //Stores reference to window to update textblock
    int logSize = 50; //should make dynamic based on size of console (CHANGED TO BE MAX BACKLOG???)
    string[] messageLog; //for display
    string logDisplay;

    private Channel currentChannel;

    private bool didUpdate;


    public ConsoleHandler(Window1 mainWindow)
    {
        window = mainWindow;

        messageLog = new string[logSize];

        for(int i = 0; i < logSize; i++){
            messageLog[i] = " ";
        }

        formatTextblock();

       

    }


    private void formatTextblock() //This rebuilds the string for the ENTIRE log every time it refeshes....
    {
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < logSize; i++)
        {
            sb.Append(messageLog[i]);
            sb.Append("\n");
        }

        logDisplay = sb.ToString();
    }

    public void writeLine(string line)
    {
        didUpdate = true;
        //move all the messages up
        for (int i = 0; i < logSize-1; i++)
        {
            messageLog[i] = messageLog[i + 1];
        }
        
        //add new message
        messageLog[logSize-1] = line;
        formatTextblock();
    }

    //Prefixes the writeline command with the console, makes it easier to edit in the future
    public void writeFromConsole(string line)
    {
        writeLine("Console: " + line);
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

    public void executeCommand(string command)
    {
        switch (command) {
            case "~test":
                writeFromConsole("test");
                break;
            case "~help":
                writeFromConsole("Help command is WIP");
                break;

            default:
                writeFromConsole("Command not found"); 
                break;
        }

    }

}
