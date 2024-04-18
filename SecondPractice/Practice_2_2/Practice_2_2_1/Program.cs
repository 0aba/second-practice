using System.Globalization;


namespace Practice_2_2_1;

class Student
{
    public static readonly int maxNumberOfGrades = 5;

    public string Surname { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public ushort NumberGroups { get; set; }
    public byte[] AcademicPerformance { get; } = new byte[maxNumberOfGrades];

    public void PrintWhoIsThis()
    {
        Console.WriteLine($"surname: {Surname}.\n" +
                          $"Date of birth: {DateOfBirth: dd MMM yyyy}");
    }
}

class Program
{
    public static Student CreateStudent()
    {
        Student NewStudent = new Student();

        Console.Write("Input surname student: ");
        NewStudent.Surname = Console.ReadLine().Trim();
        Console.WriteLine();

        Console.Write("Input birthday in format \"dd-MM-yyyy\": ");
        NewStudent.DateOfBirth = DateTime.ParseExact(Console.ReadLine(), "dd-MM-yyyy", CultureInfo.InvariantCulture);
        Console.WriteLine();

        Console.Write("Input number groups: ");
        NewStudent.NumberGroups = ushort.Parse(Console.ReadLine());
        Console.WriteLine();

        for (int i = 0; i < Student.maxNumberOfGrades; ++i)
        {
            Console.Write($"Input grades №{i} (0 - none, 2-5): ");
            NewStudent.AcademicPerformance[i] = byte.Parse(Console.ReadLine());
            Console.WriteLine();
        }

        return NewStudent;
    }

    static void Main()
    {
        /*
         Программа получает от пользователя: фамилию, дату рождения, группу и 5 оценок,
         если студент старше 15 лет, то вывести кто он, иначе вывесли сообщение, что возраст меньше 15.
         */
        Student NewStudent = CreateStudent();

        if ((DateTime.Now - NewStudent.DateOfBirth) < new TimeSpan(15 * 365, 0, 0, 0, 0, 0))
        {
            Console.WriteLine("Error: age less 15!!!1");
        }
        else
        {
            Console.WriteLine("Student successfully created!1!!");
            NewStudent.PrintWhoIsThis();
        }
    }
}