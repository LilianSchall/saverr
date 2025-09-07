using System.IO.Compression;

namespace Saverr;

public static class Program
{
    public static int Main(string[] args)
    {
        try
        {
            Config config = Config.From(args);
            List<SaveFile> saves = Tools.CollectSaveFiles(config.destinationFolder);
            saves.Sort((a, b) => a.saveDate < b.saveDate ? -1 : a.saveDate == b.saveDate ? 0 : 1);
            
            Tools.NewSave(config.sourceFolder, config.destinationFolder);
            if (saves.Count > config.nbSaves)
            {
                saves
                    .Take(saves.Count - config.nbSaves)
                    .ToList()
                    .ForEach(s => File.Delete(s.path));
            }
        }
        catch (Exception e)
        {
            Console.Error.WriteLine(e.Message);
            return 1;
        }

        return 0;
    }
}