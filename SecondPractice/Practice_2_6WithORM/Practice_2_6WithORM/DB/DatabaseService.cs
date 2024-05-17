namespace Practice_2_6WithORM.DB;

public static class DatabaseService
{
    public static UserAPP? LoggedUser = null;
    
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