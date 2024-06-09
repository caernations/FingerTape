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
using System.Text;
using Tubes3_TheTorturedInformaticsDepartment;

namespace GUI
{
    public partial class MainWindow : Window
    {
        private string ascii;

        public MainWindow()
        {
            InitializeComponent();
            this.AttachDevTools();
            // Make the window draggable
            this.PointerPressed += OnPointerPressed;

            // Initialize the SelectedImage property
            SelectedImage = this.FindControl<Avalonia.Controls.Image>("SelectedImage");

            // Initialize the ResultImage property
            ResultImage = this.FindControl<Avalonia.Controls.Image>("ResultImage");

            // Initialize the Percentage property
            Percentage = this.FindControl<TextBlock>("Percentage");

            // Initialize the Time property
            Time = this.FindControl<TextBlock>("Time");

            Nama = this.FindControl<TextBlock>("Nama");

            TempatLahir = this.FindControl<TextBlock>("TempatLahir");

            TanggalLahir = this.FindControl<TextBlock>("TanggalLahir");

            JenisKelamin = this.FindControl<TextBlock>("JenisKelamsin");

            GolonganDarah = this.FindControl<TextBlock>("GolonganDarah");

            Alamat = this.FindControl<TextBlock>("Alamat");

            Agama = this.FindControl<TextBlock>("Agama");

            StatusPerkawinan = this.FindControl<TextBlock>("StatusPerkawinan");

            Pekerjaan = this.FindControl<TextBlock>("Pekerjaan");

            Kewarganegaraan = this.FindControl<TextBlock>("Kewarganegaraan");

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

                using (var fileStreamA = new FileStream(result[0], FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    bitmapA = new Avalonia.Media.Imaging.Bitmap(fileStreamA);
                    SelectedImage.Source = bitmapA;
                }

                using (var fileStream = new FileStream(result[0], FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
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

                    ascii = asciiString.ToString();
                }

            }
        }

        private void SubmitButton_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            // get algorithm choice
            string algorithm = AlgorithmButton.Content.ToString();

            List<string> paths = DB.SelectAllPaths();

            double highestMatchPercentage = 0;
            string highestMatchImagePath = null;
            string highestMatchImagePathOriginal = null;

            var startTime = DateTime.Now;

            foreach (string path in paths)
            {
                // Construct the full path to the image file
                string imagePath = System.IO.Path.Combine("../../../../../", path);

                if (!File.Exists(imagePath))
                {
                    Console.WriteLine($"File not found: {imagePath}");
                    continue;
                }

                try
                {
                    // Load match image and convert to binary and ASCII
                    System.Drawing.Bitmap matchBitmap = new System.Drawing.Bitmap(imagePath);
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
                    double similarityPercentage = 0;

                    // Determine algorithm to use
                    if (algorithm.ToUpper() == "KMP")
                    {

                        KnuthMorrisPratt kmp = new KnuthMorrisPratt(ascii.ToString());
                        int matchIndex = kmp.Search(matchAsciiString.ToString());

                        if (matchIndex != -1)
                        {
                            Console.WriteLine($"Pattern found at index {matchIndex}");
                        }
                        else
                        {
                            Console.WriteLine("Pattern not found");
                        }

                        similarityPercentage = kmp.CalculateSimilarity(matchAsciiString.ToString());
                        Console.WriteLine($"Similarity: {similarityPercentage}%");
                    }
                    else if (algorithm.ToUpper() == "BM")
                    {
                        BoyerMoore bm = new BoyerMoore(ascii.ToString());
                        int matchIndex = bm.Search(matchAsciiString.ToString());

                        if (matchIndex != -1)
                        {
                            Console.WriteLine($"Pattern found at index {matchIndex}");
                        }
                        else
                        {
                            Console.WriteLine("Pattern not found");
                        }

                        similarityPercentage = bm.CalculateSimilarity(matchAsciiString.ToString());
                        Console.WriteLine($"Similarity: {similarityPercentage}%");
                    }


                    if (similarityPercentage > highestMatchPercentage)
                    {
                        highestMatchPercentage = similarityPercentage;
                        highestMatchImagePath = imagePath;
                        highestMatchImagePathOriginal = path;
                        Console.WriteLine($"New highest match percentage: {highestMatchPercentage:F2}%");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while processing the image at {imagePath}: {ex.Message}");
                }


            }

            string nama_db = DB.GetNamaFromPath(highestMatchImagePathOriginal);
            List<string> names = new List<string>();
            names.Add(nama_db);
            BahasaAlay regex = new BahasaAlay(names);
            List<List<string>> biodata = DB.GetBiodata();

            var endTime = DateTime.Now;
            var executionTime = endTime - startTime;

            if (highestMatchImagePath != null)
            {
                using (var fileStream = new FileStream(highestMatchImagePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    var bitmap = new Avalonia.Media.Imaging.Bitmap(fileStream);
                    ResultImage.Source = bitmap;
                }
            }
            // Update the TextBlock controls with the highestMatchPercentage and the execution time
            Percentage.Text = $"Match Percentage: {highestMatchPercentage:F2}%";
            Time.Text = $"Execution Time: {executionTime.TotalMilliseconds} ms";

            foreach (List<string> data in biodata)
            {
                string nama = BahasaAlay.AlayToOriginal(data[1]);

                if (regex.GetMostSimilarOriginalName(nama) == nama_db)
                {
                    Console.WriteLine("Nama: " + regex.GetMostSimilarOriginalName(nama));
                    Nama.Text = "Nama:\n" + regex.GetMostSimilarOriginalName(nama);
                    Console.WriteLine("Tempat Lahir: " + data[2]);
                    TempatLahir.Text = "Tempat Lahir:\n" + data[2];
                    Console.WriteLine("Tanggal Lahir: " + data[3]);
                    TanggalLahir.Text = "Tanggal Lahir:\n" + data[3];
                    Console.WriteLine("Jenis Kelamin: " + data[4]);
                    JenisKelamin.Text = "Jenis Kelamin:\n" + data[4];
                    Console.WriteLine("Golongan Darah: " + data[5]);
                    GolonganDarah.Text = "Golongan Darah:\n" + data[5];
                    Console.WriteLine("Alamat: " + data[6]);
                    Alamat.Text = "Alamat:\n" + data[6];
                    Console.WriteLine("Agama: " + data[7]);
                    Agama.Text = "Agama:\n" + data[7];
                    Console.WriteLine("Status Perkawinan: " + data[8]);
                    StatusPerkawinan.Text = "Status Perkawinan:\n" + data[8];
                    Console.WriteLine("Pekerjaan: " + data[9]);
                    Pekerjaan.Text = "Pekerjaan:\n" + data[9];
                    Console.WriteLine("Kewarganegaraan: " + data[10]);
                    Kewarganegaraan.Text = "Kewarganegaraan:\n" + data[10];
                }
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
    }
}