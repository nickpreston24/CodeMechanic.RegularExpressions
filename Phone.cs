namespace CodeMechanic.RegularExpressions;

public sealed class PhoneNumber
{
    public string country_code { get; set; } = string.Empty;
    public string area_code { get; set; } = string.Empty;
    public string telephone_prefix { get; set; } = string.Empty;
    public string line_number { get; set; } = string.Empty;

    public static string sample = @"1-888-280-4331 
https://linkedphone.com/what-are-the-different-parts-of-a-phone-number-called/";
}

public sealed class PhoneRegex : RegexEnumBase
{
    public static PhoneRegex Full = new PhoneRegex(1, nameof(Full),
        @"((?<country_code>\d)-)?(?<area_code>\d{3})-?(?<telephone_prefix0>\d{3})-(?<line_number>\d{4})",
        "https://regex101.com/r/7JekCE/2");

    public PhoneRegex(int id, string name, string pattern, string uri = "") :
        base(id, name, pattern, uri)
    {
    }
}