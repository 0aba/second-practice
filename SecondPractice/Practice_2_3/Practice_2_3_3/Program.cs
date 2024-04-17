namespace Practice_2_3_3;

class Calculation
{
    private string CalculationLine { get; set; } = string.Empty;

    public void SetCalculationLine(string calculationLine) { CalculationLine = calculationLine; }

    public void SetLastSymbolCalculationLine(char symbol) { CalculationLine += symbol; }
    
    public string GetCalculationLine() { return CalculationLine; }
    
    public char GetLastSymbol() { return CalculationLine.Length == 0 
        ? '\0' : CalculationLine[CalculationLine.Length - 1]; }
    
    public void DeleteLastSymbol() { CalculationLine = CalculationLine.Length == 0 
        ? string.Empty : 
        CalculationLine.Substring(0, CalculationLine.Length - 1); }
}

class Program
{
    static void Main(string[] args)
    {
        int n = 0;
        Calculation NewCalculation = new Calculation();
        
        Console.Write("input start line: ");
        NewCalculation.SetCalculationLine(Console.ReadLine());
        Console.WriteLine();
        
        Console.Write("input amount replaced char: ");
        n = int.Parse(Console.ReadLine());
        Console.WriteLine();

        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"deleted char: {NewCalculation.GetLastSymbol()}");
            NewCalculation.DeleteLastSymbol();
        }
        
        Console.WriteLine($"input {n} new char:");
        for (int i = 0; i < n; i++)
        {
            NewCalculation.SetLastSymbolCalculationLine(Console.ReadLine()[0]);
        }
        
        Console.WriteLine($"result operation: {NewCalculation.GetCalculationLine()}");
    }
}