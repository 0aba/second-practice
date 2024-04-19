using Practice_2_6.DB;

namespace Practice_2_6;

public static class Authorization
{
    public static void AuthorizationApp()
    {
        while (true)
        {
            Console.Clear();
            if (DatabaseService.idLoginUser != DatabaseService.NOT_LOGIN) { break; }
            
            Console.WriteLine("You are not logged in\n" +
                              "choice the option: 'login', 'register' and 'exit'");
            
            Console.Write(">> ");
            string choice = Console.ReadLine() ?? "exit";
            
            switch (choice)
            {
                case "login":
                    _LoginApp();
                    break;
                case "register":
                    _RegistrationApp();
                    break;
                case "exit":
                    Environment.Exit(Utils.EXIT_COMMAND);
                    break;
            }
        }
    }
    
    private static void _LoginApp()
    {
        string login, pass;
        
        Console.Write("input login: ");
        login = (Console.ReadLine() ?? string.Empty).Trim();
        Console.WriteLine();
        
        Console.Write("input password: ");
        pass = (Console.ReadLine() ?? string.Empty).Trim();
        Console.WriteLine();
        
        try
        {
            DatabaseService.idLoginUser = DatabaseRequests.GetUserRegistration(login, pass);
        }
        catch (Npgsql.NpgsqlException error)
        {
            Console.WriteLine($"Sql state: {error.Data["SqlState"]}\n" +
                              $"Message text; {error.Data["MessageText"]}\n" +
                              $"Enter for continue...");
            Console.ReadKey(true);
        }
    }
    
    public static void LogoutApp()
    {
        DatabaseService.idLoginUser = DatabaseService.NOT_LOGIN;
    }
    
    private static void _RegistrationApp()
    {
        string login, pass;
        
        Console.Write("input login (min len 8): ");
        login = (Console.ReadLine() ?? string.Empty).Trim();
        Console.WriteLine();
        
        Console.Write("input password (min len 8): ");
        pass = (Console.ReadLine() ?? string.Empty).Trim();
        Console.WriteLine();
        
        try
        {
            DatabaseRequests.UserRegistration(login, pass);
        }
        catch (Npgsql.NpgsqlException error)
        {
            Console.WriteLine($"Sql state: {error.Data["SqlState"]}\n" +
                              $"Message text; {error.Data["MessageText"]}\n" +
                              $"Enter for continue...");
            Console.ReadKey(true);
        }
    }
}