using System.Text;

namespace CUF.Utils;

public class PasswordService
{
    static internal string? SHA512(string passowrd)
    {
        var sha512 = System.Security.Cryptography.SHA512.Create();
        var bytes = Encoding.UTF8.GetBytes(passowrd);
        var hash = sha512.ComputeHash(bytes);

        return Convert.ToBase64String(hash);
    }
}