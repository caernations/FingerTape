using System.Globalization;
using System.Text.RegularExpressions;

public class BahasaAlay
{
    private static List<string> OriginalNames = new List<string> { "Bintang Dwi Marthen", "Bernardus Willson", "Tricya", "TaylorSwift" };
    private static readonly Dictionary<string, List<string>> characterReplacements = new Dictionary<string, List<string>>()
    {
        {"a", new List<string> {"4", "A", "a"}}, 
        {"b", new List<string> {"B", "b", "8"}},
        {"c", new List<string> {"C", "c"}}, 
        {"d", new List<string> {"D", "d"}},
        {"e", new List<string> {"3", "E", "e"}}, 
        {"f", new List<string> {"F", "f"}},
        {"g", new List<string> {"G", "g", "6", "9"}}, 
        {"h", new List<string> {"H", "h"}},
        {"i", new List<string> {"!", "1", "I", "i"}}, 
        {"j", new List<string> {"J", "j"}},
        {"k", new List<string> {"K", "k"}}, 
        {"l", new List<string> {"L", "l", "1"}},
        {"m", new List<string> {"M", "m"}}, 
        {"n", new List<string> {"N", "n"}},
        {"o", new List<string> {"0", "O", "o"}}, 
        {"p", new List<string> {"P", "p"}},
        {"q", new List<string> {"Q", "q", "9"}}, 
        {"r", new List<string> {"R", "r"}},
        {"s", new List<string> {"S", "s", "5"}}, 
        {"t", new List<string> {"T", "t", "7"}},
        {"u", new List<string> {"U", "u"}}, 
        {"v", new List<string> {"V", "v"}},
        {"w", new List<string> {"W", "w"}}, 
        {"x", new List<string> {"X", "x"}},
        {"y", new List<string> {"Y", "y"}}, 
        {"z", new List<string> {"Z", "z", "2"}}
    };

    private static readonly Random randomGenerator = new Random();
    private static readonly Dictionary<string, string> alayToOriginalMapping;
    private static readonly string regularExpression;

    static BahasaAlay()
    {
        alayToOriginalMapping = new Dictionary<string, string>();
        foreach (var pair in characterReplacements)
        {
            foreach (var val in pair.Value)
            {
                if (!alayToOriginalMapping.ContainsKey(val.ToLower()))
                {
                    alayToOriginalMapping[val.ToLower()] = pair.Key;
                }
            }
        }

        var sortedKeys = alayToOriginalMapping.Keys.OrderByDescending(s => s.Length).ToList();
        regularExpression = string.Join("|", sortedKeys.Select(Regex.Escape));
    }

    public static string OriginalToAlay(string originalText, bool NumberSymbol = true, bool MixedCase = true, bool RemoveVowelLetters = true)
    {
        string convertedText = originalText.ToLower();

        if (NumberSymbol)
        {
            convertedText = new string(convertedText.Select(c => characterReplacements.ContainsKey(c.ToString()) ? characterReplacements[c.ToString()][randomGenerator.Next(characterReplacements[c.ToString()].Count)][0] : c).ToArray());
        }

        if (MixedCase)
        {
            convertedText = new string(convertedText.Select(c => randomGenerator.Next(2) == 0 ? char.ToUpper(c) : c).ToArray());
        }

        if (RemoveVowelLetters)
        {
            double removalProbability = 0.5;
            var vowels = new HashSet<char> { 'a', 'e', 'i', 'o', 'u' };
            convertedText = new string(convertedText.Where(c => !vowels.Contains(c)).ToArray());
            Random random = new Random();
            convertedText = new(convertedText.Where(c => !vowels.Contains(c) || random.NextDouble() > removalProbability).ToArray());
        }

        return convertedText;
    }

    public static string AlayToOriginal(string alayText)
    {
        Regex regex = new Regex(regularExpression, RegexOptions.IgnoreCase);
        string convertedText = regex.Replace(alayText, match =>
        {
            string key = match.Value.ToLower();
            if (alayToOriginalMapping.ContainsKey(key))
            {
                return alayToOriginalMapping[key];
            }
            return match.Value;
        });

        return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(convertedText.ToLower());
    }
}