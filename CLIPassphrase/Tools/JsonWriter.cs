using Microsoft.Extensions.Configuration;

namespace CLIPassphrase.Tools;
public class JsonWriter
{
    private readonly IConfiguration _configs;

    public JsonWriter(IConfiguration configs)
    {
        _configs = configs;
    }

    public void Modify(string key, string newValue)
    {
        _configs[key] = newValue;
    }
}