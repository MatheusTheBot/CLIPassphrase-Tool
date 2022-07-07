using CLIPassphrase.Tools;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.CommandLine;
using System.CommandLine.NamingConventionBinder;
using System.CommandLine.Parsing;
using System.Diagnostics;
using System.Reflection;

namespace CLIPassphrase;
public class Program
{
    static void Main(string[] args)
    {
        var builder = new ConfigurationBuilder();
        var configs = new MyConfiguration();

        var app = BuildConfig(builder, configs);

        var rootCommand = new RootCommand();

        var timer = new Stopwatch();
        timer.Start();

        BuildCommandOptions(rootCommand, app);
        rootCommand.Invoke(args);

        timer.Stop();
        Printer.StopWatch(timer);
    }


    static IConfigurationRoot BuildConfig(IConfigurationBuilder builder, MyConfiguration configs)
    {
        builder.SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();

        var app = builder.Build();
        app.GetSection(nameof(MyConfiguration)).Bind(configs);
        return app;
    }

    static void BuildCommandOptions(RootCommand command, IConfiguration configs)
    {
        //all possible commands
        //--init, -i (BuildRootCommand)

        //--language, -l (Here)
        //--wordLenght, -w (Here)
        //--quantityWords, -q (Here)

        //--configure, -c
        //  {Language, -l
        //  {WordLenght, -w
        //  {QuantityWords, -q
        //++++++++++++++++++++++++++++++++++++++

        var languageOption = new Option<string>(new[] { "--language", "-l" }, "Define a language to use to retrieve in this specific case");
        var wordLenghtOption = new Option<int>(new[] { "--wordLenght", "-w" }, "Define the word lenght to retrieve in this specific case");
        var quantityWordsOption = new Option<int>(new[] { "--quantityWords", "-q" }, "Define the quantity of words to retrieve in this specific case");

        languageOption.IsRequired = false;
        wordLenghtOption.IsRequired = false;
        quantityWordsOption.IsRequired = false;

        languageOption.SetDefaultValue(configs["Language"]);
        wordLenghtOption.SetDefaultValue(configs["WordLenght"]);
        quantityWordsOption.SetDefaultValue(configs["QuantityWords"]);

        var initCommand = new Command("init", "Initialize the tool with a menu")
        {
            languageOption,
            wordLenghtOption,
            quantityWordsOption
        };

        command.AddCommand(initCommand);

        command.AddOption(languageOption);
        command.AddOption(wordLenghtOption);
        command.AddOption(quantityWordsOption);

        command.Handler = CommandHandler.Create<ParseResult>(x =>
        {
            var l = x.GetValueForOption(languageOption);
            var w = x.GetValueForOption(wordLenghtOption);
            var q = x.GetValueForOption(quantityWordsOption);
            var handler = new Handlers.Handler();

            handler.Handle(new Commands.CommandSearchOptions(l, configs, w, q));
        });

        //command.SetHandler<ParseResult>(res =>
        //{
        //    var l = res.GetValueForOption(languageOption);
        //    var w = res.GetValueForOption(wordLenghtOption);
        //    var q = res.GetValueForOption(quantityWordsOption);
        //    var handler = new Handlers.Handler();

        //    handler.Handle(new Commands.CommandSearchOptions(l, w, q, configs));

        //}, languageOption, wordLenghtOption, quantityWordsOption);
    }
}