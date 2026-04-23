using System.Text.RegularExpressions;
using CodeMechanic.Types;

namespace CodeMechanic.RegularExpressions;

public class RegexEnumBase : Enumeration
{
    protected RegexEnumBase(int id, string name, string pattern, string uri = "")
        : base(id, name)
    {
        Pattern = pattern;
        CompiledRegex = new Regex(
            pattern,
            RegexOptions.IgnorePatternWhitespace
                | RegexOptions.Compiled
                | RegexOptions.IgnoreCase
                | RegexOptions.Multiline
        );
        this.uri = uri;
    }

    public string uri { get; set; } = string.Empty;

    public Regex CompiledRegex { get; set; }
    public string Pattern { get; set; }
}
