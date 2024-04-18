namespace Practice_2_2_3;

class СlassOfTwoNumbers                                                                                             
{
    public int Number1 { get; private set; }
    public int Number2 { get; private set; }
                                                                                                                    
    public void PrintNumbers()
    {
        Console.WriteLine($"number1: {Number1}\n" +
                          $"number2: {Number2}");
    }

    public void ChangeNumbers(int number1, int number2)                                                             
    {
        Number1 = number1;
        Number2 = number2;
    }
                                                                                                                    
    public int GetSum()
    {
        return Number1 + Number2;
    }
                                                                                                                    
    public int GetMax()
    {
        return Number1 > Number2 ? Number1 : Number2;
    }
}
                                                                                                                    
class Program                                                                                                       
{                                                                                                                   
    static void Main()
    {
        /*                                                                                                          
         Программа получает от пользователя два числа возвращая сумму и большее из этих двух чисел.                 
         */                                                                                                         

        СlassOfTwoNumbers TwoNumbers = new СlassOfTwoNumbers();

        Console.Write("Input numbers: ");
        TwoNumbers.ChangeNumbers(int.Parse(Console.ReadLine()), int.Parse(Console.ReadLine()));

        TwoNumbers.PrintNumbers();
        Console.WriteLine($"Get sum: {TwoNumbers.GetSum()}");
        Console.WriteLine($"Get max: {TwoNumbers.GetMax()}");
    }                                                                                                               
}      