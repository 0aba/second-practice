namespace Practice_2_4;

class ParserRomanNumber
{
    public const int ERROR_ROMAN_NUMBER = -1;

    public static readonly Dictionary<char, ushort> RomanNumber = new Dictionary<char, ushort>()
    {
        { 'I', 1 },
        { 'V', 5 },
        { 'X', 10 },
        { 'L', 50 },
        { 'C', 100 },
        { 'D', 500 },
        { 'M', 1000 }
    };

    public static int ParseNumber(string romanNumber)
    {
        int result = 0;
        char сurrentLargeRomanNumber = 'I';

        if (!CheckValidRomanNumber(romanNumber))  { return ERROR_ROMAN_NUMBER; }

        for (int i = romanNumber.Length - 1; i > -1; --i)
        {
            if (RomanNumber.GetValueOrDefault(romanNumber[i]) >= RomanNumber.GetValueOrDefault(сurrentLargeRomanNumber))
            {
                result += RomanNumber.GetValueOrDefault(romanNumber[i]);
                сurrentLargeRomanNumber = romanNumber[i];
            }
            else // ... < ...
            {
                result -= RomanNumber.GetValueOrDefault(romanNumber[i]);
            }
        }

        return result;
    }

    public static bool CheckValidRomanNumber(string romanNumber)
    {
        ushort сurrentLargeRomanNumber = RomanNumber.GetValueOrDefault('I');

        for (int i = romanNumber.Length - 1; i > -1; --i)
        {
            switch (romanNumber[i])
            {
                case 'I':
                    if (сurrentLargeRomanNumber > RomanNumber.GetValueOrDefault('I'))
                    {
                        return false;
                    }
                    break;
                case 'V':
                    if (сurrentLargeRomanNumber > RomanNumber.GetValueOrDefault('V'))
                    {
                        return false;
                    }

                    if (i != 0 && romanNumber[i - 1] == 'I' && 
                        сurrentLargeRomanNumber != RomanNumber.GetValueOrDefault('V'))
                    {
                        --i;
                    }

                    сurrentLargeRomanNumber = RomanNumber.GetValueOrDefault('V');
                    break;
                case 'X':
                    if (сurrentLargeRomanNumber > RomanNumber.GetValueOrDefault('X'))
                    {
                        return false;
                    }

                    if (i != 0 && romanNumber[i - 1] == 'I' &&
                        сurrentLargeRomanNumber != RomanNumber.GetValueOrDefault('X'))
                    {
                        --i;
                    }

                    сurrentLargeRomanNumber = RomanNumber.GetValueOrDefault('X');
                    break;
                case 'L':
                    if (сurrentLargeRomanNumber > RomanNumber.GetValueOrDefault('L'))
                    {
                        return false;
                    }

                    if (i != 0 && romanNumber[i - 1] == 'X' &&
                        сurrentLargeRomanNumber != RomanNumber.GetValueOrDefault('L'))
                    {
                        --i;
                    }

                    сurrentLargeRomanNumber = RomanNumber.GetValueOrDefault('L');
                    break;
                case 'C':
                    if (сurrentLargeRomanNumber > RomanNumber.GetValueOrDefault('C'))
                    {
                        return false;
                    }

                    if (i != 0 && romanNumber[i - 1] == 'X' &&
                        сurrentLargeRomanNumber != RomanNumber.GetValueOrDefault('C'))
                    {
                        --i;
                    }

                    сurrentLargeRomanNumber = RomanNumber.GetValueOrDefault('C');
                    break;
                case 'D':
                    if (сurrentLargeRomanNumber > RomanNumber.GetValueOrDefault('D'))
                    {
                        return false;
                    }

                    if (i != 0 && romanNumber[i - 1] == 'C' &&
                        сurrentLargeRomanNumber != RomanNumber.GetValueOrDefault('D'))
                    {
                        --i;
                    }

                    сurrentLargeRomanNumber = RomanNumber.GetValueOrDefault('D');
                    break;
                case 'M':
                    if (i != 0 && romanNumber[i - 1] == 'C' &&
                        сurrentLargeRomanNumber != RomanNumber.GetValueOrDefault('M'))
                    {
                        --i;
                    }

                    сurrentLargeRomanNumber = RomanNumber.GetValueOrDefault('M');
                    break;
                default:
                    return false;
            }
        }

        return true;
    }
}

class Program
{
    static void Main()
    {
        Console.Write("input roman number: ");
        int result = ParserRomanNumber.ParseNumber(Console.ReadLine() ?? string.Empty);
        Console.WriteLine();
        
        Console.WriteLine(result == ParserRomanNumber.ERROR_ROMAN_NUMBER ? "error: not valid roman number": $"result parsed: {result}");
    }
}