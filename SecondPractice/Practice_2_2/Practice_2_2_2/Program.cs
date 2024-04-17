using System.Globalization;


namespace Practice_2_2_2;

class Train
{
    public string DestinationName { get; set; } = string.Empty;
    public ushort NumberTrain { get; set; }
    public DateTime DepartureTime { get; set; }

    public void PrintAboutTrain()
    {
        Console.WriteLine($"Destination name: {DestinationName}\n" +
                          $"Number train: {NumberTrain}\n" +
                          $"Departure time: {DepartureTime: HH:mm:ss}");
    }
}

class Program
{
    public static Train СreateTrain()
    {
        Train NewTrain = new Train();

        Console.Write("Destination name: ");
        NewTrain.DestinationName = Console.ReadLine().Trim();
        Console.WriteLine();

        Console.Write("Number train: ");
        NewTrain.NumberTrain = ushort.Parse(Console.ReadLine());
        Console.WriteLine();

        Console.Write("Departure time in format \"HH:mm:ss\": ");
        NewTrain.DepartureTime = DateTime.ParseExact(Console.ReadLine(), "HH:mm:ss", CultureInfo.InvariantCulture);
        Console.WriteLine();

        return NewTrain;
    }


    static void Main()
    {
        /*
         Программа получает от пользователя: название пункта назначения, номер поезда, время отправления,
         если получившийся поездка на поезде была сделана с Томска, то вывести информацию о нем, иначе сообщить, что поездка сделана не с Томска.
         */

        Train NewTrain = СreateTrain();

        if (NewTrain.DestinationName.Equals("Tomsk"))
        {
            NewTrain.PrintAboutTrain();
        }
        else
        {
            Console.WriteLine("The departure city is not Tomsk.");
        }
    }
}