using GarageConsoleApp.DB;


namespace GarageConsoleApp.Pages;

public class CarPage
{
    private static _OptionMenu _firstOption = _OptionMenu.ViewCarOption;
    private static _OptionMenu _lastOption  = _OptionMenu.Back;
    private enum _OptionMenu
    {
        ViewCarOption     = 0,
        AddCarOption      = 1,
        ViewTypeCarOption = 2,
        AddTypeCarOption  = 3,
        Back              = 4
    }

    private static void _PrintCarPage(_OptionMenu optionMenu)
    {
        Console.WriteLine($"Cars page\n" +
                          $"controls: up arrow, down arrow to move, and enter to select.\n" +
                          $"1) view cars     {(optionMenu == _OptionMenu.ViewCarOption ? "+": string.Empty)}\n" +
                          $"2) add car       {(optionMenu == _OptionMenu.AddCarOption ? "+": string.Empty)}\n" +
                          $"3) view type car {(optionMenu == _OptionMenu.ViewTypeCarOption ? "+": string.Empty)}\n" +
                          $"4) add type car  {(optionMenu == _OptionMenu.AddTypeCarOption ? "+": string.Empty)}\n" +
                          $"5) back to menu  {(optionMenu == _OptionMenu.Back ? "+": string.Empty)}");
    }

    public static void OpenCarPage(ref PageUtils.Pages currentPage)
    {
        currentPage = PageUtils.Pages.MenuPage;
        
        ConsoleKeyInfo key;
        _OptionMenu currentOptionMenu = _firstOption; 
        
        while (true)
        {
            Console.Clear();
            _PrintCarPage(currentOptionMenu);

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
            
            Console.Clear();
            
            switch (currentOptionMenu)
            {
                case _OptionMenu.ViewCarOption:
                    _ViewCar();
                    break;
                case _OptionMenu.AddCarOption:
                    _AddCar();
                    break;
                case _OptionMenu.ViewTypeCarOption:
                    _ViewTypeCar();
                    break;
                case _OptionMenu.AddTypeCarOption:
                    _AddTypeCar();
                    break;
            }

            if (currentOptionMenu == _OptionMenu.Back) { break; }
        }
    }

    private static void _ViewCar()
    {
        DatabaseRequests.GetCarQuery();
        Console.Write("Enter for continue...");
        Console.ReadKey(true);
        
    }
    
    private static void _AddCar()
    {
        int idTypeCar = 0;
        uint numberPassengers = 0;
        string name, stateNumber;
        
        try
        {
            Console.Write("input id type car: ");
            idTypeCar = int.Parse(Console.ReadLine() ?? "0");
            Console.WriteLine();
        }
        catch
        {
            Console.WriteLine("Error: input number (value default 0)");
        }
        
        Console.Write("input name: ");
        name = Console.ReadLine() ?? string.Empty;
        Console.WriteLine();
        
        Console.Write("input state number: ");
        stateNumber = Console.ReadLine() ?? string.Empty;
        Console.WriteLine();
        
        try
        {
            Console.Write("input max number passengers: ");
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
            DatabaseRequests.AddCarQuery(idTypeCar, name, stateNumber, numberPassengers);
        }
        catch (Npgsql.NpgsqlException error)
        {
            Console.WriteLine($"Sql state: {error.Data["SqlState"]}\n" +
                              $"Message text; {error.Data["MessageText"]}\n" +
                              $"Enter for continue...");
            Console.ReadKey(true);
        }
    }
    
    private static void _ViewTypeCar()
    {
        DatabaseRequests.GetTypeCarQuery();
        Console.Write("Enter for continue...");
        Console.ReadKey(true);
    }
    
    private static void _AddTypeCar()
    {
        Console.Write("input name license: ");
        string name = Console.ReadLine() ?? string.Empty;
        Console.WriteLine();
        
        Console.WriteLine("enter 'yes' to create, otherwise what else.");
        if (!"yes".Equals(Console.ReadLine())) { return; }

        DatabaseRequests.AddTypeCarQuery(name);
    }
}