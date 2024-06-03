using Practice_2_7.DB.Models;

namespace Practice_2_7.DB;

public static class DatabaseService
{
    private static ApplicationContext? _applicationContext = null;
    
    public static ApplicationContext GetApplicationContext()
    {
        if (_applicationContext is null)
        {
            _applicationContext = new ApplicationContext();
        }

        return _applicationContext;
    }
}