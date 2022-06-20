using Flunt.Notifications;
using Flunt.Validations;

namespace CLIPassphrase.Commands;
public class CommandFindMultipleWords : Notifiable<Notification>, ICommand
{
    public CommandFindMultipleWords(int quantityWords, int lengthWords, string language)
    {
        QuantityWords = quantityWords;
        LengthWords = lengthWords;
        Language = language;

        Test();
    }

    public int QuantityWords { get; set; }
    public int LengthWords { get; set; }
    public string Language { get; set; }

    public void Test()
    {
        AddNotifications(new Contract<Notification>()
            .Requires()
            .IsGreaterOrEqualsThan(QuantityWords, 2, nameof(QuantityWords), "The min quantity of words is 2")
            .IsGreaterOrEqualsThan(LengthWords, 3, nameof(LengthWords), "The min lenght is 3")
            .IsNotNullOrWhiteSpace(Language, nameof(Language), "Cannot be null or empty")
        );
    }
}