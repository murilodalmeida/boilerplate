using System.Text.RegularExpressions;

namespace FwksLabs.Libs.Core.Constants;

public partial class AppRegex
{
    [GeneratedRegex(@"^\+\d{1,3}\d{5,14}$")]
    public static partial Regex PhoneNumber();

    [GeneratedRegex(@"[^a-zA-Z0-9]")]
    public static partial Regex Alphanumeric();
}