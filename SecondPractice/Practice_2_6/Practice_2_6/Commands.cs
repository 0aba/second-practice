using System.Globalization;
using Practice_2_6.DB;
using Npgsql;


namespace Practice_2_6;


internal static class Сommands
{
    public static void RunСommand(string command)
    {
        Console.Clear();

       List<string> commandData = Utils.GetDataFromString(command);

        switch (commandData[0])
        {
            case "help":
                Help();
                break;

            case "help-command":
                if (commandData.Count != 2)
                {
                    Console.WriteLine("Ошибка количества аргументов");
                    return;
                }

                HelpСommand(commandData[1]);
                break;

            case "exit":
                Environment.Exit(Utils.EXIT_COMMAND);
                break;

            case "add-note":
                if (commandData.Count != 4)
                {
                    Console.WriteLine("Ошибка количества аргументов");
                    return;
                }

                _AddNote(commandData[1].Substring(1, commandData[1].Length - 2),
                        commandData[2].Substring(1, commandData[2].Length - 2),
                        commandData[3]);

                break;

            case "delete-note":
                if (commandData.Count != 2)
                {
                    Console.WriteLine("Ошибка количества аргументов");
                    return;
                }
                try
                {
                    int.Parse(commandData[1]);
                }
                catch
                { 
                    Console.WriteLine("index not number");
                    return;
                }

                _DeleteNote(int.Parse(commandData[1]));
                break;

            case "notes":
                _PrintNotes();
                break;

            case "update-note":
                if (commandData.Count != 5)
                { 
                    Console.WriteLine("Number of arguments error");
                    return;
                }
                try
                {
                    int.Parse(commandData[1]);
                }
                catch
                { 
                    Console.WriteLine("index not number");
                    return;
                }

                _UpdateNote(int.Parse(commandData[1]),
                            commandData[2].Substring(1, commandData[2].Length - 2),
                               commandData[3].Substring(1, commandData[3].Length - 2),
                               commandData[4]);
                break;

            case "clear-notes":
                _ClearNotes();
                break;

            case "completed-notes":
                _PrintCompletedNotes();
                break;

            case "overdue-notes":
                _PrintOverdueNotes();
                break;

            case "incompleted-notes":
                _PrintInCompletedNotes();
                break;

            case "about-note":
                if (commandData.Count != 2)
                {
                    Console.WriteLine("Ошибка количества аргументов");
                    return;
                }
                try
                {
                    int.Parse(commandData[1]);
                }
                catch
                { 
                    Console.WriteLine("index not number");
                    return;
                }
                
                Utils.AboutNote(int.Parse(commandData[1]));
                break;

            case "notes-today":
                _PrintNotesToday();
                break;

            case "notes-tomorrow":
                _PrintNotesTomorrow();
                break;

            case "notes-toweek":
                _PrintNotesToweek();
                break;

            case "mark-completed":
                if (commandData.Count != 2)
                {
                    Console.WriteLine("Ошибка количества аргументов");
                    return;
                }
                try
                {
                    int.Parse(commandData[1]);
                }
                catch
                { 
                    Console.WriteLine("index not number");
                    return;
                }

                _MarkCompleted(int.Parse(commandData[1]));
                break;

            case "logout":
                Authorization.LogoutApp();
                break;
            
            default:
                Console.WriteLine("Такой команды нету воспользуйтесь командой 'help'");
                break;
        }
    }

    // ---------------------------------------------------------------КОМАНДЫ---------------------------------------------------------------

    private static void Help()
    {
        Console.WriteLine("Command info:\n\n" +
                          "'help': сommand info \n\n" +
                          "'help-command': Information about the command and its syntax \n\n" +
                          "'exit': exit program \n\n" +
                          "'add-note': add note \n\n" +
                          "'delete-note': delete note \n\n" +
                          "'clear-notes': delete all notes \n\n" +
                          "'update-note': update note \n\n" +
                          "'notes': get all notes \n\n" +
                          "'completed-notes': get all completed notes \n\n" +
                          "'overdue-notes': get all overdue notes \n\n" +
                          "'incompleted-notes': get all in completed notes \n\n" +
                          "'about-note': about note \n\n" +
                          "'notes-today': get notes on today \n\n" +
                          "'notes-tomorrow': get notes on tomorrow \n\n" +
                          "'notes-toweek': get notes on to week \n\n" +
                          "'logout': logout from account \n\n" +
                          "'mark-completed': make completed note \n\n"
                          );
    }

    private static void HelpСommand(string command)
    {
        string? commandInfo = Utils.commandsInfo.GetValueOrDefault(command);

        Console.WriteLine("Additional info: \n" +
                          "NONE - It's a special word that means to leave it unchangedй \n" +
                          "(See help-command in which command arguments you can use) \n" +
                          "\"(...)\" - This means that the information must be recorded in these double forgings \" \" даже если это одно слово \n" +
                          "(See help-command in which command arguments you can use) \n");
        Console.WriteLine("Command Syntax: ");

        Console.WriteLine(commandInfo is null ? "Error: no such command" : commandInfo);
    }

    private static void _AddNote(string title, string text, string timeEnd)
    {
        DateTime parsedTime;
        
        try
        { 
            parsedTime = DateTime.ParseExact(timeEnd, "dd/MM/yyyy-HH:mm:ss", CultureInfo.InvariantCulture);
        }
        catch 
        {
            Console.WriteLine("Error entered date: usage form (dd/MM/yyyy-HH:mm:ss) example: 22/12/2023-11:05:00");
            Console.ReadKey(true);
            return;
        }

        try
        {
            DatabaseRequests.AddNote(title, text, parsedTime);
            Console.WriteLine("successfully created");
        }
        catch (NpgsqlException error)
        {
            Console.WriteLine($"Sql state: {error.Data["SqlState"]}\n" +
                              $"Message text; {error.Data["MessageText"]}\n" +
                              $"Enter for continue...");
        }
        
        Console.ReadKey(true);
    }

    private static void _UpdateNote(int idNote, string title, string text, string timeEnd)
    {
        string newTitle, newText;
        DateTime newTimeEnd;

        using var note = DatabaseRequests.GetNotes(idNote);

        newTitle = title.Equals("NONE") ? note.GetString(1): title;

        newText = text.Equals("NONE") ? note.GetString(2) : text;

        try
        {
            newTimeEnd = timeEnd.Equals("NONE") ? 
                DateTime.ParseExact(note.GetString(3), "dd/MM/yyyy-HH:mm:ss", CultureInfo.InvariantCulture): 
                DateTime.ParseExact(timeEnd, "dd/MM/yyyy-HH:mm:ss", CultureInfo.InvariantCulture);
        }
        catch 
        {
            Console.WriteLine("Error entered date: usage form (dd/MM/yyyy-HH:mm:ss) example: 22/12/2023-11:05:00");
            Console.ReadKey(true);
            return;
        }
        
        try
        {
            DatabaseRequests.UpdateNote(idNote, newTitle, newText, newTimeEnd);
            Console.WriteLine("successfully update");
        }
        catch (NpgsqlException error)
        {
            Console.WriteLine($"Sql state: {error.Data["SqlState"]}\n" +
                              $"Message text; {error.Data["MessageText"]}\n" +
                              $"Enter for continue...");
        }
        
        Console.ReadKey(true);
    }

    private static void _DeleteNote(int idNote)
    {
        try
        {
            DatabaseRequests.DeleteNote(idNote);
            Console.WriteLine("successfully delete");
        }
        catch (NpgsqlException error)
        {
            Console.WriteLine($"Sql state: {error.Data["SqlState"]}\n" +
                              $"Message text; {error.Data["MessageText"]}\n" +
                              $"Enter for continue...");
        }
        
        Console.ReadKey(true);
    }

    public static void _ClearNotes()
    {
        try
        {
            DatabaseRequests.СlearNote();
            Console.WriteLine("successfully clear");
        }
        catch (NpgsqlException error)
        {
            Console.WriteLine($"Sql state: {error.Data["SqlState"]}\n" +
                              $"Message text; {error.Data["MessageText"]}\n" +
                              $"Enter for continue...");
        }
        
        Console.ReadKey(true);
    }

    private static void  __PrintQueryNotes(NpgsqlDataReader notes)
    {
        while (notes.Read())
        {
            Utils.PrintNoteTitle(notes.GetInt32(0), notes.GetString(2), notes.GetDateTime(4));

            Console.WriteLine();
        }
        
        notes.Close();
    }


    private static void _PrintNotes()
    {
        
        try
        {
            using var notes = DatabaseRequests.GetQueryNotes();
            __PrintQueryNotes(notes);
        }
        catch (NpgsqlException error)
        {
            Console.WriteLine($"Sql state: {error.Data["SqlState"]}\n" +
                              $"Message text; {error.Data["MessageText"]}\n" +
                              $"Enter for continue...");
        }
        
        Console.ReadKey(true);
        
        
        
    }

    private static void _PrintCompletedNotes()
    { 
        try
        {
            using var notes = DatabaseRequests.GetQueryCompletedNotes();
            __PrintQueryNotes(notes);
        }
        catch (NpgsqlException error)
        {
            Console.WriteLine($"Sql state: {error.Data["SqlState"]}\n" +
                              $"Message text; {error.Data["MessageText"]}\n" +
                              $"Enter for continue...");
        }
        
        Console.ReadKey(true);
    }

    private static void _PrintOverdueNotes()
    {
        try
        {
            using var notes = DatabaseRequests.GetQueryOverdueNotes();
            __PrintQueryNotes(notes);
        }
        catch (NpgsqlException error)
        {
            Console.WriteLine($"Sql state: {error.Data["SqlState"]}\n" +
                              $"Message text; {error.Data["MessageText"]}\n" +
                              $"Enter for continue...");
        }
        
        Console.ReadKey(true);
    }

    private static void _PrintInCompletedNotes()
    {
        try
        {
            using var notes = DatabaseRequests.GetQueryInCompletedNotes();
            __PrintQueryNotes(notes);
        }
        catch (NpgsqlException error)
        {
            Console.WriteLine($"Sql state: {error.Data["SqlState"]}\n" +
                              $"Message text; {error.Data["MessageText"]}\n" +
                              $"Enter for continue...");
        }
            
        Console.ReadKey(true);
    }

    private static void _PrintNotesToday()
    {
        try
        {
            using var notes = DatabaseRequests.GetQueryNotesOnDateBetweenDates(DateTime.Now, DateTime.Now);
            __PrintQueryNotes(notes);
        }
        catch (NpgsqlException error)
        {
            Console.WriteLine($"Sql state: {error.Data["SqlState"]}\n" +
                              $"Message text; {error.Data["MessageText"]}\n" +
                              $"Enter for continue...");
        }
                
        Console.ReadKey(true);
    }

    private static void _PrintNotesTomorrow()
    {
        try
        {
            using var notes = DatabaseRequests.GetQueryNotesOnDateBetweenDates(DateTime.Now.AddDays(1), DateTime.Now.AddDays(1));
            __PrintQueryNotes(notes);
        }
        catch (NpgsqlException error)
        {
            Console.WriteLine($"Sql state: {error.Data["SqlState"]}\n" +
                              $"Message text; {error.Data["MessageText"]}\n" +
                              $"Enter for continue...");
        }
                
        Console.ReadKey(true);
    }

    private static void _PrintNotesToweek()
    {
        try
        {
            using var notes = DatabaseRequests.GetQueryNotesOnDateBetweenDates(DateTime.Now, DateTime.Now.AddDays(7));
            __PrintQueryNotes(notes);
        }
        catch (NpgsqlException error)
        {
            Console.WriteLine($"Sql state: {error.Data["SqlState"]}\n" +
                              $"Message text; {error.Data["MessageText"]}\n" +
                              $"Enter for continue...");
        }
                
        Console.ReadKey(true);
    }

    private static void _MarkCompleted(int pkNotes)
    {
        try
        {
            DatabaseRequests.MarkCompletedNotes(pkNotes);
        }
        catch (NpgsqlException error)
        {
            Console.WriteLine($"Sql state: {error.Data["SqlState"]}\n" +
                              $"Message text; {error.Data["MessageText"]}\n" +
                              $"Enter for continue...");
        }
                
        Console.ReadKey(true);
    }
}