namespace Practice_2_6WithORM;


class Program
{
    public static void Main()
    {
        string command;

        Console.WriteLine("Введите 'help' чтобы узнать какие есть команды");

        while (true)
        {
            if (DB.DatabaseService.LoggedUser == null) { Authorization.AuthorizationApp(); }
        
            Console.Write(">> ");

            command = Console.ReadLine().Trim();

            Сommands.RunСommand(command);
        }
    }
}

