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
            if (result != null && result.Length > 0)
            {
                var bitmapA = new Avalonia.Media.Imaging.Bitmap(new FileStream(result[0], FileMode.Open));
                var bitmap = new System.Drawing.Bitmap(new FileStream(result[0], FileMode.Open));
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

                // Display binary and ASCII data
                Console.WriteLine("Binary Data:");
                Console.WriteLine(binaryString.ToString());
                Console.WriteLine("ASCII Data:");
                Console.WriteLine(asciiString.ToString());
                // New code ends here
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