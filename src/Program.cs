public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("1. Original to Alay");
        Console.WriteLine("2. Alay to Original");
        Console.Write("Masukkan pilihan (1 atau 2): ");
        string choice = Console.ReadLine();

        if (choice == null)
        {
            Console.WriteLine("Input tidak valid.");
            return;
        }

        if (choice == "1")
        {
            Console.WriteLine("Masukkan kata orisinil:");
            string fullName = Console.ReadLine();

            if (fullName == null)
            {
                Console.WriteLine("Input tidak valid.");
                return;
            }

            string alayName = BahasaAlay.OriginalToAlay(fullName);

            // Output the alay version
            Console.WriteLine("Nama dalam bahasa alay: " + alayName);
        }
        else if (choice == "2")
        {
            Console.WriteLine("Masukkan kata dalam bahasa alay:");
            string alayName = Console.ReadLine();

            if (alayName == null)
            {
                Console.WriteLine("Input tidak valid.");
                return;
            }

            string originalName = BahasaAlay.AlayToOriginal(alayName);

            Console.WriteLine("Nama orisinil: " + originalName);
        }
        else
        {
            Console.WriteLine("Pilihan tidak valid.");
        }
    }
}

// MAIN PROGRAM BARU BARU BARUUU

// using System;

// class Program
// {
//     static void Main(string[] args)
//     {
//         Console.WriteLine("Choose algorithm (1 for Boyer-Moore, 2 for Knuth-Morris-Pratt):");
//         int choice = int.Parse(Console.ReadLine());

//         Console.WriteLine("Enter the text:");
//         string text = Console.ReadLine();

//         Console.WriteLine("Enter the pattern:");
//         string pattern = Console.ReadLine();

//         if (choice == 1)
//         {
//             BoyerMoore bm = new BoyerMoore(pattern);
//             int matchIndex = bm.Search(text);

//             if (matchIndex != -1)
//             {
//                 Console.WriteLine($"Pattern found at index {matchIndex}");
//             }
//             else
//             {
//                 Console.WriteLine("Pattern not found");
//             }

//             double similarityPercentage = bm.CalculateSimilarity(text);
//             Console.WriteLine($"Similarity: {similarityPercentage}%");
//         }
//         else if (choice == 2)
//         {
//             KnuthMorrisPratt kmp = new KnuthMorrisPratt(pattern);
//             int matchIndex = kmp.Search(text);

//             if (matchIndex != -1)
//             {
//                 Console.WriteLine($"Pattern found at index {matchIndex}");
//             }
//             else
//             {
//                 Console.WriteLine("Pattern not found");
//             }

//             double similarityPercentage = kmp.CalculateSimilarity(text);
//             Console.WriteLine($"Similarity: {similarityPercentage}%");
//         }
//         else
//         {
//             Console.WriteLine("Invalid choice");
//         }
//     }
// }


// MAIN PROGRAM ASLI LAMA
// using System;
// using System.Drawing;
// using System.IO;
// using System.Text;

// class FingerprintRecognition
// {
//     static void Main(string[] args)
//     {
//         // Step 1: Input image name (bitmap)
//         string directoryPath = "./fingerprints/";
//         Console.WriteLine("Enter the image name (with .bmp extension):");
//         string? imageName = Console.ReadLine();
        
//         if (imageName == null)
//         {
//             Console.WriteLine("No input provided.");
//             return;
//         }

//         string imagePath = Path.Combine(directoryPath, imageName);
//         if (!File.Exists(imagePath))
//         {
//             Console.WriteLine("File does not exist.");
//             Console.WriteLine(imagePath);
//             return;
//         }

//         // Step 2 and 3: Load image and convert bitmap to binary and then to ASCII
//         Bitmap bitmap = new Bitmap(imagePath);
//         StringBuilder binaryString = new StringBuilder();
//         StringBuilder asciiString = new StringBuilder();

//         for (int y = 0; y < bitmap.Height; y++)
//         {
//             StringBuilder binaryLine = new StringBuilder();
//             for (int x = 0; x < bitmap.Width; x++)
//             {
//                 Color pixel = bitmap.GetPixel(x, y);
//                 binaryLine.Append(pixel.GetBrightness() < 0.5 ? "1" : "0");
//             }

//             binaryString.AppendLine(binaryLine.ToString());

//             for (int i = 0; i < binaryLine.Length; i += 8)
//             {
//                 if (i + 8 <= binaryLine.Length)
//                 {
//                     string byteString = binaryLine.ToString(i, 8);
//                     asciiString.Append((char)Convert.ToInt32(byteString, 2));
//                 }
//             }
//             asciiString.AppendLine();
//         }

//         // Display binary and ASCII data
//         Console.WriteLine("Binary Data:");
//         Console.WriteLine(binaryString.ToString());
//         Console.WriteLine("ASCII Data:");
//         Console.WriteLine(asciiString.ToString());

//         // Step 4: Input the image name to be matched
//         Console.WriteLine("Enter the image name to be matched (with .bmp extension):");
//         string? matchImageName = Console.ReadLine();
        
//         if (matchImageName == null)
//         {
//             Console.WriteLine("No input provided.");
//             return;
//         }

//         string matchImagePath = Path.Combine(directoryPath, matchImageName);
//         if (!File.Exists(matchImagePath))
//         {
//             Console.WriteLine("File does not exist.");
//             Console.WriteLine(matchImagePath);
//             return;
//         }

//         // Load match image and convert to binary and ASCII
//         Bitmap matchBitmap = new Bitmap(matchImagePath);
//         StringBuilder matchBinaryString = new StringBuilder();
//         StringBuilder matchAsciiString = new StringBuilder();

//         for (int y = 0; y < matchBitmap.Height; y++)
//         {
//             StringBuilder binaryLine = new StringBuilder();
//             for (int x = 0; x < matchBitmap.Width; x++)
//             {
//                 Color pixel = matchBitmap.GetPixel(x, y);
//                 binaryLine.Append(pixel.GetBrightness() < 0.5 ? "1" : "0");
//             }

//             matchBinaryString.AppendLine(binaryLine.ToString());

//             for (int i = 0; i < binaryLine.Length; i += 8)
//             {
//                 if (i + 8 <= binaryLine.Length)
//                 {
//                     string byteString = binaryLine.ToString(i, 8);
//                     matchAsciiString.Append((char)Convert.ToInt32(byteString, 2));
//                 }
//             }
//             matchAsciiString.AppendLine();
//         }
        
//         // Display binary and ASCII data for match image
//         Console.WriteLine("Match Binary Data:");
//         Console.WriteLine(matchBinaryString.ToString());
//         Console.WriteLine("Match ASCII Data:");
//         Console.WriteLine(matchAsciiString.ToString());

//         // Step 5: User inputs algorithm choice (KMP or BM)
//         Console.WriteLine("Choose pattern matching algorithm (KMP/BM):");
//         string? algorithmChoice = Console.ReadLine();

//         if (algorithmChoice == null)
//         {
//             Console.WriteLine("No input provided.");
//             return;
//         }

//         // Step 6: Use the chosen algorithm to match ASCII strings
//         bool isMatchFound = false;
//         int matchCount = 0;
//         int totalCount = Math.Min(asciiString.Length, matchAsciiString.Length);

//         if (algorithmChoice.ToUpper() == "KMP")
//         {
//             isMatchFound = KnuthMorrisPratt(matchAsciiString.ToString(), asciiString.ToString());
//         }
//         else if (algorithmChoice.ToUpper() == "BM")
//         {
//             isMatchFound = BoyerMoore(matchAsciiString.ToString(), asciiString.ToString());
//         }
//         else
//         {
//             Console.WriteLine("Invalid algorithm choice.");
//             return;
//         }

//         // Step 7: Display match result and percentage
//         if (isMatchFound)
//         {
//             matchCount = GetMatchCount(asciiString.ToString(), matchAsciiString.ToString());
//             Console.WriteLine("Match found.");
//         }
//         else
//         {
//             Console.WriteLine("No match found.");
//         }

//         double matchPercentage = (double)matchCount / totalCount * 100;
//         Console.WriteLine($"Match percentage: {matchPercentage:F2}%");

//         // Cleanup
//         bitmap.Dispose();
//         matchBitmap.Dispose();
//     }

//     // Knuth-Morris-Pratt (KMP) algorithm
//     static bool KnuthMorrisPratt(string text, string pattern)
//     {
//         int[] lps = ComputeLPSArray(pattern);
//         int i = 0; // index for text
//         int j = 0; // index for pattern
//         while (i < text.Length)
//         {
//             if (pattern[j] == text[i])
//             {
//                 j++;
//                 i++;
//             }
//             if (j == pattern.Length)
//             {
//                 return true;
//             }
//             else if (i < text.Length && pattern[j] != text[i])
//             {
//                 if (j != 0)
//                 {
//                     j = lps[j - 1];
//                 }
//                 else
//                 {
//                     i++;
//                 }
//             }
//         }
//         return false;
//     }

//     static int[] ComputeLPSArray(string pattern)
//     {
//         int[] lps = new int[pattern.Length];
//         int length = 0;
//         int i = 1;
//         lps[0] = 0;
//         while (i < pattern.Length)
//         {
//             if (pattern[i] == pattern[length])
//             {
//                 length++;
//                 lps[i] = length;
//                 i++;
//             }
//             else
//             {
//                 if (length != 0)
//                 {
//                     length = lps[length - 1];
//                 }
//                 else
//                 {
//                     lps[i] = 0;
//                     i++;
//                 }
//             }
//         }
//         return lps;
//     }

//     // Boyer-Moore (BM) algorithm
//     static bool BoyerMoore(string text, string pattern)
//     {
//         int[] badChar = BuildBadCharTable(pattern);
//         int shift = 0;
//         while (shift <= (text.Length - pattern.Length))
//         {
//             int j = pattern.Length - 1;
//             while (j >= 0 && pattern[j] == text[shift + j])
//             {
//                 j--;
//             }
//             if (j < 0)
//             {
//                 return true;
//             }
//             else
//             {
//                 shift += Math.Max(1, j - badChar[text[shift + j]]);
//             }
//         }
//         return false;
//     }

//     static int[] BuildBadCharTable(string pattern)
//     {
//         int[] badChar = new int[256];
//         for (int i = 0; i < 256; i++)
//         {
//             badChar[i] = -1;
//         }
//         for (int i = 0; i < pattern.Length; i++)
//         {
//             badChar[(int)pattern[i]] = i;
//         }
//         return badChar;
//     }

//     // Function to get the number of matching characters
//     static int GetMatchCount(string text, string pattern)
//     {
//         int matchCount = 0;
//         for (int i = 0; i < Math.Min(text.Length, pattern.Length); i++)
//         {
//             if (text[i] == pattern[i])
//             {
//                 matchCount++;
//             }
//         }
//         return matchCount;
//     }
// }