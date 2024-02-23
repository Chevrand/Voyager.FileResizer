using System.Drawing;

namespace Voyager.FileResizer.Services;

public static class ResizerService
{
    private static readonly string _inputDirectory = "Input_Files";
    private static readonly string _finishedDirectory = "Finished_Files";
    private static readonly string _outputDirectory = "Output_Files";

    public static void Resize(int height)
    {
        Console.Clear();
        Console.WriteLine("\nVerifying directories ...");

        CreateDirectories();

        Console.WriteLine("\nVerifying files ...");

        var files = Directory.EnumerateFiles(_inputDirectory);

        Console.WriteLine("\nResizing files ...\n");

        ResizeFiles(files, height);
    }

    private static void CreateDirectories()
    {
        if (!Directory.Exists(_inputDirectory))
            Directory.CreateDirectory(_inputDirectory);

        if (!Directory.Exists(_finishedDirectory))
            Directory.CreateDirectory(_finishedDirectory);

        if (!Directory.Exists(_outputDirectory))
            Directory.CreateDirectory(_outputDirectory);
    }

    private static void ResizeFiles(IEnumerable<string> files, int height)
    {
        Image image;
        Bitmap newImage;
        Graphics g;
        double ratio;
        int newWidth;
        int newHeight;

        foreach (var file in files)
        {
            try
            {
                image = Image.FromFile(file);
                ratio = (double)height / image.Height;
                newWidth = (int)(image.Width * ratio);
                newHeight = (int)(image.Height * ratio);
                newImage = new Bitmap(newWidth, newHeight);

                g = Graphics.FromImage(newImage);
                g.DrawImage(image, 0, 0, newWidth, newHeight);

                newImage.Save(Path.Combine(_outputDirectory, Path.GetFileName(file)));
                image.Dispose();
                g.Dispose();

                MoveFileTo(file, _finishedDirectory);

                Console.WriteLine($"File '{file}' resized successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unable to resize file '{file}': {ex.Message}");
            }
        }
    }

    private static void MoveFileTo(string file, string targetDirectory)
    {
        var destFileName = Path.Combine(targetDirectory, Path.GetFileName(file));

        File.Move(file, destFileName, true);
    }
}
