using GarageConsoleApp.DB;


namespace GarageConsoleApp.Pages;

public class ItineraryPage
{
    private static _OptionMenu _firstOption = _OptionMenu.ViewItineraryOption;
    private static _OptionMenu _lastOption  = _OptionMenu.Back;
    private enum _OptionMenu
    {
        ViewItineraryOption = 0,
        AddItineraryOption  = 1,
        Back            = 2
    }

    private static void _PrintItineraryPage(_OptionMenu optionMenu)
    {
        Console.WriteLine($"Itinerary page\n" +
                          $"controls: up arrow, down arrow to move, and enter to select.\n" +
                          $"1) view itinerary {(optionMenu == _OptionMenu.ViewItineraryOption ? "+": string.Empty)}\n" +
                          $"2) add  itinerary {(optionMenu == _OptionMenu.AddItineraryOption ? "+": string.Empty)}\n" +
                          $"3) back to menu   {(optionMenu == _OptionMenu.Back ? "+": string.Empty)}");
    }

    public static void OpenItineraryPage(ref PageUtils.Pages currentPage)
    {
        currentPage = PageUtils.Pages.MenuPage;
        ConsoleKeyInfo key;
        _OptionMenu currentOptionMenu = _firstOption; 
        
        while (true)
        {
            Console.Clear();
            _PrintItineraryPage(currentOptionMenu);

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
                case _OptionMenu.ViewItineraryOption:
                    _ViewItinerary();
                    break;
                case _OptionMenu.AddItineraryOption:
                    _AddItinerary();
                    break;
            }

            if (currentOptionMenu == _OptionMenu.Back) { break; }
        }
    }
    
    private static void _ViewItinerary()
    {
        DatabaseRequests.GetItineraryQuery();
    }
    
    private static void _AddItinerary()
    {
        Console.Write("input name itinerary: ");
        string name = Console.ReadLine() ?? string.Empty;
        Console.WriteLine();
        
        Console.WriteLine("enter 'yes' to create, otherwise what else.");
        if (!"yes".Equals(Console.ReadLine())) { return; }
        
        DatabaseRequests.AddItineraryQuery(name);
    }
}