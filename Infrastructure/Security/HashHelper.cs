using ServiceSchool.Application.Services;
using ServiceSchool.Model;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace ServiceSchool.Infrastructure.Security;

public class HashHelper
{    
    public static string CreateHash(string input)
    {
        using var sha256 = SHA256.Create();
        return Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(input)));   
    }
}