using Flunt.Notifications;
using Flunt.Validations;

namespace CLIPassphrase.Commands;
public class CommandFindJustOneWord : Notifiable<Notification>, ICommand
{
    public CommandFindJustOneWord(string language, int wordLenght)
    {
        Language = language;
        WordLenght = wordLenght;

        Test();
    }

    public string Language { get; set; }
    public int WordLenght { get; set; }

    public void Test()
    {
        AddNotifications(new Contract<Notification>()
            .Requires()
            .IsNotNullOrWhiteSpace(Language, nameof(Language), "Cannot be null or empty")
            .IsGreaterOrEqualsThan(WordLenght, 4, nameof(WordLenght), "For one word commands, the min lenght output is 4 characters")
        );
    }
}