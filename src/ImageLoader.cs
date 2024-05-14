using System;
using System.Drawing;
using System.IO;
using System.Text;
using Tubes3_TheTorturedInformaticsDepartment;

public class ImageLoader
{
    public static void LoadImageFolder (string directoryPath)
    {

        // Check if the directory exists
        if (!Directory.Exists(directoryPath))
        {
            Console.WriteLine("Directory does not exist.");
            return;
        }

        // Get all BMP files in the directory
        string[] imageFiles = Directory.GetFiles(directoryPath, "*.bmp", SearchOption.TopDirectoryOnly);

        if (imageFiles.Length == 0)
        {
            Console.WriteLine("No BMP files found in the directory.");
            return;
        }

        // Iterate over each BMP file
        foreach (string imagePath in imageFiles)
        {
            try
            {
                // Load the image
                Bitmap bitmap = new Bitmap(imagePath);

                // Convert binary to ASCII
                StringBuilder binaryString = new StringBuilder();
                StringBuilder asciiString = new StringBuilder();

                for (int y = 0; y < bitmap.Height; y++)
                {
                    StringBuilder binaryLine = new StringBuilder();
                    for (int x = 0; x < bitmap.Width; x++)
                    {
                        Color pixel = bitmap.GetPixel(x, y);
                        binaryLine.Append(pixel.GetBrightness() < 0.5 ? "1" : "0");
                    }

                    if (y < bitmap.Height - 1)
                    {
                        binaryString.AppendLine(binaryLine.ToString());
                    }
                    else
                    {
                        binaryString.Append(binaryLine.ToString());
                    }

                    for (int i = 0; i < binaryLine.Length; i += 8)
                    {
                        if (i + 8 <= binaryLine.Length)
                        {
                            string byteString = binaryLine.ToString(i, 8);
                            asciiString.Append((char)Convert.ToInt32(byteString, 2));
                        }
                    }

                    if (y < bitmap.Height - 1)
                    {
                        asciiString.AppendLine();
                    }
                }

                Console.WriteLine($"Processing image: {Path.GetFileName(imagePath)}");
                Console.WriteLine("Binary Data:");
                Console.WriteLine(binaryString.ToString());

                Console.WriteLine("ASCII Data:");
                Console.WriteLine(asciiString.ToString());

                // Insert ASCII data into database
                DB.Insert(asciiString.ToString(), Path.GetFileName(imagePath));

                // Cleanup
                bitmap.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while processing {Path.GetFileName(imagePath)}: {ex.Message}");
            }
        }

        Console.WriteLine("Processing completed.");
    }
}