using System;

public class BoyerMoore
{
    private int[] last;
    private string pattern;

    public BoyerMoore(string pattern)
    {
        this.pattern = pattern;
        this.last = BuildLast(pattern);
    }

    private int[] BuildLast(string pattern)
    {
        int[] last = new int[128 * 2];
        for (int i = 0; i < 128 * 2; i++)
        {
            last[i] = -1;
        }
        for (int i = 0; i < pattern.Length; i++)
        {
            last[(int)pattern[i]] = i;
        }
        return last;
    }

    public int Search(string text)
    {
        int n = text.Length;
        int m = pattern.Length;
        int i = m - 1;

        if (i > n - 1)
        {
            return -1; // no match if pattern is longer than text
        }

        int j = m - 1;
        do
        {
            if (pattern[j] == text[i])
            {
                if (j == 0)
                {
                    return i; // match
                }
                else
                {
                    i--;
                    j--;
                }
            }
            else
            {
                int lo = last[(int)text[i]]; // last occurrence
                i = i + m - Math.Min(j, 1 + lo);
                j = m - 1;
            }
        } while (i <= n - 1);

        return -1; // no match
    }

    public double CalculateSimilarity(string text)
    {
        int matches = 0;
        int totalMatches = 0;
        int total = text.Length;
        int patternLength = pattern.Length;

        for (int i = 0; i < text.Length - pattern.Length + 1; i++)
        {
            if (Search(text.Substring(i, pattern.Length)) != -1)
            {
                totalMatches += pattern.Length;
                i += pattern.Length - 1;
            }
        }

        return ((double)totalMatches / total) * 100;
    }
}
