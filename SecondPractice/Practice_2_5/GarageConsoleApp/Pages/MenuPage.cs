using GarageConsoleApp.DB;


namespace GarageConsoleApp.Pages;

public static class MenuPage
{
    private static _OptionMenu _firstOption = _OptionMenu.CarPageOption;
    private static _OptionMenu _lastOption  = _OptionMenu.Exit;
    private enum _OptionMenu
    {
        CarPageOption       = 0,
        DriverPageOption    = 1,
        RoutePageOption     = 2,
        ItineraryPageOption = 3,
        Exit                = 4
    }

    private static void _PrintMenuPage(_OptionMenu optionMenu)
    {
        Console.WriteLine($"Menu page\n" +
                          $"controls: up arrow, down arrow to move, and enter to select.\n" +
                          $"1) cars page      {(optionMenu == _OptionMenu.CarPageOption ? "+": string.Empty)}\n" +
                          $"2) Drivers page   {(optionMenu == _OptionMenu.DriverPageOption ? "+": string.Empty)}\n" +
                          $"3) Routes page    {(optionMenu == _OptionMenu.RoutePageOption ? "+": string.Empty)}\n" +
                          $"4) Itinerary page {(optionMenu == _OptionMenu.ItineraryPageOption ? "+": string.Empty)}\n" +
                          $"5) exit           {(optionMenu == _OptionMenu.Exit ? "+": string.Empty)}");
    }

    public static void OpenMenuPage(ref PageUtils.Pages currentPage)
    {
        ConsoleKeyInfo key;
        _OptionMenu currentOptionMenu = _firstOption; 
        
        while (true)
        {
            Console.Clear();
            _PrintMenuPage(currentOptionMenu);

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
                case _OptionMenu.CarPageOption:
                    currentPage = PageUtils.Pages.CarsPage;
                    break;
                case _OptionMenu.DriverPageOption:
                    currentPage = PageUtils.Pages.DriversPage;
                    break;
                case _OptionMenu.ItineraryPageOption:
                    currentPage = PageUtils.Pages.ItineraryPage;
                    break;
                case _OptionMenu.RoutePageOption:
                    currentPage = PageUtils.Pages.RoutesPage;
                    break;
                case _OptionMenu.Exit:
                    currentPage = PageUtils.Pages.None;
                    break;
            }
            break;
        }
    }
}