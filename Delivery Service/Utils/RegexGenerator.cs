using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Delivery_Service.Services; 
public enum RegexPatterns {
    PhonePattern,
    PasswordPattern

}

public static partial class RegexGenerator {
    [GeneratedRegex(@"^\+?[0-9]{11}$")]
    private static partial Regex PhoneRegex(); 

    [GeneratedRegex(@"^[a-zA-Z0-9]{6,}$")]
    private static partial Regex PasswordRegex();
    public static Regex GetRegex(RegexPatterns pattern) {
        if (pattern == RegexPatterns.PhonePattern) { return PhoneRegex(); }
        if(pattern == RegexPatterns.PasswordPattern) { return PasswordRegex(); }
        else throw  new ArgumentException("Некорректный паттерн регулярного выражения");
    }

    
}
