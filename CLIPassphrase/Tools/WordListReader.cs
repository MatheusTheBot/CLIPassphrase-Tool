using CLIPassphrase.Enums;
using CLIPassphrase.Models;
using System.Text;

namespace CLIPassphrase.Tools;
public class WordListReader
{
    readonly Dictionary<string, string> Languages = new()
    {
        { "German", "de"},
        { "English", "en"},
        { "Spanish", "es"},
        { "French", "fr"},
        { "Italian", "it" },
        { "Latin", "la" },
        { "Dutch", "nl" }
    };
    readonly Dictionary<string, string> FilePath = new()
    {
        //'C:\Users\matt_\source\repos\CLIPassphrase Tool\CLIPassphrase\bin\Debug\WordLists\english.txt'.
        //C:\Users\matt_\source\repos\CLIPassphrase Tool\CLIPassphrase\WordLists\dutch.txt
        {"German", "../../../WordLists/german.txt"},
        { "English", "../../../WordLists/english.txt"},
        { "Spanish", "../../../WordLists/spanish.txt"},
        { "French", "../../../WordLists/french.txt"},
        { "Italian", "../../../WordLists/italian.txt" },
        { "Latin", "../../../WordLists/latin.txt" },
        { "Dutch", "../../../WordLists/dutch.txt" }
    };

    public ResponseModel Search(int Length, string Language)
    {
        if (!Languages.ContainsKey(Language) && !Languages.ContainsValue(Language))
        {
            return new ResponseModel(false, $"This language >-{Language}-< was not recognised or suported", ETypeOfError.Generic);
        }


        while (true)
        {
            string Word;
            int Lines;
            Random rdn;

            //using statement calls the G.C. for StreaReader after ended Using body ended  
            using (StreamReader reader = new StreamReader(FilePath[Language], Encoding.UTF8))
            {
                Lines = reader.ReadLine().Count();


                while ((Word = reader.ReadLine()) != null)
                {
                    Word = Word.Trim();

                    if (Word.Length == Length)
                    {
                        if (!Word.Contains(' '))
                        {
                            return new ResponseModel(true, Word);
                        }
                    }
                }
            }
            if (Word == null || Word.Length == 0)
            {
                return new ResponseModel(false, "Word with this length was not found in our dictionary", ETypeOfError.Internal);
            }
        }
    }
}