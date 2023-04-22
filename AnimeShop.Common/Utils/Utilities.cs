using System.Security.Cryptography;
using System.Text;

namespace AnimeShop.Common.Utils;

public static class Utilities
{
    private const string SALT = "cust0ms4ltt000ld";
    
    public static string ComputeHashForPassword(string password)
    {
        SHA256 sha256 = SHA256.Create();
        var passwordToBytes = Encoding.ASCII.GetBytes(password + SALT);
        var hash = sha256.ComputeHash(passwordToBytes);

        string resultPassword = "";
        foreach (var c in hash)
        {
            resultPassword += (char)(c % 26 + 97);
        }

        return resultPassword;
    }
}