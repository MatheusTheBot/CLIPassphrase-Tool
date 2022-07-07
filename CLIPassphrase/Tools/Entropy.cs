namespace CLIPassphrase.Tools;
public class Entropy
{
    private Dictionary<char, int> Letters = new();

    public double Calculate(string input)
    {
        double result = 0;
        double frequency;

        foreach (char c in input)
        {
            if (c == ' ')
            {
                continue;
            }

            if (!Letters.ContainsKey(c))
            {
                Letters.Add(c, 1);
            }
            else
            {
                Letters[c]++;
            }
        }

        foreach (var item in Letters)
        {
            frequency = (double)item.Value / input.Length;
            result -= frequency * (Math.Log(frequency) / Math.Log(2));
        }
        return result;
    }
    public double Calculate(string[] input)
    {
        double result = 0;
        string output = "";
        double frequency;

        //fix string[] onto string;
        foreach (var item in input)
        {
            if (output == "")
            {
                output = item.ToString();
            }
            else
            {
                output = $"{output}-{item}";
            }
        }

        foreach (char c in output)
        {
            if (c == ' ')
            {
                continue;
            }

            if (!Letters.ContainsKey(c))
            {
                Letters.Add(c, 1);
            }
            else
            {
                Letters[c]++;
            }
        }

        foreach (var item in Letters)
        {
            frequency = (double)item.Value / input.Length;
            result -= frequency * (Math.Log(frequency) / Math.Log(2));
        }
        return Math.Abs(result);
    }
}