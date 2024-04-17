namespace Practice_2_1_1;

class Program
{
    static uint CountAmountOfGems(string jewelry, string stones)
    {
        uint result = 0;

        HashSet<char> allJewelry = new HashSet<char>(jewelry);

        foreach (var stone in stones)
        {
            if (allJewelry.Contains(stone)) { ++result; }
        }
            
        return result;
    }
    static void Main()
    {
        string j, s;
            
        Console.Write("Input j: ");
        j = Console.ReadLine();
        Console.WriteLine();
            
        Console.Write("Input s: ");
        s = Console.ReadLine();
        Console.WriteLine();
            
        Console.Write($"gems in s = {CountAmountOfGems(j, s)}");
    }
}