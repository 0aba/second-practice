namespace Practice_2_3__1_2;

class Worker
{
    private string  _name = string.Empty;
    private string  _surname = string.Empty;
    private decimal _rate;
    private uint    _days;
    
    public string Name
    {
        get => _name;
        set => _name = value; 
    }
    public string Surname
    {
        get => _surname;
        set => _surname = value; 
    }
    public decimal Rate
    {
        get => _rate;
        set => _rate = value; 
    }
    public uint Days
    {
        get => _days;
        set => _days = value; 
    }

    public decimal GetSalary() { return Rate * Days; }
}

class Program
{
    public static Worker CreateWorker()
    {
        Worker NewWorker = new Worker();
        
        Console.Write("input name: ");
        NewWorker.Name = Console.ReadLine().Trim();
        Console.WriteLine();
        
        Console.Write("input surname: ");
        NewWorker.Surname = Console.ReadLine().Trim();
        Console.WriteLine();
        
        Console.Write("input rate: ");
        NewWorker.Rate = decimal.Parse(Console.ReadLine());
        Console.WriteLine();
        
        Console.Write("input days: ");
        NewWorker.Days = uint.Parse(Console.ReadLine());
        Console.WriteLine();
        
        return NewWorker;
    }
    
    static void Main()
    {
        Worker NewWorker = CreateWorker();
        
        Console.WriteLine($"name: {NewWorker.Name} surname: {NewWorker.Surname}\n" +
                          $"rate: {NewWorker.Rate} days: {NewWorker.Days}\n" +
                          $"salary: {NewWorker.GetSalary()}");
    }
}