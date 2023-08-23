using User.Domain.Abstracts;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace User.Domain.AgregattesModel.ValueObjects;

public record Password : ValueObject
{
    private const int MinimumLength = 12;
    private const string UpperCasePattern = @"[A-Z]";
    private const string LowerCasePattern = @"[a-z]";
    private const string DigitPattern = @"\d";
    private const string SpecialCharacterPattern = @"[@#$%^&+=]";
    
    private readonly string _passwordOriginal;


    public string Value { get; }

	public Password(string password)
    {
        Value = HashPassword(password);
        _passwordOriginal = password;
    }

    public bool IsComplex(string password)
    {
        if (password.Length < MinimumLength)
            return false;

        return Regex.IsMatch(password, UpperCasePattern) &&
               Regex.IsMatch(password, LowerCasePattern) &&
               Regex.IsMatch(password, DigitPattern) &&
               Regex.IsMatch(password, SpecialCharacterPattern);
    }

    private string HashPassword(string password)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            byte[] hashBytes = sha256.ComputeHash(bytes);
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
    }    
    public override int GetHashCode() => Value.GetHashCode();
    public static Password Create(string value) => new Password(value);    

    public override void Validate()
    {
        if (string.IsNullOrEmpty(_passwordOriginal))
        {
            AddNotification(new Notification("Senha", "Senha não pode ser nula ou vazio"));
            return;
        }            

        if (IsComplex(_passwordOriginal) == false)
        {
            AddNotification(new Notification("Senha", "A senha não atende às políticas de complexidade."));
            return;
        }
    }
}
