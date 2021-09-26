using ShepMUDClient;
using System;
using System.Text;

public class ConsoleHandler
{


    Window1 window; //Stores reference to window to update textblock
    int logSize = 24;
    string[] messageLog; //for display
    string logDisplay;


    public ConsoleHandler(Window1 mainWindow)
    {
        window = mainWindow;

        messageLog = new string[logSize];

        for(int i = 0; i < logSize; i++){
            messageLog[i] = " ";
        }

        formatTextblock();

        //Example of writing to the console
        writeLine("testing writing to console");
        writeLine("testing writing to console");
        writeLine("testing writing to console");
        writeLine("This should be the fourth message"); //notice these get overriden by the next loop
        for(int i = 0; i < logSize; i++)
        {
            writeLine("Message : " + i);
        }
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

        //move all the messages up
        for(int i = 0; i < logSize-1; i++)
        {
            messageLog[i] = messageLog[i + 1];
        }
        
        //add new message
        messageLog[logSize-1] = line;

        formatTextblock();
    }


    public void getUpdate()
    {
        window.ChangeText(logDisplay);
    }


}
