using System;
using System.Drawing;
using System.IO;
using System.Text;

class FingerprintRecognition
{
    static void Main(string[] args)
    {
        // path
        string directoryPath = "./fingerprints/";
        Console.WriteLine("Enter the image name (with .bmp extension):");
        string? imageName = Console.ReadLine();

        if (imageName == null)
        {
            Console.WriteLine("No input provided.");
            return;
        }

        string imagePath = Path.Combine(directoryPath, imageName);
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("File does not exist.");
            Console.WriteLine(imagePath);
            
            return;
        }

        // load image
        Bitmap bitmap = new Bitmap(imagePath);

        // convert binary to ascii
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


        Console.WriteLine("Binary Data:");
        Console.WriteLine(binaryString.ToString());

        Console.WriteLine("ASCII Data:");
        Console.WriteLine(asciiString.ToString());

        // cleanup
        bitmap.Dispose();
    }
}