using Practice_2_6WithORM.DB;

namespace Practice_2_6WithORM;

public static class Authorization
{
    public static void AuthorizationApp()
    {
        while (true)
        {
            Console.Clear();
            if (DatabaseService.LoggedUser != null) { break; }
            
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
        UserAPP loginUser = new UserAPP();
        
        Console.Write("input login: ");
        loginUser.Login = (Console.ReadLine() ?? string.Empty).Trim();
        Console.WriteLine();
        
        Console.Write("input password: ");
        loginUser.Pass = (Console.ReadLine() ?? string.Empty).Trim();
        Console.WriteLine();
        
        try
        {
            DatabaseService.LoggedUser = DatabaseRequests.GetUserAuthorization(loginUser);
        }
        catch (Microsoft.EntityFrameworkCore.DbUpdateException error)
        {
            Console.WriteLine($"Error: {error.Message}");
            Console.ReadKey(true);
        }
    }
    
    public static void LogoutApp()
    {
        DatabaseService.LoggedUser = null;
    }
    
    private static void _RegistrationApp()
    {
        UserAPP registerUser = new UserAPP();
        
        Console.Write("input login (min len 8 and without space char): ");
        registerUser.Login = (Console.ReadLine() ?? string.Empty).Trim();
        Console.WriteLine();
        
        Console.Write("input password (min len 8 and without space char): ");
        registerUser.Pass = (Console.ReadLine() ?? string.Empty).Trim();
        Console.WriteLine();
        
        try
        {
            DatabaseRequests.UserRegistration(registerUser);
        }
        catch (Microsoft.EntityFrameworkCore.DbUpdateException error)
        {
            Console.WriteLine($"Error: {error.Message}");
            Console.ReadKey(true);
        }
    }
}