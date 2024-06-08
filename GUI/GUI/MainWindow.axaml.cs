using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Dialogs;
using Avalonia.Animation;
using Avalonia.Animation.Easings;
using System.Collections.Generic;
using System.IO;
using System;
using System.Drawing;
using System.IO;
using System.Text;
using Tubes3_TheTorturedInformaticsDepartment;

namespace GUI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.AttachDevTools();
            // Make the window draggable
            this.PointerPressed += OnPointerPressed;

            // Initialize the SelectedImage property
            SelectedImage = this.FindControl<Avalonia.Controls.Image>("SelectedImage");

            // Initialize the AlgorithmButton property
            AlgorithmButton = this.FindControl<SplitButton>("AlgorithmButton");

            // Initialize the HomePanel property
            HomePanel = this.FindControl<StackPanel>("HomePanel");

            // Initialize the FingerPanel property
            FingerPanel = this.FindControl<StackPanel>("FingerPanel");

            // Initialize the AboutUsPanel property
            AboutUsPanel = this.FindControl<StackPanel>("AboutUsPanel");

            // Show the HomePanel and hide all other panels
            HomePanel.IsVisible = true;
            FingerPanel.IsVisible = false;
            AboutUsPanel.IsVisible = false;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void OnPointerPressed(object sender, PointerPressedEventArgs e)
        {
            if (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
            {
                BeginMoveDrag(e);
            }
        }

        private void PowerButton_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            this.Close();
        }

        private void KMP_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (AlgorithmButton != null)
            {
                AlgorithmButton.Content = "KMP";
            }
        }

        private void BM_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (AlgorithmButton != null)
            {
                AlgorithmButton.Content = "BM";
            }
        }

        private async void SelectImageButton_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                AllowMultiple = false,
                Filters = new List<FileDialogFilter> { new FileDialogFilter { Name = "Images", Extensions = new List<string> { "jpg", "png", "bmp" } } }
            };
            var result = await dialog.ShowAsync(this);
            Avalonia.Media.Imaging.Bitmap bitmapA;
            if (result != null && result.Length > 0)
            {

                using (var fileStreamA = new FileStream(result[0], FileMode.Open))
                {
                    bitmapA = new Avalonia.Media.Imaging.Bitmap(fileStreamA);
                    SelectedImage.Source = bitmapA;
                }

                using (var fileStream = new FileStream(result[0], FileMode.Open))
                {
                    var bitmap = new System.Drawing.Bitmap(fileStream);
                    SelectedImage.Source = bitmapA;

                    // New code starts here
                    StringBuilder binaryString = new StringBuilder();
                    StringBuilder asciiString = new StringBuilder();

                    for (int y = 0; y < bitmap.Height; y++)
                    {
                        StringBuilder binaryLine = new StringBuilder();
                        for (int x = 0; x < bitmap.Width; x++)
                        {
                            System.Drawing.Color pixel = bitmap.GetPixel(x, y);
                            binaryLine.Append(pixel.GetBrightness() < 0.5 ? "1" : "0");
                        }

                        binaryString.AppendLine(binaryLine.ToString());

                        for (int i = 0; i < binaryLine.Length; i += 8)
                        {
                            if (i + 8 <= binaryLine.Length)
                            {
                                string byteString = binaryLine.ToString(i, 8);
                                asciiString.Append((char)Convert.ToInt32(byteString, 2));
                            }
                        }

                        asciiString.AppendLine();
                    }

                    string ascii = asciiString.ToString();


                    // // Display binary and ASCII data
                    // Console.WriteLine("Binary Data:");
                    // Console.WriteLine(binaryString.ToString());
                    // Console.WriteLine("ASCII Data:");
                    // Console.WriteLine(asciiString.ToString());
                    // // New code ends here


                }

            }
        }

        private void SubmitButton_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            // get algorithm choice
            string algorithm = AlgorithmButton.Content.ToString();
            Console.WriteLine(algorithm);

            List<string> paths = DB.SelectAllPath();
            bool isMatchFound = false;


            foreach (string path in paths)
            {

                // Load match image and convert to binary and ASCII
                System.Drawing.Bitmap matchBitmap = new System.Drawing.Bitmap("../" + path);
                StringBuilder matchBinaryString = new StringBuilder();
                StringBuilder matchAsciiString = new StringBuilder();

                for (int y = 0; y < matchBitmap.Height; y++)
                {
                    StringBuilder binaryLine = new StringBuilder();
                    for (int x = 0; x < matchBitmap.Width; x++)
                    {
                        System.Drawing.Color pixel = matchBitmap.GetPixel(x, y);
                        binaryLine.Append(pixel.GetBrightness() < 0.5 ? "1" : "0");
                    }

                    matchBinaryString.AppendLine(binaryLine.ToString());

                    for (int i = 0; i < binaryLine.Length; i += 8)
                    {
                        if (i + 8 <= binaryLine.Length)
                        {
                            string byteString = binaryLine.ToString(i, 8);
                            matchAsciiString.Append((char)Convert.ToInt32(byteString, 2));
                        }
                    }
                    matchAsciiString.AppendLine();
                }

                int matchCount = 0;
                int totalCount = Math.Min(asciiString.Length, matchAsciiString.Length);



                // nat nanti asciiStringnya itu dari input image nya
                if (algorithm.ToUpper() == "KMP")
                {
                    isMatchFound = KnuthMorrisPratt(matchAsciiString.ToString(), asciiString.ToString());
                }
                else if (algorithm.ToUpper() == "BM")
                {
                    isMatchFound = BoyerMoore(matchAsciiString.ToString(), asciiString.ToString());
                }

                if (isMatchFound)
                {
                    matchCount = GetMatchCount(asciiString.ToString(), matchAsciiString.ToString());
                    Console.WriteLine("Match found.");
                }
                else
                {
                    Console.WriteLine("No match found.");
                }

                double matchPercentage = (double)matchCount / totalCount * 100;
                Console.WriteLine($"Match percentage: {matchPercentage:F2}%");
            }


        }

        private void HomeButton_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            // Hide all other panels
            FingerPanel.IsVisible = false;
            AboutUsPanel.IsVisible = false;

            // Show the HomePanel
            HomePanel.IsVisible = true;
        }

        private void FingerPrintButton_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            // Hide all other panels
            HomePanel.IsVisible = false;
            AboutUsPanel.IsVisible = false;

            // Show the FingerPanel
            FingerPanel.IsVisible = true;
        }

        private void Person_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            // Hide all other panels
            HomePanel.IsVisible = false;
            FingerPanel.IsVisible = false;

            // Show the AboutUsPanel
            AboutUsPanel.IsVisible = true;
        }

        // Knuth-Morris-Pratt (KMP) algorithm
        static bool KnuthMorrisPratt(string text, string pattern)
        {
            int[] lps = ComputeLPSArray(pattern);
            int i = 0; // index for text
            int j = 0; // index for pattern
            while (i < text.Length)
            {
                if (pattern[j] == text[i])
                {
                    j++;
                    i++;
                }
                if (j == pattern.Length)
                {
                    return true;
                }
                else if (i < text.Length && pattern[j] != text[i])
                {
                    if (j != 0)
                    {
                        j = lps[j - 1];
                    }
                    else
                    {
                        i++;
                    }
                }
            }
            return false;
        }

        static int[] ComputeLPSArray(string pattern)
        {
            int[] lps = new int[pattern.Length];
            int length = 0;
            int i = 1;
            lps[0] = 0;
            while (i < pattern.Length)
            {
                if (pattern[i] == pattern[length])
                {
                    length++;
                    lps[i] = length;
                    i++;
                }
                else
                {
                    if (length != 0)
                    {
                        length = lps[length - 1];
                    }
                    else
                    {
                        lps[i] = 0;
                        i++;
                    }
                }
            }
            return lps;
        }

        // Boyer-Moore (BM) algorithm
        static bool BoyerMoore(string text, string pattern)
        {
            int[] badChar = BuildBadCharTable(pattern);
            int shift = 0;
            while (shift <= (text.Length - pattern.Length))
            {
                int j = pattern.Length - 1;
                while (j >= 0 && pattern[j] == text[shift + j])
                {
                    j--;
                }
                if (j < 0)
                {
                    return true;
                }
                else
                {
                    shift += Math.Max(1, j - badChar[text[shift + j]]);
                }
            }
            return false;
        }

        static int[] BuildBadCharTable(string pattern)
        {
            int[] badChar = new int[256];
            for (int i = 0; i < 256; i++)
            {
                badChar[i] = -1;
            }
            for (int i = 0; i < pattern.Length; i++)
            {
                badChar[(int)pattern[i]] = i;
            }
            return badChar;
        }

        // Function to get the number of matching characters
        static int GetMatchCount(string text, string pattern)
        {
            int matchCount = 0;
            for (int i = 0; i < Math.Min(text.Length, pattern.Length); i++)
            {
                if (text[i] == pattern[i])
                {
                    matchCount++;
                }
            }
            return matchCount;
        }
    }


}