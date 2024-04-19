namespace Practice_2_6;

class Program
{
    public static void Main()
    {
        string command;

        Console.WriteLine("Введите 'help' чтобы узнать какие есть команды");

        while (true)
        {
            if (DB.DatabaseService.idLoginUser == DB.DatabaseService.NOT_LOGIN) { Authorization.AuthorizationApp(); }
        
            Console.Write(">> ");

            command = Console.ReadLine().Trim();

            Сommands.RunСommand(command);
        }
    }
}

