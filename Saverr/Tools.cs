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
            SafeCreateZipFromDirectory(sourcePath, zipPath);
            Console.WriteLine($"Successfully created archive at path: {zipPath}");
        }
        catch (Exception e)
        {
            Console.Error.WriteLine($"An error occured while trying to zip {sourcePath}: {e.Message}");
        }
    }
    
    public static void SafeCreateZipFromDirectory(string sourcePath, string zipPath)
    {
        using (FileStream zipFileStream = new FileStream(zipPath, FileMode.Create))
        {
            using (ZipArchive archive = new ZipArchive(zipFileStream, ZipArchiveMode.Create))
            {
                AddDirectoryToArchive(sourcePath, string.Empty, archive);
            }
        }
    }

    private static void AddDirectoryToArchive(string sourceDirectory, string entryName, ZipArchive archive)
    {
        try
        {
            foreach (var file in Directory.GetFiles(sourceDirectory))
            {
                string entryFilePath = Path.Combine(entryName, Path.GetFileName(file));
                try
                {
                    archive.CreateEntryFromFile(file, entryFilePath, CompressionLevel.Optimal);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Skipping file {entryFilePath}: {e.Message}");
                }
            }
            
            // Recursively process subdirectories
            foreach (var subDirectory in Directory.GetDirectories(sourceDirectory))
            {
                // Recursive call to handle subdirectories.
                AddDirectoryToArchive(subDirectory, Path.Combine(entryName, Path.GetFileName(subDirectory)), archive);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Skipping directory {sourceDirectory}: {e.Message}");
        }
    }

    private static string GenerateName(DateTime date)
    {
        string dateString =
            $"{date.Year.ToString().PadLeft(4, '0')}-{date.Month.ToString().PadLeft(2, '0')}-{date.Day.ToString().PadLeft(2, '0')}";
        return $"save-{dateString}.zip";
    }
}