namespace Practice_2_1_2;

class Program
{
    private static int[] _GetSubarrayByNumberCombination(ref int[] candidates, int numberCombination)
    {
        int[] result;
        string binaryCombination = Convert.ToString(numberCombination, 2);
        int addIndexResult;
        
        result = new int[binaryCombination.Count(chr => chr.Equals('1'))];
        addIndexResult = result.Length - 1;
        int amountIteration = candidates.Length < binaryCombination.Length 
                              ? candidates.Length : binaryCombination.Length;
        
        for (int i = 0 ; i < amountIteration; ++i)
        {
            if (binaryCombination[amountIteration - 1 - i].Equals('0')) { continue; }
            
            result[addIndexResult] = candidates[candidates.Length - 1 - i];
            --addIndexResult;
        }

        return result;
    }

    private static bool _IsUniqueArrayInList(ref List<int[]> listArrays, ref int[] checkArray)
    {
        foreach (var arrayInclude in listArrays)
        {
            if (arrayInclude.Length != checkArray.Length) { continue; }

            int i;
            for (i = 0; i < arrayInclude.Length; ++i)
            {
                if (arrayInclude[i] != checkArray[i]) { break; }
            }

            if (i == arrayInclude.Length) { return false; }
        }

        return true;
    }
    
    public static List<int[]> GetAllUniqueCombinationsEqualTarget(int[] candidates, int target)
    {
        Array.Sort(candidates);
        List<int[]> result = new List<int[]>();

        for (int i = 0; i < Math.Pow(2, candidates.Length); ++i)
        {
            int[] subarray = _GetSubarrayByNumberCombination(ref candidates, i);

            if (subarray.Sum() == target && _IsUniqueArrayInList(ref result, ref subarray))
            {
                result.Add(subarray);
            }
        }
        
        return result;
    }

    static void PrintNumberArray( int[] numbers)
    {
        Console.Write("[");
        for (int i = 0; i < numbers.Length; ++i)
        {
            Console.Write( i == numbers.Length - 1 ? $"{numbers[i]}" : $"{numbers[i]}, ");
        }
        Console.Write("]");
    }
    
    static void Main()
    {
        int[] candidates;
        int target;
        List<int[]> combination;
        
        Console.Write("Input target: ");
        target = int.Parse(Console.ReadLine());
        
        Console.Write("Input amount combination: ");
        candidates = new int[int.Parse(Console.ReadLine())];
        
        Console.WriteLine("Input combination: ");
        for (int i = 0; i < candidates.Length; ++i)
        {
            candidates[i] = int.Parse(Console.ReadLine());
        }

        combination = GetAllUniqueCombinationsEqualTarget(candidates, target);

        Console.WriteLine('[');
        for (int i = 0; i < combination.Count; ++i)
        {
            Console.Write('\t');
            PrintNumberArray(combination[i]);
            Console.Write(i == combination.Count - 1 ? "" : ",");
            Console.WriteLine();
        }
        Console.Write(']');
    }
}