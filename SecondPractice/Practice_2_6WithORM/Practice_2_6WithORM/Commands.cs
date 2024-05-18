using System.Globalization;


using Practice_2_6WithORM.DB;

namespace Practice_2_6WithORM;


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
                          "\"(...)\" - This means that the information must be recorded in these double forgings \" \" even if it's one word\n" +
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

        if (parsedTime < DateTime.Now)
        {
            Console.WriteLine("Error date: the note is already overdue\n");
            Console.ReadKey(true);
            return;
        }
        
        try
        {
            DatabaseRequests.AddNote(new TodoTask()
            {
                UserPk = DatabaseService.LoggedUser.Pk,
                User = DatabaseService.LoggedUser,
                Title = title,
                TextTodo = text,
                TimeEnd = parsedTime
            });
            Console.WriteLine("successfully created");
        }
        catch (Microsoft.EntityFrameworkCore.DbUpdateException error)
        {
            Console.WriteLine($"Error: {error.Message}");
        }
        
        Console.ReadKey(true);
    }

    private static void _UpdateNote(int idNote, string title, string text, string timeEnd)
    {
        TodoTask updateTodoTask = new TodoTask()
        {
            Pk = idNote,
            Title = title,
            TextTodo = text,
            TimeEnd = DateTime.Now
        };

        TodoTask oldNote = DatabaseRequests.GetNote(idNote).ToList().First();

        updateTodoTask.Title = title.Equals("NONE") ? oldNote.Title: title;

        updateTodoTask.TextTodo = text.Equals("NONE") ? oldNote.TextTodo: text;

        try
        {
            updateTodoTask.TimeEnd = timeEnd.Equals("NONE") ? oldNote.TimeEnd: 
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
            DatabaseRequests.UpdateNote(idNote, updateTodoTask);
            Console.WriteLine("successfully update");
        }
        catch (Microsoft.EntityFrameworkCore.DbUpdateException error)
        {
            Console.WriteLine($"Error: {error.Message}");
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
        catch (Microsoft.EntityFrameworkCore.DbUpdateException error)
        {
            Console.WriteLine($"Error: {error.Message}");
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
        catch (Microsoft.EntityFrameworkCore.DbUpdateException error)
        {
            Console.WriteLine($"Error: {error.Message}");
        }
        
        Console.ReadKey(true);
    }

    private static void  __PrintQueryNotes(IQueryable<TodoTask> notes)
    {
        foreach (TodoTask note in notes)
        {
            Utils.PrintNoteTitle(note);

            Console.WriteLine();
        }
    }


    private static void _PrintNotes()
    {
        try
        {
            IQueryable<TodoTask> notes = DatabaseRequests.GetQueryNotes();
            __PrintQueryNotes(notes);
        }
        catch (Microsoft.EntityFrameworkCore.DbUpdateException error)
        {
            Console.WriteLine($"Error: {error.Message}");
        }
        
        Console.ReadKey(true);
    }

    private static void _PrintCompletedNotes()
    { 
        try
        {
            IQueryable<TodoTask> notes = DatabaseRequests.GetQueryCompletedNotes();
            __PrintQueryNotes(notes);
        }
        catch (Microsoft.EntityFrameworkCore.DbUpdateException error)
        {
            Console.WriteLine($"Error: {error.Message}");
        }
        
        Console.ReadKey(true);
    }

    private static void _PrintOverdueNotes()
    {
        try
        {
            IQueryable<TodoTask> notes = DatabaseRequests.GetQueryOverdueNotes();
            __PrintQueryNotes(notes);
        }
        catch (Microsoft.EntityFrameworkCore.DbUpdateException error)
        {
            Console.WriteLine($"Error: {error.Message}");
        }
        
        Console.ReadKey(true);
    }

    private static void _PrintInCompletedNotes()
    {
        try
        {
            IQueryable<TodoTask> notes = DatabaseRequests.GetQueryInCompletedNotes();
            __PrintQueryNotes(notes);
        }
        catch (Microsoft.EntityFrameworkCore.DbUpdateException error)
        {
            Console.WriteLine($"Error: {error.Message}");
        }
            
        Console.ReadKey(true);
    }

    private static void _PrintNotesToday()
    {
        try
        {
            IQueryable<TodoTask> notes = DatabaseRequests.GetQueryNotesOnDateBetweenDates(
                new DateTime(DateTime.Now.Year, 
                    DateTime.Now.Month, 
                    DateTime.Now.Day, 
                    0,
                    0,
                    1), 
                new DateTime(DateTime.Now.Year, 
                    DateTime.Now.Month, 
                    DateTime.Now.Day, 
                    23,
                    59,
                    59));
            __PrintQueryNotes(notes);
        }
        catch (Microsoft.EntityFrameworkCore.DbUpdateException error)
        {
            Console.WriteLine($"Error: {error.Message}");
        }
                
        Console.ReadKey(true);
    }

    private static void _PrintNotesTomorrow()
    {
        try
        {
            IQueryable<TodoTask> notes = DatabaseRequests.GetQueryNotesOnDateBetweenDates(
                new DateTime(DateTime.Now.Year, 
                    DateTime.Now.Month, 
                    DateTime.Now.Day, 
                    0,
                    0,
                    1).AddDays(1), 
                new DateTime(DateTime.Now.Year, 
                    DateTime.Now.Month, 
                    DateTime.Now.Day, 
                    23,
                    59,
                    59).AddDays(1));
            __PrintQueryNotes(notes);
        }
        catch (Microsoft.EntityFrameworkCore.DbUpdateException error)
        {
            Console.WriteLine($"Error: {error.Message}");
        }
                
        Console.ReadKey(true);
    }

    private static void _PrintNotesToweek()
    {
        try
        {
            IQueryable<TodoTask> notes = DatabaseRequests.GetQueryNotesOnDateBetweenDates(
                new DateTime(DateTime.Now.Year, 
                    DateTime.Now.Month, 
                    DateTime.Now.Day, 
                    0,
                    0,
                    1), 
                new DateTime(DateTime.Now.Year, 
                    DateTime.Now.Month, 
                    DateTime.Now.Day, 
                    23,
                    59,
                    59).AddDays(7)
                );
            __PrintQueryNotes(notes);
        }
        catch (Microsoft.EntityFrameworkCore.DbUpdateException error)
        {
            Console.WriteLine($"Error: {error.Message}");
        }
                
        Console.ReadKey(true);
    }

    private static void _MarkCompleted(int pkNotes)
    {
        try
        {
            DatabaseRequests.MarkCompletedNotes(pkNotes);
            Console.WriteLine("successfully set completed");
        }
        catch (Microsoft.EntityFrameworkCore.DbUpdateException error)
        {
            Console.WriteLine($"Error: {error.Message}");
        }
        Console.ReadKey(true);
    }
}