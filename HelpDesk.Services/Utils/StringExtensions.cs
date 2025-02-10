using System.Security.Cryptography;
using System.Text;

namespace HelpDesk.Services.Utils;

public static class StringExtensions
{
    public static string GetHash(this string value)
    {
        using SHA256 sha256 = SHA256.Create();
        byte[] inputBytes = Encoding.UTF8.GetBytes(value);
        byte[] hashBytes = sha256.ComputeHash(inputBytes);
        StringBuilder sb = new StringBuilder();
        foreach (var t in hashBytes) sb.Append(t.ToString("x2"));
        return sb.ToString();
    }
}