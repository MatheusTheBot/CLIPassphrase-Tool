using CLIPassphrase.Enums;
using CLIPassphrase.Models;
using System.Diagnostics;

namespace CLIPassphrase.Tools;
public static class Printer
{
    public static void Error(ResponseModel model)
    {
        //just in case...
        if (model.Success == true)
        {
            return;
        }

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"Error - {model.Content}");
        switch (model.ErrorType)
        {
            case ETypeOfError.Generic:
                Console.WriteLine($"Please, try again later {Environment.NewLine}");
                break;
            case ETypeOfError.Internal:
                Console.WriteLine($"This problem will be fixed soon. Please, try again later {Environment.NewLine}");
                break;
            case ETypeOfError.Api:
                Console.WriteLine($"This problem do not depend on us. This will be fixed soon, so please, try again later {Environment.NewLine}");
                break;
            case ETypeOfError.Unknown:
                Console.WriteLine($"This is was an unexpected error, please, try again later {Environment.NewLine}");
                break;
            default:
                Console.WriteLine($"Please, try again {Environment.NewLine}");
                break;
        }
        Console.ForegroundColor = ConsoleColor.White;
    }

    public static void NewPassPhrase(string[] passphrase)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("Your generated passphrase is: [");
        for (int i = 0; i < passphrase.Length; i++)
        {
            string s = passphrase[i];

            if (i == 0)
            {
                Console.Write($"{s}");
            }
            else
            {
                Console.Write($"-{s}");
            }
        }
        Console.WriteLine($"] {Environment.NewLine}");
        Console.ForegroundColor = ConsoleColor.White;
    }
    public static void NewPassWord(string password)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Your generated word is: [{password}] {Environment.NewLine}");
        Console.ForegroundColor = ConsoleColor.White;
    }

    public static void FinalEntropy(string password, double entropy)
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine($"The entropy for the word {password} is: {entropy} {Environment.NewLine}");
        Console.ForegroundColor = ConsoleColor.White;
    }
    public static void FinalEntropy(string[] password, double entropy)
    {
        var response = "";
        foreach (var item in password)
        {
            if (response == "")
                response = item.ToString();
            else
                response = $"{response}-{item}";
        }

        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine($"The entropy for the phrase {response} is: {entropy} {Environment.NewLine}");
        Console.ForegroundColor = ConsoleColor.White;
    }

    public static void StopWatch(Stopwatch timer)
    {
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine($"Execution time: {timer.Elapsed} {Environment.NewLine}");
        Console.ForegroundColor = ConsoleColor.White;
    }
}
