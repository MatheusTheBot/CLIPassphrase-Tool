using CLIPassphrase.Commands;
using CLIPassphrase.Enums;
using CLIPassphrase.Models;
using CLIPassphrase.Tools;
using Flunt.Notifications;

namespace CLIPassphrase.Handlers;
public class Handler : Notification,
    IHandler<CommandFindMultipleWords>,
    IHandler<CommandFindJustOneWord>,
    IHandler<CommandCalculateEntropy>,
    IHandler<CommandSearchOptions>
{
    //public void Handle(CommandFindMultipleWords command)
    //{
    //    //validation
    //    command.Test();
    //    if (!command.IsValid)
    //    {
    //        Printer.Error(new ResponseModel(false, command.Notifications, ETypeOfError.Generic));
    //        return;
    //    }

    //    //make the variables and objects to use
    //    Crawler crawler = new();
    //    Entropy entropy = new();
    //    string[] response = new string[command.QuantityWords];

    //    //get the ordered amount of words 
    //    for (int i = 0; i < command.QuantityWords; i++)
    //    {
    //        var r = crawler.Search(command.LengthWords, command.Language);
    //        if (r.Result.Success == true && r.Result.Content != null)
    //        {
    //            response[i] = r.Result.Content.ToString();
    //        }
    //        else
    //        {
    //            Printer.Error(new ResponseModel(false, "Somehow, the generated word came as null", ETypeOfError.Unknown));
    //            return;
    //        }
    //    }

    //    //All chars to lower
    //    for (int i = 0; i < response.Length; i++)
    //    {
    //        string s = response[i];
    //        response[i] = s.ToLower();
    //    }

    //    Printer.NewPassPhrase(response);
    //    Printer.FinalEntropy(response, entropy.Calculate(response));
    //}

    //public void Handle(CommandFindJustOneWord command)
    //{
    //    command.Test();
    //    if (!command.IsValid)
    //    {
    //        Printer.Error(new ResponseModel(false, command.Notifications, ETypeOfError.Generic));
    //        return;
    //    }

    //    Crawler crawler = new();
    //    Entropy entropy = new();
    //    string response;

    //    var r = crawler.Search(command.WordLenght, command.Language);
    //    if (r.Result.Content == null || r.Result.Success == false)
    //    {
    //        Printer.Error(new ResponseModel(false, $"Somehow, the genereted word ({r.Result.Content}) came as null", ETypeOfError.Unknown));
    //        return;
    //    }
    //    else
    //    {
    //        response = r.Result.Content.ToString();
    //    }

    //    response = response.ToLower();

    //    Printer.NewPassWord(response);
    //    Printer.FinalEntropy(response, entropy.Calculate(response));
    //}

    public void Handle(CommandFindMultipleWords command)
    {
        //validation
        command.Test();
        if (!command.IsValid)
        {
            Printer.Error(new ResponseModel(false, command.Notifications, ETypeOfError.Generic));
            return;
        }

        //make the variables and objects to use
        //WordListReader reader = new();
        Crawler crawler = new();
        Entropy entropy = new();
        string[] response = new string[command.QuantityWords];

        //get the ordered amount of words 
        for (int i = 0; i < command.QuantityWords; i++)
        {
            var r = crawler.Search(command.LengthWords, command.Language);
            if (r.Success == true && r.Content != null)
            {
                response[i] = r.Content.ToString();
            }
            else
            {
                Printer.Error(new ResponseModel(false, "Somehow, the generated word came as null", ETypeOfError.Unknown));
                return;
            }
        }

        //All chars to lower
        for (int i = 0; i < response.Length; i++)
        {
            string s = response[i];
            response[i] = s.ToLower();
        }

        Printer.NewPassPhrase(response);
        Printer.FinalEntropy(response, entropy.Calculate(response));
    }

    public void Handle(CommandFindJustOneWord command)
    {
        command.Test();
        if (!command.IsValid)
        {
            Printer.Error(new ResponseModel(false, command.Notifications, ETypeOfError.Generic));
            return;
        }

        //WordListReader reader = new();
        Crawler crawler = new();
        Entropy entropy = new();
        string response;

        var r = crawler.Search(command.WordLenght, command.Language);
        if (r.Content == null || r.Success == false)
        {
            Printer.Error(new ResponseModel(false, $"Somehow, the genereted word ({r.Content}) came as null", ETypeOfError.Unknown));
            return;
        }
        else
        {
            response = r.Content.ToString();
        }

        response = response.ToLower();

        Printer.NewPassWord(response);
        Printer.FinalEntropy(response, entropy.Calculate(response));
    }

    public void Handle(CommandCalculateEntropy command)
    {
        command.Test();
        if (!command.IsValid)
        {
            Printer.Error(new ResponseModel(false, command.Notifications, ETypeOfError.Generic));
            return;
        }

        Entropy entropy = new Entropy();
        double result = entropy.Calculate(command.Password);

        Printer.FinalEntropy(command.Password, result);
    }

    public void Handle(CommandSearchOptions command)
    {
        //validation
        command.Test();
        if (!command.IsValid)
        {
            Printer.Error(new ResponseModel(false, command.Notifications, ETypeOfError.Generic));
            return;
        }

        //make the variables and objects to use
        //WordListReader reader = new();
        Entropy entropy = new();
        Crawler crawler = new();
        string[] response;
        string response1;

        if (command.QuantityWords > 1)
        {
            response = new string[command.QuantityWords];

            //get the ordered amount of words 
            for (int i = 0; i < command.QuantityWords; i++)
            {
                var r = crawler.Search(command.WordLenght, command.Language);

                if (r.Success == true && r.Content != null)
                {
                    response[i] = r.Content.ToString();
                }
                else
                {
                    Printer.Error(new ResponseModel(false, "Somehow, the generated word came as null", ETypeOfError.Unknown));
                    return;
                }
            }

            //All chars to lower
            for (int i = 0; i < response.Length; i++)
            {
                string s = response[i];
                response[i] = s.ToLower();
            }

            Printer.NewPassPhrase(response);
            Printer.FinalEntropy(response, entropy.Calculate(response));
        }
        else if (command.QuantityWords == 1)
        {
            var r = crawler.Search(command.WordLenght, command.Language);
            if (r.Content == null || r.Success == false)
            {
                Printer.Error(new ResponseModel(false, $"Somehow, the genereted word ({r.Content}) came as null", ETypeOfError.Unknown));
                return;
            }
            else
            {
                response1 = r.Content.ToString();
            }

            response1 = response1.ToLower();

            Printer.NewPassWord(response1);
            Printer.FinalEntropy(response1, entropy.Calculate(response1));
        }
    }
}