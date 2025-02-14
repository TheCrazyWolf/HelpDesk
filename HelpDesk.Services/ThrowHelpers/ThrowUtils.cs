namespace HelpDesk.Services.ThrowHelpers;

public static class ThrowUtils
{
    public static void ThrowIfNull(this object? obj, string type)
    {
        if(obj == null) throw new NullReferenceException($"{type} не найден");
    }
}