using System;

public class KnuthMorrisPratt
{
    private string pattern;
    private int[] border;

    public KnuthMorrisPratt(string pattern)
    {
        this.pattern = pattern;
        this.border = ComputeBorder(pattern);
    }

    private int[] ComputeBorder(string pattern)
    {
        int[] border = new int[pattern.Length];
        border[0] = 0;
        int j = 0;
        for (int i = 1; i < pattern.Length; i++)
        {
            while (j > 0 && pattern[i] != pattern[j])
            {
                j = border[j - 1];
            }
            if (pattern[i] == pattern[j])
            {
                j++;
            }
            border[i] = j;
        }
        return border;
    }

    public int Search(string text)
    {
        int n = text.Length;
        int m = pattern.Length;
        int j = 0;
        for (int i = 0; i < n; i++)
        {
            while (j > 0 && text[i] != pattern[j])
            {
                j = border[j - 1];
            }
            if (text[i] == pattern[j])
            {
                j++;
            }
            if (j == m)
            {
                return i - m + 1;
            }
        }
        return -1;
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
