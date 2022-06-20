using CLIPassphrase.Enums;

namespace CLIPassphrase.Models;
public class ResponseModel
{
    public ResponseModel(bool success, object content, ETypeOfError? errorType = null)
    {
        Success = success;
        Content = content;
        ErrorType = errorType;
    }

    public bool Success { get; set; }
    public object Content { get; set; } = "null";
    public ETypeOfError? ErrorType { get; set; }
}