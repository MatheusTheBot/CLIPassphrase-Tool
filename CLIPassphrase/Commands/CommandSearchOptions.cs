using Flunt.Notifications;
using Flunt.Validations;
using Microsoft.Extensions.Configuration;

namespace CLIPassphrase.Commands
{
    public class CommandSearchOptions : Notifiable<Notification>, ICommand
    {
        private readonly IConfiguration _configs;
        public CommandSearchOptions(string language, IConfiguration configs, int wordLenght = 0, int quantityWords = 0)
        {
            _configs = configs;

            //verify if it has any errors, if yes, change for the default value in appsettings, else continue

            if (language == null || language == "")
            {
                Language = _configs["MyConfiguration:" + nameof(Language)];
            }
            else
            {
                Language = language;
            }

            if (wordLenght == 0 || wordLenght < 3)
            {
                WordLenght = int.Parse(_configs["MyConfiguration:" + nameof(WordLenght)]);
            }
            else
            {
                WordLenght = wordLenght;
            }

            if (quantityWords < 1)
            {
                QuantityWords = int.Parse(_configs["MyConfiguration:" + nameof(QuantityWords)]);
            }
            else
            {
                QuantityWords = quantityWords;
            }

            Test();
        }

        public string Language { get; set; }
        public int WordLenght { get; set; }
        public int QuantityWords { get; set; }

        public void Test()
        {
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNotNull(_configs, nameof(_configs), "IConfiguration cannot be null")
                .IsGreaterOrEqualsThan(QuantityWords, 1, nameof(QuantityWords), "The min quantity of words is 1")
                .IsGreaterOrEqualsThan(WordLenght, 3, nameof(WordLenght), "The min lenght is 3")
                .IsNotNullOrWhiteSpace(Language, nameof(Language), "Cannot be null or empty")
            );
        }
    }
}