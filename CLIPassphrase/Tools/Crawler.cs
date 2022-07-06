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


        Task<HtmlDocument>[] Loads = new Task<HtmlDocument>[10];
        HtmlDocument[] Doc = new HtmlDocument[10];
        HtmlWeb Site = new();

        while (true)
        {
            for (int i = 0; i < Loads.Length; i++)
            {
                var c = Loads[i];
                c = Site.LoadFromWebAsync($"https://educalingo.com/{Language}/dic-{Language}/random-word");
                Loads[i] = c;
            }
            Task.WaitAll(Loads);

            for (int i = 0; i < Loads.Length; i++)
            {
                var d = Loads[i];
                Doc[i] = d.Result;
            }


            // Sample: "...<title> WORD - Definition and synonyms of..."

            foreach (var i in Doc)
            {
                HtmlNode Node = i.DocumentNode.SelectSingleNode("//head/title");
                string Word = Node.WriteTo();

                int EndIndex = Word.IndexOf(" - ");

                Word = Word.Substring(0, EndIndex);
                Word = Word.Replace("<title>", " ").Replace(".", " ").Trim().ToLower();
                
                if (Word.Length == Length)
                {
                    //if (!Word.Contains(' '))
                        return new ResponseModel(true, Word);
                }
            }
        }
    }
}