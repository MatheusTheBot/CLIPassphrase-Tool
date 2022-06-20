using Flunt.Notifications;
using Flunt.Validations;

namespace CLIPassphrase.Commands
{
    public class CommandCalculateEntropy : Notifiable<Notification>, ICommand
    {
        public CommandCalculateEntropy(string password)
        {
            Password = password;

            Test();
        }

        public string Password { get; set; }

        public void Test()
        {
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsGreaterOrEqualsThan(Password.Length, 4, nameof(Password), "THe minimum lenght is 4 characters")
            );
        }
    }
}