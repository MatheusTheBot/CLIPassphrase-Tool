using CLIPassphrase.Enums;
using CLIPassphrase.Models;
using HtmlAgilityPack;

namespace CLIPassphrase.Tools;
public class Crawler
{
    readonly Dictionary<string, string> Languages = new()
        {
            { "Bengali", "bn"},
            { "German", "de"},
            { "English", "en"},
            { "Spanish", "es"},
            { "French", "fr"},
            { "Hindi", "hi"},
            { "Italian", "it"},
            { "Japanese", "ja"},
            { "Javanese", "jv"},
            { "Korean", "ko"},
            { "Marathi", "mr"},
            { "Malay", "ms"},
            { "Polish", "pl"},
            { "Portuguese", "pt"},
            { "Romanian", "ro"},
            { "Russian", "ru"},
            { "Tamil", "ta"},
            { "Turkish", "tr"},
            { "Ukranian", "uk"},
            { "Chinese", "zh"}
        };

    public ResponseModel Search(int Length, string Language)
    {
        if (Languages.ContainsKey(Language))
        {
            Language = Languages[Language];
        }

        if (Languages.ContainsValue(Language))
        {
            Language = Languages.Values.First(x => x.Contains(Language));
        }

        if (!Languages.ContainsKey(Language) && !Languages.ContainsValue(Language))
        {
            return new ResponseModel(false, $"This language >-{Language}-< was not recognised or suported", ETypeOfError.Generic);
        }


        while (true)
        {
            HtmlDocument Doc;
            HtmlWeb Site = new();

            Doc = Site.Load($"https://educalingo.com/{Language}/dic-{Language}/random-word");

            // Sample: "...<title> Random word -..."

            int IndexStart = Doc.ParsedText.IndexOf("<title>");
            int IndexEnd = Doc.ParsedText.IndexOf(" - ");

            string Word = Doc.ParsedText.Substring(IndexStart, IndexEnd);
            Word = Word.Substring(Word.IndexOf(">"), Word.IndexOf("-"));

            Word = Word.Remove(Word.IndexOf("-"));
            Word = Word.Remove(Word.IndexOf(">"), 1);

            Word = Word.Trim();
            if (Word.Length == Length)
            {
                //if (!Word.Contains(' '))
                return new ResponseModel(true, Word);
            }
        }
    }
}