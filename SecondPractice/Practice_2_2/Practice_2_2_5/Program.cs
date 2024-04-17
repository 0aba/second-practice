namespace Practice_2_2_5;

class СlassOfTwoNumbers
{
    public int Number1 { get; set; }
    public int Number2 { get; set; }

    public СlassOfTwoNumbers(int number1, int number2)
    {
        Number1 = number1;
        Number2 = number2;
    }

    public СlassOfTwoNumbers()
    {
        Number1 = 0;
        Number2 = 0;
    }

    ~СlassOfTwoNumbers()
    {
        Console.WriteLine("Object is deleted"); 
    }


}

class Program
{
    static void Main()
    {
        /*
         Программа получает от пользователя два числа возвращает два числа.
         */

        Console.WriteLine("Input numbers: ");
        СlassOfTwoNumbers TwoNumbers = new СlassOfTwoNumbers(int.Parse(Console.ReadLine()), int.Parse(Console.ReadLine()));
        Console.WriteLine($"Input numbers {TwoNumbers.Number1} and {TwoNumbers.Number2}");
    }
}