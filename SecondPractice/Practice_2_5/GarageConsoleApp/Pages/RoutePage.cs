using GarageConsoleApp.DB;


namespace GarageConsoleApp.Pages;

public class RoutePage
{
    private static _OptionMenu _firstOption = _OptionMenu.ViewRouteOption;
    private static _OptionMenu _lastOption  = _OptionMenu.Back;
    private enum _OptionMenu
    {
        ViewRouteOption = 0,
        AddRouteOption  = 1,
        Back            = 2
    }

    private static void _PrintRoutePage(_OptionMenu optionMenu)
    {
        Console.WriteLine($"Routes page\n" +
                          $"controls: up arrow, down arrow to move, and enter to select.\n" +
                          $"1) view routes  {(optionMenu == _OptionMenu.ViewRouteOption ? "+": string.Empty)}\n" +
                          $"2) add route    {(optionMenu == _OptionMenu.AddRouteOption ? "+": string.Empty)}\n" +
                          $"3) back to menu {(optionMenu == _OptionMenu.Back ? "+": string.Empty)}");
    }

    public static void OpenRoutePage(ref PageUtils.Pages currentPage)
    {
        currentPage = PageUtils.Pages.MenuPage;
        
        ConsoleKeyInfo key;
        _OptionMenu currentOptionMenu = _firstOption; 
        
        while (true)
        {
            Console.Clear();
            _PrintRoutePage(currentOptionMenu);

            key = Console.ReadKey();

            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    currentOptionMenu = currentOptionMenu == _firstOption ? _lastOption : --currentOptionMenu;
                    break;
                case ConsoleKey.DownArrow:
                    currentOptionMenu = currentOptionMenu == _lastOption ? _firstOption : ++currentOptionMenu;
                    break;
            }

            if (key.Key != ConsoleKey.Enter) { continue; }
            
            switch (currentOptionMenu)
            {
                case _OptionMenu.ViewRouteOption:
                    _ViewRoute();
                    break;
                case _OptionMenu.AddRouteOption:
                    _AddRoute();
                    break;
            }

            if (currentOptionMenu == _OptionMenu.Back) { break; }
        }
    }
    
    private static void _ViewRoute()
    {
        DatabaseRequests.GetRouteQuery();
    }
    
    private static void _AddRoute()
    {
        int idDriver = 0, idCar = 0, idItinerary = 0;
        uint numberPassengers = 0;
        
        try
        {
            Console.Write("input id driver: ");
            idDriver = int.Parse(Console.ReadLine() ?? "0");
            Console.WriteLine();
        }
        catch
        {
            Console.WriteLine("Error: input number (value default 0)");
        }
        
        try
        {
            Console.Write("input id car: ");
            idCar = int.Parse(Console.ReadLine() ?? "0");
            Console.WriteLine();
        }
        catch
        {
            Console.WriteLine("Error: input number (value default 0)");
        }
        
        try
        {
            Console.Write("input id itinerary: ");
            idItinerary = int.Parse(Console.ReadLine() ?? "0");
            Console.WriteLine();
        }
        catch
        {
            Console.WriteLine("Error: input number (value default 0)");
        }
        
        try
        {
            Console.Write("input number passengers: ");
            numberPassengers = uint.Parse(Console.ReadLine() ?? "0");
            Console.WriteLine();
        }
        catch
        {
            Console.WriteLine("Error: input number (value default 0)");
        }
        
        Console.WriteLine("enter 'yes' to create, otherwise what else.");
        if (!"yes".Equals(Console.ReadLine())) { return; }
        
        try
        {
            DatabaseRequests.AddRouteQuery(idDriver, idCar, idItinerary, numberPassengers);
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