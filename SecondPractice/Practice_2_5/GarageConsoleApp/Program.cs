using GarageConsoleApp.Pages;

namespace GarageConsoleApp;

/// <summary>
/// Класс Program
/// здесь описываем логику приложения
/// вызываем нужные методы пишем обработчики и тд
/// </summary>
public class Program 
{
    public static void Main()
    {
        PageUtils.Pages optionPage = PageUtils.Pages.MenuPage;

        while (true)
        {
            switch (optionPage)
            {
                case PageUtils.Pages.MenuPage:
                    MenuPage.OpenMenuPage(ref optionPage);
                    break;
                case PageUtils.Pages.CarsPage:
                    CarPage.OpenCarPage(ref optionPage);
                    break;
                case PageUtils.Pages.DriversPage:
                    DriverPage.OpenDriverPage(ref optionPage);
                    break;
                case PageUtils.Pages.RoutesPage:
                    RoutePage.OpenRoutePage(ref optionPage);
                    break;
                case PageUtils.Pages.ItineraryPage:
                    ItineraryPage.OpenItineraryPage(ref optionPage);
                    break;
            }

            if (optionPage != PageUtils.Pages.None) { continue; }
            break;
        }
    }
    
}