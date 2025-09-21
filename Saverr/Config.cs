namespace Saverr;

public class Config
{
    public string sourceFolder;
    public string destinationFolder;
    public int nbSaves;

    public Config(string sourceFolder, string destinationFolder, int nbSaves)
    {
        this.sourceFolder = sourceFolder;
        this.destinationFolder = destinationFolder;
        this.nbSaves = nbSaves;
    }

    public static Config From(string[] args)
    {
        if (args.Length != 3)
            throw new ArgumentException($"Usage: ./Saverr.exe [source_folder] [destination_folder] [nb_max_save]");

        try
        {
            int nbMaxSave = Convert.ToInt32(args[2]);
            return new Config(args[0], args[1], nbMaxSave);
        }
        catch (Exception)
        {
            throw new ArgumentException("max save parameter is not a valid integer.");
        }
    }
}