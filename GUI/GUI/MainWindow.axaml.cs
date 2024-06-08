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
            SelectedImage = this.FindControl<Image>("SelectedImage");

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
                var bitmap = new Bitmap(new FileStream(result[0], FileMode.Open));
                SelectedImage.Source = bitmap;
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