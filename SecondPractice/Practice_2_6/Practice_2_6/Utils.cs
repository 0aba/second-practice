using Npgsql;
using Practice_2_6.DB;

namespace Practice_2_6;


internal static class Utils
{
    private const int NOT_DEFIND = -1;

    public const int EXIT_COMMAND = 0;
    
    private static short wight = 100;

    public static Dictionary<string, string> commandsInfo = new Dictionary<string, string>()
    {
        {"help", "help"},
        {"help-command", "help-command (command)"},
        {"logout", "logout"},
        {"exit", "exit"},
        {"add-note", "add-note \"(title)\" \"(text)\" (date time in format 'dd/MM/yyyy-HH:mm:ss' example: 22/04/2023-11:45:23)"},
        {"delete-note", "delete-note \"(id note)\""},
        {"clear-notes", "clear-notes"},
        {"update-note", "update-note (id note) \"(title / NONE)\" \"(text / NONE)\" (date time in format 'dd/MM/yyyy-HH:mm:ss' example: 22/04/2023-11:45:23 / NONE)"},
        {"completed-notes", "completed-notes"},
        {"overdue-notes", "overdue-notes"},
        {"incompleted-notes", "incompleted-notes"},
        {"about-note", "about-note \"(id note)\""},
        {"notes", "notes"},
        {"notes-today", "notes-today"},
        {"notes-tomorrow", "notes-tomorrow"},
        {"notes-toweek", "notes-toweek"},
        {"mark-completed", "mark-completed \"(id note)\""}
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

    public static void PrintNoteTitle(int pkNote,string title, DateTime timeEnd)
    {
        Console.WriteLine("----------------------------------------------------------------------------------------------------\n" +
                          string.Format("|                                        pk: {0,10}                                            |\n", pkNote));
        
        Console.WriteLine("----------------------------------------------------------------------------------------------------");
        Console.Write("|");

        Console.SetCursorPosition((wight - title.Length) / 2, Console.CursorTop);
        Console.Write(title);
        Console.SetCursorPosition(wight - 1, Console.CursorTop);

        Console.WriteLine("|");
        Console.WriteLine("----------------------------------------------------------------------------------------------------\n" +
                         string.Format("|                                       {0,20}                                       |\n", timeEnd) +
                         "----------------------------------------------------------------------------------------------------");
    }
    
    public static void AboutNote(int pkNote)
    {
        try
        {
            using var note = DatabaseRequests.GetNotes(pkNote);
            if (note.Read())
            {
                PrintNoteTitle(note.GetInt32(0),note.GetString(2), note.GetDateTime(4));
                PrintBodyNote(note.GetString(3), note.GetBoolean(5));
            }
            
        }
        catch (NpgsqlException error)
        {
            Console.WriteLine($"Sql state: {error.Data["SqlState"]}\n" +
                              $"Message text; {error.Data["MessageText"]}\n" +
                              $"Enter for continue...");
        }
    }
    
    public static void PrintBodyNote(string text, bool complited)
    {
        Console.Write("|");
        byte countCharInLine = 0;

        foreach (char chr in text)
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
                          string.Format("|                                              {0,2} выполнена                                        |", complited ? "  ": "не") +
                          "\n----------------------------------------------------------------------------------------------------");
    }
}