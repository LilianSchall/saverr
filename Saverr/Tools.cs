using System.Globalization;
using System.IO.Compression;
using System.Text.RegularExpressions;

namespace Saverr;

public class Tools
{
    public static List<SaveFile> CollectSaveFiles(string folderpath) => Directory
        .EnumerateFiles(folderpath)
        .Where(path => Regex.IsMatch(Path.GetFileName(path), @"save-[\d-]*.zip"))
        .Select(path =>
        {
            string filename = Path.GetFileName(path);
            string datetime = filename.Substring(5, 10);
            DateTime date = DateTime.ParseExact(
                datetime,
                "yyyy-MM-dd",
                CultureInfo.InvariantCulture,
                DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal
            );
            return new SaveFile(path, date);
        }).ToList();

    public static void NewSave(string sourcePath, string destinationPath)
    {
        string filename = GenerateName(DateTime.UtcNow);
        string zipPath = Path.Combine(destinationPath, filename);

        try
        {
            ZipFile.CreateFromDirectory(sourcePath, zipPath, CompressionLevel.Optimal, false);
            Console.WriteLine($"Successfully created archive at path: {zipPath}");
        }
        catch (Exception e)
        {
            Console.Error.WriteLine($"An error occured while trying to zip {sourcePath}: {e.Message}");
        }
    }

    private static string GenerateName(DateTime date)
    {
        string dateString =
            $"{date.Year.ToString().PadLeft(4, '0')}-{date.Month.ToString().PadLeft(2, '0')}-{date.Day.ToString().PadLeft(2, '0')}";
        return $"save-{dateString}.zip";
    }
}