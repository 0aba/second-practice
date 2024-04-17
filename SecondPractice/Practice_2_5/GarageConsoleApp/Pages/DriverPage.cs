using System.Globalization;
using GarageConsoleApp.DB;


namespace GarageConsoleApp.Pages;

public class DriverPage
{
    private static _OptionMenu _firstOption = _OptionMenu.ViewDriverOption;
    private static _OptionMenu _lastOption  = _OptionMenu.Back;
    private enum _OptionMenu
    {
        ViewDriverOption        = 0,
        AddDriverOption         = 1,
        ViewLicenseOption       = 2,
        AddLicenseOption        = 3,
        ViewLicenseDriverOption = 4,
        AddLicenseDriverOption  = 5,
        Back                    = 6
    }

    private static void _PrintDriverPage(_OptionMenu optionMenu)
    {
        Console.WriteLine($"Drivers page\n" +
                          $"controls: up arrow, down arrow to move, and enter to select.\n" +
                          $"1) view driver         {(optionMenu == _OptionMenu.ViewDriverOption ? "+": string.Empty)}\n" +
                          $"2) add driver          {(optionMenu == _OptionMenu.AddDriverOption ? "+": string.Empty)}\n" +
                          $"3) view licenses       {(optionMenu == _OptionMenu.ViewLicenseOption ? "+": string.Empty)}\n" +
                          $"4) add licenses        {(optionMenu == _OptionMenu.AddLicenseOption ? "+": string.Empty)}\n" +
                          $"5) view license driver {(optionMenu == _OptionMenu.ViewLicenseDriverOption ? "+": string.Empty)}\n" +
                          $"6) add license driver  {(optionMenu == _OptionMenu.AddLicenseDriverOption ? "+": string.Empty)}\n" +
                          $"7) back to menu        {(optionMenu == _OptionMenu.Back ? "+": string.Empty)}");
    }

    public static void OpenDriverPage(ref PageUtils.Pages currentPage)
    {
        currentPage = PageUtils.Pages.MenuPage;
        
        ConsoleKeyInfo key;
        _OptionMenu currentOptionMenu = _firstOption; 
        
        while (true)
        {
            Console.Clear();
            _PrintDriverPage(currentOptionMenu);

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
                case _OptionMenu.ViewDriverOption:
                    _ViewDriver();
                    break;
                case _OptionMenu.AddDriverOption:
                    _AddDriver();
                    break;
                case _OptionMenu.ViewLicenseOption:
                    _ViewLicense();
                    break;
                case _OptionMenu.AddLicenseOption:
                    _AddLicense();
                    break;
                case _OptionMenu.ViewLicenseDriverOption:
                    _ViewLicenseDriver();
                    break;
                case _OptionMenu.AddLicenseDriverOption:
                    _AddLicenseDriver();
                    break;
            }

            if (currentOptionMenu == _OptionMenu.Back) { break; }
        }
    }
    
    private static void _ViewDriver()
    {
        DatabaseRequests.GetDriverQuery();
    }
    
    private static void _AddDriver()
    {
        string firstName, lastName;
        DateTime birthDate;
        
        
        Console.Write("input first name: ");
        firstName = Console.ReadLine() ?? string.Empty;
        Console.WriteLine();
        
        
        Console.Write("input id last name: ");
        lastName = Console.ReadLine() ?? string.Empty;
        Console.WriteLine();
        
        try
        {
            Console.Write("input date in format 'yyyy-MM-dd': ");
            birthDate = DateTime.ParseExact(Console.ReadLine() ?? "2000-01-01", "yyyy-MM-dd", CultureInfo.InvariantCulture);
            Console.WriteLine();
        }
        catch
        {
            Console.WriteLine("Error: input number (value default 2000-01-01)");
            birthDate = new DateTime(2000, 1, 1);
        }
        
        Console.WriteLine("enter 'yes' to create, otherwise what else.");
        if (!"yes".Equals(Console.ReadLine())) { return; }

        DatabaseRequests.AddDriverQuery(firstName, lastName, birthDate);
    }
    
    private static void _ViewLicense()
    {
        DatabaseRequests.GetRightsCategoryQuery();
    }
    
    private static void _AddLicense()
    {
        Console.Write("input name license: ");
        string name = Console.ReadLine() ?? string.Empty;
        Console.WriteLine();
        
        Console.WriteLine("enter 'yes' to create, otherwise what else.");
        if (!"yes".Equals(Console.ReadLine())) { return; }
        
        DatabaseRequests.AddRightsCategoryQuery(name);
    }
    
    private static void _ViewLicenseDriver()
    {
        int idDriver = 0;

        try
        {
            Console.Write("input id drive: ");
            idDriver = int.Parse(Console.ReadLine() ?? "0");
            Console.WriteLine();
        }
        catch
        {
            Console.WriteLine("Error: input number (value default 0)");
        }
        
        try
        {
            DatabaseRequests.GetDriverRightsCategoryQuery(idDriver);
        }
        catch (Npgsql.NpgsqlException error)
        {
            Console.WriteLine($"Sql state: {error.Data["SqlState"]}\n" +
                              $"Message text; {error.Data["MessageText"]}\n" +
                              $"Enter for continue...");
            Console.ReadKey(true);
        }
    }
    
    private static void _AddLicenseDriver()
    {
        int idDriver = 0, idRightsCategory = 0;

        try
        {
            Console.Write("input id drive: ");
            idDriver = int.Parse(Console.ReadLine() ?? "0");
            Console.WriteLine();
        }
        catch
        {
            Console.WriteLine("Error: input number (value default 0)");
        }
        
        try
        {
            Console.Write("input id rights category: ");
            idRightsCategory = int.Parse(Console.ReadLine() ?? "0");
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
            DatabaseRequests.AddDriverRightsCategoryQuery(idDriver, idRightsCategory);
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