namespace Practice_2_2_4;

class Counter
{
    public int CounterNumber { get; private set; }

    public Counter()
    {
        CounterNumber = 0;
    }
                                                                                                                             
    public Counter(int counterNumber)
    {
        CounterNumber = counterNumber;
    }                                                                                                                        

    public static Counter operator ++(Counter CurrentCounter)
    {                                                                                                                        
        CurrentCounter.CounterNumber++;
        return CurrentCounter;
    }                                                                                                                        

    public static Counter operator --(Counter CurrentCounter)
    {
        --CurrentCounter.CounterNumber;
        return CurrentCounter;
    }                                                                                                                        
}                                                                                                                            
                                                                                                                             
class Program                                                                                                                
{                                                                                                                            
    static void Main()
    {
        /*                                                                                                                   
         Программа выводит числа с 1 до n, где n вводит пользователь.                                                        
         */                                                                                                                  

        Counter MyCounter = new Counter(1);

        Console.Write("Input n: ");
        int n = int.Parse(Console.ReadLine());
        Console.WriteLine();

        for (Counter i = MyCounter; i.CounterNumber < n; ++MyCounter)
        {
            Console.WriteLine($"i = {i.CounterNumber}");
        }
    }                                                                                                                        
}     