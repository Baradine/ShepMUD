using System;

public class ConsoleHandler
{

    public string bindtext { get; set; }
    private static ConsoleHandler handler;

    public ConsoleHandler()
    {
        if(handler == null)
        {
            handler = this;
        }
        //bindtext = "Text will be displayed in this window";
    }

    public string getText()
    {
        return bindtext;
    }

    public static Object getHandler()
    {
        return handler;
    }

}
