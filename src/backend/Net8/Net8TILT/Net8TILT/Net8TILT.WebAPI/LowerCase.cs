using System.Text.Json;

namespace Net8TILT.WebAPIWebAPI;
public class LowerCaseNamingPolicy : JsonNamingPolicy
{
    public override string ConvertName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return name;

        return name.ToLower();
    }
}
