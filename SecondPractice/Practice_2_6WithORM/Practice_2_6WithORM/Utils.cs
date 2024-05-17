using Practice_2_6WithORM.DB;

namespace Practice_2_6WithORM;


internal static class Utils
{
    public const int EXIT_COMMAND = 0;
    
    private static short _wight = 100;

    public static Dictionary<string, string> commandsInfo = new Dictionary<string, string>()
    {
        {"help", "help"},
        {"help-command", "help-command (command)"},
        {"logout", "logout"},
        {"exit", "exit"},
        {"add-note", "add-note \"(title)\" \"(text)\" (date time in format 'dd/MM/yyyy-HH:mm:ss' example: 22/04/2023-11:45:23)"},
        {"delete-note", "delete-note (id note)"},
        {"clear-notes", "clear-notes"},
        {"update-note", "update-note (id note) \"(title / NONE)\" \"(text / NONE)\" (date time in format 'dd/MM/yyyy-HH:mm:ss' example: 22/04/2023-11:45:23 / NONE)"},
        {"completed-notes", "completed-notes"},
        {"overdue-notes", "overdue-notes"},
        {"incompleted-notes", "incompleted-notes"},
        {"about-note", "about-note (id note)"},
        {"notes", "notes"},
        {"notes-today", "notes-today"},
        {"notes-tomorrow", "notes-tomorrow"},
        {"notes-toweek", "notes-toweek"},
        {"mark-completed", "mark-completed (id note)"}
    };
    
    public static List<string> GetDataFromString(string command)
    {
        List<string> commandData = new List<string>();

        command += " ";
        bool notInBrackets = true;

        int startWord = 0;
        int endWord = 0;

        foreach (char chr in command)
        {
            if (chr.Equals('"'))
            {
                notInBrackets = !notInBrackets;
            }

            if (notInBrackets && chr.Equals(' '))
            {
                commandData.Add(command.Substring(startWord, endWord - startWord));

                startWord = endWord + 1;
            }
            ++endWord;
        }

        return commandData;
    }

    public static void PrintNoteTitle(TodoTask todoTask)
    {
        Console.WriteLine("----------------------------------------------------------------------------------------------------\n" +
                          string.Format("|                                        pk: {0,10}                                            |\n", todoTask.Pk));
        
        Console.WriteLine("----------------------------------------------------------------------------------------------------");
        Console.Write("|");

        Console.SetCursorPosition((_wight - todoTask.Title.Length) / 2, Console.CursorTop);
        Console.Write(todoTask.Title);
        Console.SetCursorPosition(_wight - 1, Console.CursorTop);

        Console.WriteLine("|");
        Console.WriteLine("----------------------------------------------------------------------------------------------------\n" +
                         string.Format("|                                       {0,20}                                       |\n", todoTask.TimeEnd) +
                         "----------------------------------------------------------------------------------------------------");
    }
    
    public static void AboutNote(int pkNote)
    {
        try
        {
            var note = DatabaseRequests.GetNote(pkNote);
            foreach (TodoTask n in note)
            {
                PrintNoteTitle(n);
                PrintBodyNote(n);
            }
            
        }
        catch (Microsoft.EntityFrameworkCore.DbUpdateException error)
        {
            Console.WriteLine($"Error: {error.Message}");
        }
    }
    
    public static void PrintBodyNote(TodoTask todoTask)
    {
        Console.Write("|");
        byte countCharInLine = 0;

        foreach (char chr in todoTask.TextTodo)
        {
            Console.Write(chr);

            if (countCharInLine == 100)
            {
                Console.WriteLine("— |");
                Console.Write("|");
                countCharInLine = 0;
            }

            ++countCharInLine;
        }

        Console.WriteLine("\n----------------------------------------------------------------------------------------------------\n" +
            string.Format("|                                              {0,2} выполнена                                        |", todoTask.Complited ? "  ": "не") +
                          "\n----------------------------------------------------------------------------------------------------");
    }
}