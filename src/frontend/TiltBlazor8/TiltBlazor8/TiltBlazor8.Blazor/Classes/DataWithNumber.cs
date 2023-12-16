namespace TiltBlazor8.Blazor.Classes;

public record DataWithNumber<T>(int number, T data)
    where T : class
{
}
