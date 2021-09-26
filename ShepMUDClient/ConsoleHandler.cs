using ShepMUDClient;
using System;
using System.Text;

public class ConsoleHandler
{


    Window1 window; //Stores reference to window to update textblock
    int logSize = 22; //should make dynamic based on size of console
    string[] messageLog; //for display
    string logDisplay;

    private bool didUpdate;


    public ConsoleHandler(Window1 mainWindow)
    {
        window = mainWindow;

        messageLog = new string[logSize];

        for(int i = 0; i < logSize; i++){
            messageLog[i] = " ";
        }

        formatTextblock();

        
        //Fills console for no reason
        //for(int i = 0; i < logSize; i++)
        //{
        //    writeLine("Message : " + i);
        //}

    }


    private void formatTextblock()
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


    public void getUpdate()
    {
        didUpdate = false;
        window.ChangeText(logDisplay);
    }

    public bool IsConsoleUpdated()
    {
        return didUpdate;
    }


}
