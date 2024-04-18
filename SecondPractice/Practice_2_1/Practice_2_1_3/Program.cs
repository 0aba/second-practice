namespace Practice_2_1_3;

class Program
{
    public static bool CheckArrayUniqueOfNumbers(ref int[] numbers)
    {
        HashSet<int> setNumber = new HashSet<int>(numbers);

        return numbers.Length == setNumber.Count;
    }
        
    static void Main()
    {
        int[] numbers;

        Console.WriteLine("Input amount numbers: ");
        numbers = new int[int.Parse(Console.ReadLine())];

        Console.WriteLine("Input numbers: ");
        for (int i = 0; i < numbers.Length; ++i)
        {
            numbers[i] = int.Parse(Console.ReadLine());
        }
            
        Console.WriteLine((CheckArrayUniqueOfNumbers(ref numbers) ? string.Empty : "not ") + "all element unique");
    }
}